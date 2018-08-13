/*
 * This script encapsulates the functionality of a lever.
 * It requires the gameObject of interest to be dragged to interactable.
 */

using System.Collections;
using UnityEngine;

public class Lever : MonoBehaviour {

    public GameObject interactable;

    private Quaternion startRotation;
    private Quaternion endRotation;
    private Quaternion prevRotation;
    private Quaternion prevEndRotation;
    private Quaternion originalStartRotation;
    private Quaternion originalEndRotation;

    private Vector3 currentAngle;
    private Vector3 targetAngle;
    private float angleOfRotation;

    private Coroutine currentCoroutine;
    private Direction movementDirection;

    private bool interactableState;
    private bool canPullLever;
    private bool toResume;
    private bool hasSwitchedRotation;
    private bool isRotating;
    private bool hasFinishedRotating;
    private bool wasInterruptedWhileMoving;

    private float startTime;
    private float journeyLength;
    private float speed;

    /*
     * Awake() is used instead of Start() to allow the lever
     * to be rotated by Restore() in InteractablesData
     * to its previous state when a new game is loaded
     * */
    void Awake() {
        /*
         * It is possible that a number of levers control
         * a gameObject in unison. In that case, no gameObject
         * will be attached as an interactable.
         * The InteractionManager will manage their collective
         * interaction.
         */
        if (interactable != null) {
            interactableState = interactable.activeSelf;
        }
    }

    void Start() {
        angleOfRotation = 90f;

        Vector3 angleDifference = Vector3.back * angleOfRotation;
        currentAngle = transform.eulerAngles;
        // The angle to rotate by
        targetAngle = transform.eulerAngles + angleDifference;

        startRotation = transform.rotation;
        Vector3 eulerAngles = transform.eulerAngles;
        endRotation = Quaternion.Euler(eulerAngles + angleDifference);

        originalStartRotation = startRotation;
        originalEndRotation = endRotation;

        // Default movement direction initially since all levers start tilting to the left
        movementDirection = Direction.Right;
        speed = 300f;
        journeyLength = Vector3.Distance(targetAngle, currentAngle);
    }

    /* 
     * If the player is within the collider boundaries of the lever
     * and the R key is pressed, the lever will rotate and
     * and the interactable gameObject will disappear.
    */
    void Update() {
        if (LeverIsPulled()) {
            // Start time for the rotation coroutine
            startTime = Time.time;
            if (!isRotating) { // The lever is not already rotating
                isRotating = true;
                StartRotation();
            } else { // The lever is already rotating
                InterruptRotation();

                // Set the start rotation as the current rotation
                startRotation = transform.rotation;
                SetEndRotation();

                StartRotation();
            }
        }

        // Resuming from a saved game
        if (toResume) {
            isRotating = true;
            StartRotation();
        }
    }

    private void StartRotation() {
        currentCoroutine = StartCoroutine(Rotate());
    }

    private bool LeverIsPulled() {
        return canPullLever && Input.GetKeyUp(KeyCode.R) && !PauseLevel.IsLevelPaused();
    }

    private void InterruptRotation() {
        // Set the lever to rotate in the opposite direction
        ChangeMovementDirection();
        StopCoroutine(currentCoroutine);
        wasInterruptedWhileMoving = true;
    }

    private void SetEndRotation() {
        // Set the end rotation, depending on the movement direction
        if (movementDirection == Direction.Left) {
            endRotation = originalEndRotation;
        } else {
            endRotation = originalStartRotation;
        }
    }

    // When the player enters the lever
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            canPullLever = true;
        }
    }

    // When the player leaves the lever
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            canPullLever = false;
        }
    }

    private IEnumerator Rotate() {
        float distCovered;
        float fracJourney = 0;
        Quaternion start;
        Quaternion end;

        if (toResume) {
            start = prevRotation;
            end = prevEndRotation;
        } else {
            start = startRotation;
            end = endRotation;
        }

        while (fracJourney < 1) {
            if (!PauseLevel.IsLevelPaused()) {
                // Distance moved = time * speed.
                distCovered = (Time.time - startTime) * speed;

                // Fraction of journey completed = current distance divided by total distance.
                fracJourney = distCovered / journeyLength;

                // Set our position as a fraction of the distance between the markers.
                transform.rotation = Quaternion.Slerp(start, end, fracJourney);
                start = transform.rotation;
            } else { // The game is paused
                // Refresh the start time to get accurate distance covered
                startTime = Time.time;
            }
            yield return null;
        }

        ChangeInteractableState();

        if (toResume) {
            toResume = false;
        }

        Quaternion temp = startRotation;
        startRotation = endRotation;
        endRotation = temp;

        ChangeMovementDirection();

        hasSwitchedRotation = !hasSwitchedRotation;
        isRotating = false;

        if (wasInterruptedWhileMoving && !hasSwitchedRotation) {
            startRotation = originalStartRotation;
            endRotation = originalEndRotation;
            wasInterruptedWhileMoving = false;
        }
    }

    // Allow the rotation to resume
    public void ResumeRotation(Quaternion prevRotation, Quaternion prevEndRotation) {
        toResume = true;
        startTime = Time.time;
        this.prevRotation = prevRotation;
        this.prevEndRotation = prevEndRotation;
    }

    public void SwitchRotation() {
        transform.rotation = endRotation;
        ChangeInteractableState();
        hasSwitchedRotation = true;
    }

    // Controls the interactable assigned to the lever
    private void ChangeInteractableState() {
        if (interactable) {
            interactable.SetActive(!interactableState);
            interactableState = !interactableState;
        }
    }

    public bool HasSwitchedRotation() {
        return hasSwitchedRotation;
    }

    private void ChangeMovementDirection() {
        if (movementDirection == Direction.Right) {
            movementDirection = Direction.Left;
        } else {
            movementDirection = Direction.Right;
        }
    }

    public LeverData CacheData() {
        return new LeverData(transform.rotation, endRotation, hasSwitchedRotation, isRotating);
    }

}

