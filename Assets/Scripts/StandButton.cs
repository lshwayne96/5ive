using UnityEngine;

public class StandButton : MonoBehaviour {

    public float waitDuration = 3;
    public float translationDistY = 0.15f;

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

    // Movement speed in units/sec.
    public float speed = 1.0F;

    // Use this for initialization
    void Start() {
        startPosition = gameObject.transform.position;
        endPosition = startPosition - new Vector3(0, translationDistY, 0);
        journeyLength = Vector3.Distance(startPosition, endPosition);
        movementDirection = Direction.Down;
        isAtRest = true;
    }

    // Follows the target position like with a spring
    void Update() {
        // Move
        if (move) {
            Move();
        }

        // While moving and reaching the end
        if (move && !isAtRest) {
            if (HasReachedEnd(movementDirection)) {
                Debug.Log("Reached end");
                // Stop moving
                move = false;
                // Reached the final position
                isAtRest = true;

                if (movementDirection == Direction.Down) {
                    // Start waiting
                    wait = true;
                    // Get the time at which the waiting begins
                    startTime = Time.time;
                } else {
                    Debug.Log(move);
                    Debug.Log(wait);
                    Debug.Log(isAtRest);
                    Debug.Log(isBeingPressed);
                }
            }
        }

        // Finished moving and now start waiting
        if (wait) {
            if (IsWaitOver() && !isBeingPressed) {
                Debug.Log("Done waiting");
                move = true;
                isAtRest = false;
                wait = false;

                Vector3 temp = startPosition;
                startPosition = endPosition;
                endPosition = temp;
                ChangeMovementDirection();

                Move();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("First contact");
        if (!move && isAtRest && !wait) {
            startTime = Time.time;
            move = true;
            isAtRest = false;
            isBeingPressed = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision) {
        Debug.Log("Is being pressed");
        isBeingPressed = true;
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (isAtRest) {
            isBeingPressed = false;
        }
    }

    private bool HasReachedEnd(Direction direction) {
        if (direction == Direction.Down) {
            return gameObject.transform.position.y <= endPosition.y;
        } else {
            return gameObject.transform.position.y >= endPosition.y;
        }
    }

    private bool IsWaitOver() {
        return Time.time - firstTime >= waitDuration;
    }

    private void Move() {
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
}

enum Direction { Up, Down };
