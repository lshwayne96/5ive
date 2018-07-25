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

public class StandButton : MonoBehaviour, IMovable {

    public GameObject movable;
    private IMovable movableScript;
    private bool hasMoved;

    private float waitDuration;
    private float translationDistY;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private float startTime;
    private float journeyLength;

    private bool move;
    private bool wait;
    private bool isAtRest;
    private bool isBeingPressed;
    private float firstTime;
    private Direction movementDirection;

    private Vector3 originalStartPosition;
    private Vector3 originalEndPosition;

    // Movement speed in units/sec.
    public float speed = 1.0F;

    void Start() {
        movableScript = movable.GetComponent<IMovable>();

        waitDuration = 3f;
        translationDistY = 0.15f;

        startPosition = originalStartPosition = gameObject.transform.position;
        Vector3 vectorDifference = new Vector3(0, translationDistY, 0);
        endPosition = originalEndPosition = startPosition - vectorDifference;
        journeyLength = Vector3.Distance(startPosition, endPosition);

        isAtRest = true;
        movementDirection = Direction.Down;
    }

    void Update() {
        // Move
        if (move) {
            Move();
        }

        // While moving and reaching the end
        if (move && !isAtRest) {
            // The end position has been reached
            if (HasReachedFinalPosition(movementDirection)) {
                // Stop moving
                move = false;
                isAtRest = true;

                if (movementDirection == Direction.Down) { // Was moving down
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

                startTime = Time.time;
                Move();
            }
        }

        if (isAtRest && movementDirection == Direction.Up) {
            endPosition = originalStartPosition;
            startPosition = originalEndPosition;
        } else if (isAtRest && movementDirection == Direction.Down) {

        }

        if (!hasMoved && isAtRest && movementDirection == Direction.Up) {
            movableScript.Move();
            hasMoved = true;
        } else if (hasMoved && isAtRest && movementDirection == Direction.Down) {
            hasMoved = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (!move && isAtRest && !wait) {
            startTime = Time.time;
            move = true;
            isAtRest = false;
            isBeingPressed = true;
        }

        if (move && !isAtRest && !wait && movementDirection == Direction.Up) {
            isBeingPressed = true;
            Vector3 temp = startPosition;
            startPosition = endPosition;
            endPosition = temp;
            ChangeMovementDirection();
            startPosition = gameObject.transform.position;
        }
    }

    private void OnCollisionStay2D(Collision2D collision) {
        isBeingPressed = true;
    }

    private void OnCollisionExit2D(Collision2D collision) {
        isBeingPressed = false;
    }

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

    public void Move() {
        // Distance moved = time * speed.
        float distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed = current distance divided by total distance.
        float fracJourney = distCovered / journeyLength;

        // Set our position as a fraction of the distance between the markers.
        transform.position = Vector3.Lerp(startPosition, endPosition, fracJourney);
    }

    private void ChangeMovementDirection() {
        if (movementDirection == Direction.Up) {
            movementDirection = Direction.Down;
        } else {
            movementDirection = Direction.Up;
        }
    }

    private bool IsAtOriginalPosition() {
        return isAtRest && movementDirection == Direction.Down;
    }
}

enum Direction { Up, Down };
