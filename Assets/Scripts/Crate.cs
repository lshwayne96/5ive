using UnityEngine;

public class Crate : MonoBehaviour, IPickable {

    public float distXFromPlayer;
    public float distYFromPlayer;

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
        if (playerTf.localScale.x < 0) {
            distXFromPlayer *= -1;
        }
        Destroy(GetComponent<Rigidbody2D>());
        transform.position = playerTf.position + new Vector3(distXFromPlayer, distYFromPlayer);
        transform.SetParent(playerTf);
        crateIsPickedUp = true;
    }

    public void Drop() {
        transform.SetParent(null);
        gameObject.AddComponent<Rigidbody2D>();
        GetComponent<Rigidbody2D>().gravityScale = 1f;
        if (distXFromPlayer < 0) {
            distXFromPlayer *= -1;
        }
        crateIsPickedUp = false;
    }
}