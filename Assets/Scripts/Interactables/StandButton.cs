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

public class StandButton : MonoBehaviour {

    public GameObject interactable;
    public float waitDuration;
    public float translationDistY;
    public float speed;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private Vector3 originalStartPosition;
    private Vector3 originalEndPosition;

    private Booster booster;
    private Coroutine currentCoroutine;
    private Direction movementDirection;

    private float startTime;
    private float journeyLength;
    private float originalWaitDuration;

    private bool hasInitialised;
    private bool toStartMoving;
    private bool isMoving;
    private bool hasStartedMovingDown;
    private bool toStartWaiting;
    private bool isWaiting;
    private bool isBeingPressedDown;
    private bool isDown;

    void Start() {
        booster = interactable.GetComponent<Booster>();

        translationDistY = 0.15f;
        speed = 1f;

        if (!hasInitialised) {
            waitDuration = 2f;
            originalWaitDuration = waitDuration;

            startPosition = gameObject.transform.position;
            Vector3 vectorDifference = new Vector3(0, translationDistY, 0);
            endPosition = startPosition - vectorDifference;

            originalStartPosition = startPosition;
            originalEndPosition = endPosition;

            movementDirection = Direction.Down;
        }

        journeyLength = Vector3.Distance(startPosition, endPosition);
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
        endPosition = originalEndPosition;
    }

    private void StartWait() {
        currentCoroutine = StartCoroutine(Wait());
    }

    private bool IsMovingUp() {
        return isMoving && movementDirection == Direction.Up;
    }

    private IEnumerator Move() {
        float fracJourney = 0;
        isMoving = true;
        startTime = Time.time;

        // Start moving
        while (fracJourney < 1) {
            if (!PauseLevel.isPaused) {
                float distCovered = (Time.time - startTime) * speed;
                fracJourney = distCovered / journeyLength;
                // Set our position as a fraction of the distance between the markers.
                transform.position = Vector3.Lerp(startPosition, endPosition, fracJourney);
            } else {
                // Cache the current start position so that the movement can be resumed when unpaused
                startPosition = transform.position;
                // Refresh the start time to get accurate distance covered
                startTime = Time.time;
            }
            yield return null;
        }

        // Stop moving
        isMoving = false;
        ChangeMovementDirection();
        RefreshStartEndPositions();

        // The stand button has reached the bottom and will move upwards next
        if (movementDirection == Direction.Up) {
            // The booster will fire when the stand button is at the bottom
            booster.Fire();
            // Start waiting
            toStartWaiting = true;
            hasStartedMovingDown = false;
        }
    }

    private IEnumerator Wait() {
        float currentWaitDuration = 0;
        bool hasAlreadyMinus = false;
        isWaiting = true;
        isDown = true;
        startTime = Time.time;

        // Start waiting
        while (currentWaitDuration < waitDuration) {
            if (!PauseLevel.isPaused) {
                currentWaitDuration = Time.time - startTime;
            } else {
                if (!hasAlreadyMinus) {
                    waitDuration -= currentWaitDuration;
                    hasAlreadyMinus = true;
                }
                startTime = Time.time;
            }
            yield return null;
        }

        // Reset the wait duration
        waitDuration = originalWaitDuration;

        // Stop waiting and start moving up
        isWaiting = false;
        isDown = false;
        toStartMoving = true;
    }

    private void RefreshStartEndPositions() {
        if (movementDirection == Direction.Up) {
            startPosition = originalEndPosition;
            endPosition = originalStartPosition;
        } else {
            startPosition = originalStartPosition;
            endPosition = originalEndPosition;
        }
    }

    private void ChangeMovementDirection() {
        if (movementDirection == Direction.Up) {
            movementDirection = Direction.Down;
        } else {
            movementDirection = Direction.Up;
        }
    }

    public StandButtonData CacheData() {
        return new StandButtonData(transform.position, endPosition,
                                   originalStartPosition, originalEndPosition,
                                   movementDirection, isDown, isMoving,
                                   waitDuration, originalWaitDuration);
    }

    public void Restore(Vector3 startPosition, Vector3 endPosition,
                        Vector3 originalStartPosition, Vector3 originalEndPosition,
                       Direction movementDirection, bool isDown, bool isMoving,
                        float waitDuration, float originalWaitDuration) {
        this.startPosition = startPosition;
        this.endPosition = endPosition;
        this.originalStartPosition = originalStartPosition;
        this.originalEndPosition = originalEndPosition;
        this.movementDirection = movementDirection;
        this.isDown = isDown;
        this.isMoving = isMoving;
        this.waitDuration = waitDuration;
        this.originalWaitDuration = originalWaitDuration;
        hasInitialised = true;

        if (isMoving) {
            toStartMoving = true;
        }

        if (isDown) {
            isWaiting = true;
            toStartWaiting = true;
            transform.position = originalEndPosition;
        }
    }
}
