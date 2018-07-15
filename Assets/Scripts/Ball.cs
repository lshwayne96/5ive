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

    void Start() {
        player = GameObject.FindWithTag("Player");
        playerTf = player.transform;
    }

    void Update() {
        // Enable the ball to follow slightly behind the player when picked up
        if (playerHasBall) {
            Vector3 targetPosition = playerTf.position;
            GetComponent<Rigidbody2D>().velocity = speed * (targetPosition - transform.position);
        }

        // If the player has entered the ball's trigger, the player can pick it up
        if (canPickUpBall) {
            if (Input.GetKeyUp(KeyCode.G) && !playerHasBall) {
                PlayerPicksUpBall();
            }
        }

        // Enable the ball to be dropped anytime
        if (Input.GetKeyUp(KeyCode.H) && playerHasBall) {
            PlayerDropsBall();
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

    // Allow the player to pick up the ball
    private void PlayerPicksUpBall() {
        transform.position = playerTf.position;
        GetComponent<Rigidbody2D>().gravityScale = 0f;
        playerHasBall = true;
    }

    // Allow the player to drop the ball
    private void PlayerDropsBall() {
        GetComponent<Rigidbody2D>().gravityScale = 1f;
        playerHasBall = false;
    }
}
