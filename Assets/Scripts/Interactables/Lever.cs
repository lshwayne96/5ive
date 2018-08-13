/*
 * This script encapsulates the functionality of a lever.
 * It requires the gameObject of interest to be dragged to interactable.
 */

using System.Collections;
using UnityEngine;

public class Lever : MonoBehaviour {

    public GameObject interactable;
    public float speed;

    private Quaternion startRotation;
    private Quaternion endRotation;
    private Quaternion originalStartRotation;
    private Quaternion originalEndRotation;

    private Coroutine currentCoroutine;
    private Direction movementDirection;

    public bool hasSwitchedRotation { get; private set; }
    private bool interactableState;
    private bool canPullLever;
<<<<<<< HEAD
    private bool toResume;
    private bool hasSwitchedRotation;
=======
    private bool toResumeRotation;

>>>>>>> 3ae47c30b94e45541d67e8b8ee46f01a173c2acb
    private bool isRotating;
    private bool hasFinishedRotating;
    private bool hasInitialised;

    private float angleOfRotation;
    private float startTime;
    private float journeyLength;


    void Start() {
        angleOfRotation = 90f;
        Vector3 angleDifference = Vector3.back * angleOfRotation;
        Vector3 currentAngle = transform.eulerAngles;
        Vector3 targetAngle = transform.eulerAngles + angleDifference;

        /*
         * The variables here are stored in the lever data
         * so no need to initialise them again
         * Initialising them again also overwrites the previous lever state
         */
        if (!hasInitialised) {
            startRotation = transform.rotation;
            Vector3 eulerAngles = transform.eulerAngles;
            endRotation = Quaternion.Euler(eulerAngles + angleDifference);

            originalStartRotation = startRotation;
            originalEndRotation = endRotation;

            // Default movement direction initially since all levers start tilting to the left
            movementDirection = Direction.Right;
        }

        speed = 300f;
        journeyLength = Vector3.Distance(targetAngle, currentAngle);

        /*
         * It is possible that a number of levers control
         * a gameObject in unison. In that case, no gameObject
         * will be attached as an interactable.
         * The InteractionManager will manage their collective
         * interaction.
         */
<<<<<<< HEAD
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
=======
        if (interactable) {
            interactableState = interactable.activeSelf;
        }
>>>>>>> 3ae47c30b94e45541d67e8b8ee46f01a173c2acb
    }

    /* 
     * If the player is within the collider boundaries of the lever
     * and the R key is pressed, the lever will rotate and
     * and the interactable gameObject will disappear.
    */
<<<<<<< HEAD
    void Update() {
=======
    void Update() {
>>>>>>> 3ae47c30b94e45541d67e8b8ee46f01a173c2acb
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
<<<<<<< HEAD

=======
>>>>>>> 3ae47c30b94e45541d67e8b8ee46f01a173c2acb
                StartRotation();
            }
        }

<<<<<<< HEAD
        // Resuming from a saved game
        if (toResume) {
            isRotating = true;
=======
        // Resuming rotation from a saved game
        if (toResumeRotation) {
            ResumeRotation();
>>>>>>> 3ae47c30b94e45541d67e8b8ee46f01a173c2acb
            StartRotation();
        }
    }

    private void StartRotation() {
        currentCoroutine = StartCoroutine(Rotate());
    }

    private bool LeverIsPulled() {
<<<<<<< HEAD
        return canPullLever && Input.GetKeyUp(KeyCode.R) && !PauseLevel.IsLevelPaused();
    }

    private void InterruptRotation() {
        // Set the lever to rotate in the opposite direction
        ChangeMovementDirection();
        StopCoroutine(currentCoroutine);
        wasInterruptedWhileMoving = true;
=======
        return canPullLever && Input.GetKeyUp(KeyCode.R) && !PauseLevel.isPaused;
    }

    private void InterruptRotation() {
        StopCoroutine(currentCoroutine);
        // Set the lever to rotate in the opposite direction
        ChangeMovementDirection();
>>>>>>> 3ae47c30b94e45541d67e8b8ee46f01a173c2acb
    }

