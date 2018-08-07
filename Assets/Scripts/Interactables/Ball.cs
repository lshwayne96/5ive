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
            Vector3 targetPosition = playerTf.position;
            GetComponent<Rigidbody2D>().velocity = speed * (targetPosition - transform.position);
        }

        // If the player has entered the ball's trigger, the player can pick it up
        if (canPickUpBall) {
            if (Input.GetKeyDown(KeyCode.G) && !playerHasBall) {
                PickUp();
            }
        }

        // Enable the ball to be dropped anytime
        if (Input.GetKeyDown(KeyCode.H) && playerHasBall) {
            Drop();
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

    private void PickUp() {
        transform.position = playerTf.position;
        GetComponent<Rigidbody2D>().gravityScale = 0f;
        playerHasBall = true;
    }

    private void Drop() {
        GetComponent<Rigidbody2D>().gravityScale = 1f;
        playerHasBall = false;
    }
}
