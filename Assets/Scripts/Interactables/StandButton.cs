/*
 * This script encapsulates the functionality of a stand button.
 * When a gameObject is on a stand button, the stand button will
 * depress (move towards the ground).
 * 
 * After a certain amount of time (as dictated by waitDuration),
 * the stand button will resurface from the ground.
 * While the stand button is resurfacing, it is still sensitive to
 * collisions and will still depress if it senses one.
 * 
 * A stand button can be paired with another gameObject with an
 * attached script that implements the IMovable interface.
 * When the stand button is depressed, the corresponding gameObject will
 * execute its Move().
 */

using System.Collections;
using UnityEngine;

public class StandButton : MonoBehaviour, ICacheable<StandButtonData> {

    public GameObject[] interactables;
    private IActionable[] actionables;
    private int numActionables;



    private Vector3 startPosition;
    public Vector3 EndPosition { get; private set; }
    public Vector3 OriginalStartPosition { get; private set; }
    public Vector3 OriginalEndPosition { get; private set; }

    private Coroutine currentCoroutine;
    public Direction MovementDirection { get; private set; }

    private float startTime;
    private float journeyLength;
    private float translationDistance_y = 0.15f;
    private float speed = 1f;

    public float WaitDuration { get; private set; }
    public float OriginalWaitDuration { get; private set; }

    private bool hasInitialised;
    private bool toStartMoving;
    public bool IsMoving { get; private set; }
    private bool hasStartedMovingDown;
    private bool toStartWaiting;
    private bool isWaiting;
    private bool isBeingPressedDown;
    public bool IsDown { get; private set; }

    void Start() {
        numActionables = interactables.Length;
        actionables = new IActionable[numActionables];
        for (int i = 0; i < numActionables; i++) {
            actionables[i] = interactables[i].GetComponent<IActionable>();
        }

        if (!hasInitialised) {
            WaitDuration = 5f;
            OriginalWaitDuration = WaitDuration;

            startPosition = gameObject.transform.position;
            Vector3 vectorDifference = new Vector3(0, translationDistance_y, 0);
            EndPosition = startPosition - vectorDifference;

            OriginalStartPosition = startPosition;
            OriginalEndPosition = EndPosition;

            MovementDirection = Direction.Down;
        }

        journeyLength = Vector3.Distance(startPosition, EndPosition);
    }

    void Update() {
        if (toStartMoving) {
            StartMovement();
            toStartMoving = false;
        }

        if (toStartWaiting) {
            StartWait();
            toStartWaiting = false;
        }

        if (isBeingPressedDown) {
            /* 
             * Refresh the start time to make the stand button wait for
             * the waitDuration even after it's not being pressed
             */
            startTime = Time.time;
        }

        if (IsMovingUp() && toStartMoving) {
            InterruptMovement();
            StartMovement();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (!isWaiting && !hasStartedMovingDown) {
            toStartMoving = true;
            // Only one movement coroutine can be started on the way down
            hasStartedMovingDown = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision) {
        // Only while waiting will the stand button be considered as being pressed
        if (isWaiting) {
            isBeingPressedDown = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        isBeingPressedDown = false;
    }

    private void StartMovement() {
        currentCoroutine = StartCoroutine(Move());
    }

    private void InterruptMovement() {
        StopCoroutine(currentCoroutine);
        ChangeMovementDirection();
        // Set the start position as the current position
        startPosition = transform.position;
        // Set the end position as the original end position
        EndPosition = OriginalEndPosition;
    }

    private void StartWait() {
        currentCoroutine = StartCoroutine(Wait());
    }

    private bool IsMovingUp() {
        return IsMoving && MovementDirection == Direction.Up;
    }

    private IEnumerator Move() {
        float fracJourney = 0;
        IsMoving = true;
        startTime = Time.time;

        // Start moving
        while (fracJourney < 1) {
            if (!PauseLevel.isPaused) {
                float distCovered = (Time.time - startTime) * speed;
                fracJourney = distCovered / journeyLength;
                // Set our position as a fraction of the distance between the markers.
                transform.position = Vector3.Lerp(startPosition, EndPosition, fracJourney);
            } else {
                // Cache the current start position so that the movement can be resumed when unpaused
                startPosition = transform.position;
                // Refresh the start time to get accurate distance covered
                startTime = Time.time;
            }
            yield return null;
        }

        // Stop moving
        IsMoving = false;
        ChangeMovementDirection();
        RefreshStartEndPositions();

        // The stand button has reached the bottom and will move upwards next
        if (MovementDirection == Direction.Up) {
            // The actionable will act when the stand button is at the bottom
            for (int i = 0; i < numActionables; i++) {
                actionables[i].StartAction();
            }
            // Start waiting
            toStartWaiting = true;
            hasStartedMovingDown = false;
        } else {
            for (int i = 0; i < numActionables; i++) {
                actionables[i].EndAction();
            }
        }
    }

    private IEnumerator Wait() {
        float currentWaitDuration = 0;
        bool hasAlreadyMinus = false;
        isWaiting = true;
        IsDown = true;
        startTime = Time.time;

        // Start waiting
        while (currentWaitDuration < WaitDuration) {
            if (!PauseLevel.isPaused) {
                currentWaitDuration = Time.time - startTime;
            } else {
                if (!hasAlreadyMinus) {
                    WaitDuration -= currentWaitDuration;
                    hasAlreadyMinus = true;
                }
                startTime = Time.time;
            }
            yield return null;
        }

        // Reset the wait duration
        WaitDuration = OriginalWaitDuration;

        // Stop waiting and start moving up
        isWaiting = false;
        IsDown = false;
        toStartMoving = true;
    }

    private void RefreshStartEndPositions() {
        if (MovementDirection == Direction.Up) {
            startPosition = OriginalEndPosition;
            EndPosition = OriginalStartPosition;
        } else {
            startPosition = OriginalStartPosition;
            EndPosition = OriginalEndPosition;
        }
    }

    private void ChangeMovementDirection() {
        if (MovementDirection == Direction.Up) {
            MovementDirection = Direction.Down;
        } else {
            MovementDirection = Direction.Up;
        }
    }

    public StandButtonData CacheData() {
        return new StandButtonData(this);
    }

    public void Restore(StandButtonData standButtonData) {
        startPosition = standButtonData.PrevStartPosition;
        EndPosition = standButtonData.PrevEndPosition;
        OriginalStartPosition = standButtonData.OriginalStartPosition;
        OriginalEndPosition = standButtonData.OriginalEndPosition;
        MovementDirection = standButtonData.MovementDirection;
        IsDown = standButtonData.IsDown;
        IsMoving = standButtonData.IsMoving;
        WaitDuration = standButtonData.WaitDuration;
        OriginalWaitDuration = standButtonData.OriginalWaitDuration;
        hasInitialised = true;

        if (IsMoving) {
            toStartMoving = true;
        }

        if (IsDown) {
            isWaiting = true;
            toStartWaiting = true;
            transform.position = OriginalEndPosition;
        }
    }
}
