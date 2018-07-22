using UnityEngine;

public class Crate : MonoBehaviour, IPickable {

    public float distFromPlayer;
    private bool canPickUpCrate;
    private bool crateIsPickedUp;
    private Transform playerTf;

    private void Start() {
        playerTf = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update() {
        // If the player has entered the ball's trigger, the player can pick it up
        if (canPickUpCrate) {
            if (Input.GetKeyDown(KeyCode.G) && !crateIsPickedUp) {
                PickUp();
            }
        }

        // Enable the ball to be dropped anytime
        if (Input.GetKeyDown(KeyCode.H) && crateIsPickedUp) {
            Drop();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            canPickUpCrate = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            canPickUpCrate = false;
        }
    }

    public void PickUp() {
        if (playerTf.position.x < 0) {
            distFromPlayer *= -1;
        }
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        transform.position = playerTf.position + new Vector3(0, distFromPlayer);
        transform.SetParent(playerTf);
        GetComponent<Rigidbody2D>().gravityScale = 0f;
        crateIsPickedUp = true;
    }

    public void Drop() {
        transform.SetParent(null);
        GetComponent<Rigidbody2D>().gravityScale = 1f;
        distFromPlayer *= -1;
        crateIsPickedUp = false;
    }
}