/*
 * This script enables the attached gameObject,
 * namely a ladder, to interact with the player.
 * The interactions include climbing up and down,
 * and simply walking through it.
 */
using UnityEngine;

public class Ladder : MonoBehaviour {

    // Expose the climbingSpeed variable to the editor
    public float climbingSpeed;

    // Is the player simply passing through the ladder?
    private bool isPassingThrough;
    // Was the player climbing (and has now stopped on it)?
    private bool wasClimbing;
    private bool outsideLadder;
    private bool canClimb;

    private TopOfLadder topOfLadder;
    private GameObject player;

    private Rigidbody2D playerRigidBody;
    private float originalGravityScale;
    private float prevGravityScale;
    private bool hasRestored;

    void Start() {
        // Start the climbing speed to a default 6
        climbingSpeed = 6;

        isPassingThrough = true;
        outsideLadder = true;

        topOfLadder = GetComponentInChildren<TopOfLadder>();
        player = GameObject.FindWithTag("Player");
        playerRigidBody = player.GetComponent<Rigidbody2D>();

        originalGravityScale = playerRigidBody.gravityScale;
        if (hasRestored) {
            playerRigidBody.gravityScale = prevGravityScale;
        }
    }

    // While on the ladder
    private void FixedUpdate() {
        if (canClimb) {
            // Get the x of the velocity since only the y should change
            float currentX = playerRigidBody.velocity.x;
            playerRigidBody.gravityScale = originalGravityScale;
            wasClimbing = true;

            if (Input.GetKey(KeyCode.UpArrow)) { // Moving up
                playerRigidBody.velocity = new Vector2(currentX, climbingSpeed);

            } else if (Input.GetKey(KeyCode.DownArrow)) { // Moving down
                playerRigidBody.velocity = new Vector2(currentX, -climbingSpeed);

            } else { // Stopping in or on the leader
                if (!isPassingThrough) {
                    playerRigidBody.velocity = new Vector2(currentX, 0);
                    // Remove the effects of gravity on the player
                    playerRigidBody.gravityScale = 0;

                } else {
                    wasClimbing = false;
                }
            }
        }
    }

    // When entering the ladder
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            if (Input.GetKey(KeyCode.DownArrow) | Input.GetKey(KeyCode.UpArrow)) {
                canClimb = true;
                outsideLadder = false;
                isPassingThrough = false;
            }
        }
    }

    // When on the ladder
    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            canClimb = true;
            outsideLadder = false;

            if (wasClimbing) {
                isPassingThrough = false;
            }
        }
    }

    // When leaving the ladder
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            canClimb = false;
            outsideLadder = true;
            wasClimbing = false;
            isPassingThrough = true;
            // Revert back so the player doesn't go flying
            playerRigidBody.gravityScale = originalGravityScale;
        }
    }

    public bool IsOutsideLadder() {
        return outsideLadder;
    }

    public LadderData CacheData() {
        return new LadderData(isPassingThrough, wasClimbing, outsideLadder,
                              canClimb, playerRigidBody.gravityScale, topOfLadder);
    }

    public void Restore(bool isPassingThrough, bool wasClimbing, bool outsideLadder,
                        bool canClimb, float currentGravityScale) {
        this.isPassingThrough = isPassingThrough;
        this.wasClimbing = wasClimbing;
        this.outsideLadder = outsideLadder;
        this.canClimb = canClimb;

        hasRestored = true;
        prevGravityScale = currentGravityScale;
    }
}
