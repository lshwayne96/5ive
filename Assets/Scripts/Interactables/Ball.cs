/*
 * This script enables interaction with the
 * attached gameObject, namely a ball gameObject.
 * Interactions includes picking and dropping the ball.
 */

using UnityEngine;

public class Ball : MonoBehaviour {

    // Expose the speed variable to the editor
    public float speed = 10f;

    private GameObject player;
    private Transform playerTf;

    private bool playerHasBall;
    private bool canPickUpBall;

    private Vector2 preVelocity;

    void Start() {
        player = GameObject.FindWithTag("Player");
        playerTf = player.transform;
    }

    void Update() {
        // Enable the ball to follow slightly behind the player when picked up
        if (playerHasBall) {
            // Stop the rotation of the ball
            transform.rotation = Quaternion.Euler(Vector3.zero);
            Vector3 targetPosition = playerTf.position;
            GetComponent<Rigidbody2D>().velocity = speed * (targetPosition - transform.position);
        }

        // If the player has entered the ball's trigger, the player can pick it up
<<<<<<< HEAD
        if (canPickUpBall) {
            if (Input.GetKeyDown(KeyCode.G) && !playerHasBall && PauseLevel.IsLevelPaused()) {
                PickUp();
            }
        }

        // Enable the ball to be dropped anytime
        if (Input.GetKeyDown(KeyCode.H) && playerHasBall && PauseLevel.IsLevelPaused()) {
            Drop();
=======
        if (CanPickUpBall()) {
            PickUpBall();
        }

        // Enable the ball to be dropped anytime
        if (CanDropBall()) {
            DropBall();
>>>>>>> 3ae47c30b94e45541d67e8b8ee46f01a173c2acb
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            canPickUpBall = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            canPickUpBall = false;
        }
    }

    private bool CanPickUpBall() {
        return canPickUpBall && Input.GetKeyDown(KeyCode.G) && !playerHasBall && !PauseLevel.isPaused;
    }

    private bool CanDropBall() {
        return Input.GetKeyDown(KeyCode.H) && playerHasBall && !PauseLevel.isPaused;
    }

    private void PickUpBall() {
        transform.position = playerTf.position;
        GetComponent<Rigidbody2D>().gravityScale = 0f;
        playerHasBall = true;
    }

    private void DropBall() {
        GetComponent<Rigidbody2D>().gravityScale = 1f;
        playerHasBall = false;
    }
}