    private void SetEndRotation() {
        // Set the end rotation, depending on the movement direction
        if (movementDirection == Direction.Left) {
<<<<<<< HEAD
            endRotation = originalEndRotation;
        } else {
            endRotation = originalStartRotation;
=======
            endRotation = originalStartRotation;
        } else {
            endRotation = originalEndRotation;
>>>>>>> 3ae47c30b94e45541d67e8b8ee46f01a173c2acb
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
        float fracJourney = 0;
        // The lever cannot be both rotating and have its rotation switched simultaneously
        hasSwitchedRotation = false;

        while (fracJourney < 1) {
            if (!PauseLevel.isPaused) {
                // Distance moved = time * speed.
                float distCovered = (Time.time - startTime) * speed;

                // Fraction of journey completed = current distance divided by total distance.
                fracJourney = distCovered / journeyLength;

                // Set our position as a fraction of the distance between the markers.
<<<<<<< HEAD
                transform.rotation = Quaternion.Slerp(start, end, fracJourney);
                start = transform.rotation;
            } else { // The game is paused
=======
                transform.rotation = Quaternion.Slerp(startRotation, endRotation, fracJourney);
            } else { // The game is paused
                // Cache the current startRotation so that the rotation can be resumed when unpaused
                startRotation = transform.rotation;
>>>>>>> 3ae47c30b94e45541d67e8b8ee46f01a173c2acb
                // Refresh the start time to get accurate distance covered
                startTime = Time.time;
            }
            yield return null;
        }
<<<<<<< HEAD

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
=======
>>>>>>> 3ae47c30b94e45541d67e8b8ee46f01a173c2acb

        isRotating = false;
        ChangeInteractableState();
        ChangeHasSwitchedRotation();

        ChangeMovementDirection();
        RefreshStartEndRotations();
    }

<<<<<<< HEAD
    public void SwitchRotation() {
        transform.rotation = endRotation;
        ChangeInteractableState();
        hasSwitchedRotation = true;
=======
    private void RefreshStartEndRotations() {
        if (movementDirection == Direction.Left) {
            startRotation = originalEndRotation;
            endRotation = originalStartRotation;
        } else {
            startRotation = originalStartRotation;
            endRotation = originalEndRotation;
        }
>>>>>>> 3ae47c30b94e45541d67e8b8ee46f01a173c2acb
    }

    // Controls the interactable assigned to the lever
    private void ChangeInteractableState() {
<<<<<<< HEAD
        if (interactable) {
            interactable.SetActive(!interactableState);
            interactableState = !interactableState;
        }
    }

    public bool HasSwitchedRotation() {
        return hasSwitchedRotation;
=======
        if (interactable) {
            interactableState = !interactableState;
            interactable.SetActive(interactableState);
        }
>>>>>>> 3ae47c30b94e45541d67e8b8ee46f01a173c2acb
    }

    private void ChangeMovementDirection() {
        if (movementDirection == Direction.Right) {
            movementDirection = Direction.Left;
        } else {
            movementDirection = Direction.Right;
        }
    }

    private void ChangeHasSwitchedRotation() {
        if (movementDirection == Direction.Right) {
            hasSwitchedRotation = true;
        } else {
            hasSwitchedRotation = false;
        }
    }

    public LeverData CacheData() {
        return new LeverData(transform.rotation, endRotation,
                             originalStartRotation, originalEndRotation,
                             movementDirection, hasSwitchedRotation, isRotating);
    }

    public void Restore(Quaternion startRotation, Quaternion endRotation,
                        Quaternion originalStartRotation, Quaternion originalEndRotation,
                        Direction movementDirection, bool hasSwitchedRotation, bool isRotating) {
        this.startRotation = startRotation;
        this.endRotation = endRotation;
        this.originalStartRotation = originalStartRotation;
        this.originalEndRotation = originalEndRotation;
        this.movementDirection = movementDirection;
        this.hasSwitchedRotation = hasSwitchedRotation;
        this.isRotating = isRotating;
        hasInitialised = true;

        if (isRotating) {
            toResumeRotation = true;
        }

        if (hasSwitchedRotation) {
            SwitchRotation();
        }
    }

    private void ResumeRotation() {
        isRotating = true;
        // Refresh the start time for the rotation coroutine
        startTime = Time.time;
        toResumeRotation = false;
    }

    private void SwitchRotation() {
        transform.rotation = startRotation;
        ChangeInteractableState();
    }
}

