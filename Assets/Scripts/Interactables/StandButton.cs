/*
 * This script encapsulates the functionality of a stand button.
 * When a gameObject is on a stand button, the stand button will
 * depress (move towards the ground).
 * 
 * After a certain amount of time (as dictated by waitDuration),
 * the stand button will resurface from the ground.
 * While the stand button is resurfacing, it is still sensitive to
 * collisions and will still still depress if it senses one.
 * 
 * A stand button can be paired with another gameObject with an
 * attached script that implements the IMovable interface.
 * When the stand button is depressed, the corresponding gameObject will
 * execute its Move().
 */

using UnityEngine;

public class StandButton : MonoBehaviour {

    public GameObject interactable;

    private Booster booster;
    private PauseLevel pauseLevel;

    private float waitDuration;
    private float translationDistY;
    private float speed;
    private float startTime;
    private float journeyLength;

    private bool move;
    private bool wait;
    private bool isAtRest;
    private bool isBeingPressed;
    private bool interactableHasMoved;
    private bool toResume;
    private bool wasInterruptedWhileMoving;

    private Direction movementDirection;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private Vector3 prevPosition;
    private Vector3 prevEndPosition;
    private Vector3 originalStartPosition;
    private Vector3 originalEndPosition;

    void Start() {
        booster = interactable.GetComponent<Booster>();
        pauseLevel = GameObject.FindGameObjectWithTag("Pause").GetComponent<PauseLevel>();

        waitDuration = 2f;
        translationDistY = 0.15f;
        speed = 1f;

        startPosition = gameObject.transform.position;
        Vector3 vectorDifference = new Vector3(0, translationDistY, 0);
        endPosition = startPosition - vectorDifference;

        originalStartPosition = startPosition;
        originalEndPosition = endPosition;
        journeyLength = Vector3.Distance(startPosition, endPosition);

        isAtRest = true;
        movementDirection = Direction.Down;
    }

    void Update() {
        if (toResume) {
            move = true;
            isAtRest = false;
            isBeingPressed = true;
            Move();
            toResume = false;
        }

        // Move
        if (move) {
            if (!pauseLevel.IsScenePaused()) {
                Move();
            } else {
                startTime = Time.time;
            }
        }

        // While moving and reaching the end
        if (move && !isAtRest) {
            // The end position has been reached
            if (HasReachedFinalPosition(movementDirection)) {
                // Stop moving
                move = false;
                isAtRest = true;

                if (movementDirection == Direction.Down) { // Was moving down
                    if (wasInterruptedWhileMoving) {
                        startPosition = originalStartPosition;
                        endPosition = originalEndPosition;
                    }

                    // Start waiting
                    wait = true;
                    // Get the time at which the waiting begins
                    startTime = Time.time;
                }

                // Swap the start and end position
                Vector3 temp = startPosition;
                startPosition = endPosition;
                endPosition = temp;
                ChangeMovementDirection();
            }
        }

        // Finished moving and now start waiting
        if (wait) {
            // If the wait is over and the stand button is not being pressed
            if (IsWaitOver() && !isBeingPressed) {
                move = true;
                isAtRest = false;
                wait = false;

                // This startTime is to ensure smooth upward movement of the stand button
                startTime = Time.time;
                // Move upwards
                if (!pauseLevel.IsScenePaused()) {
                    Move();
                } else {
                    startTime = Time.time;
                }
            }
        }

        // Run the booster script when the stand button has just reached bottom
        if (HasJustReachedBottom()) {
            booster.Move();
            if (pauseLevel.IsScenePaused()) {
                startTime = Time.time;
            }
            interactableHasMoved = true;

        } else if (IsAtOriginalPosition()) {
            interactableHasMoved = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        // When contact is made while the stand button is at its original position
        if (IsAtOriginalPosition()) {
            startTime = Time.time;
            move = true;
            isAtRest = false;
            isBeingPressed = true;
        }

        // When contact is made while the stand button is moving towards its original position
        if (IsMovingUp()) {
            isBeingPressed = true;
            wasInterruptedWhileMoving = true;

            Vector3 temp = startPosition;
            startPosition = endPosition;
            endPosition = temp;

            ChangeMovementDirection();

            // Set the startPosition as wherever the stand button is to began the movement up
            startPosition = gameObject.transform.position;
        }
    }

    private void OnCollisionStay2D(Collision2D collision) { isBeingPressed = true; }
    private void OnCollisionExit2D(Collision2D collision) { isBeingPressed = false; }

    private bool IsAtOriginalPosition() {
        return !move && isAtRest && !wait;
    }

    private bool HasJustReachedBottom() {
        return !interactableHasMoved && isAtRest && movementDirection == Direction.Up;
    }

    // Determines if the stand button is at rest
    private bool HasReachedFinalPosition(Direction direction) {
        if (direction == Direction.Down) {
            return gameObject.transform.position.y <= endPosition.y;
        } else {
            return gameObject.transform.position.y >= endPosition.y;
        }
    }

    private bool IsWaitOver() {
        float currentWaitDuration = Time.time - startTime;
        return currentWaitDuration >= waitDuration;
    }

    private bool IsMovingUp() {
        return move && !isAtRest && !wait && movementDirection == Direction.Up;
    }

    public void Resume(Vector3 prevPosition, Vector3 prevEndPosition) {
        toResume = true;
        startTime = Time.time;
        this.prevPosition = prevPosition;
        this.prevEndPosition = prevEndPosition;
    }

    public void Move() {
        Vector3 start;
        Vector3 end;

        if (toResume) {
            start = prevPosition;
            end = prevEndPosition;
        } else {
            start = startPosition;
            end = endPosition;
        }

        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        // Set our position as a fraction of the distance between the markers.
        transform.position = Vector3.Lerp(start, end, fracJourney);
    }

    private void ChangeMovementDirection() {
        if (movementDirection == Direction.Up) {
            movementDirection = Direction.Down;
        } else {
            movementDirection = Direction.Up;
        }
    }

    public StandButtonData CacheData() {
        return new StandButtonData(transform.position, endPosition, move);
    }
}
