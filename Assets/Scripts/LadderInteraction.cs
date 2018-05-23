using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderInteraction : MonoBehaviour {
	public GameObject player;
	public float CLIMBING_SPEED;

	private bool canClimb;
	private Rigidbody2D rb;
	private float originalGravityScale;

	// Use this for initialization
	void Start() {
		CLIMBING_SPEED = 6;
		rb = player.GetComponent<Rigidbody2D>();
		originalGravityScale = rb.gravityScale;
	}

	// Update is called once per frame
	void Update() {

	}

	private void FixedUpdate() {
		if (canClimb) {
			rb.gravityScale = originalGravityScale;
			float currentX = rb.velocity.x; // x-component of velocity

			if (Input.GetKey(KeyCode.UpArrow)) { // Moving up
				rb.velocity = new Vector2(currentX, CLIMBING_SPEED);

				// Debug.Log("Climbing up");

			} else if (Input.GetKey(KeyCode.DownArrow)) { // Moving down
				rb.velocity = new Vector2(currentX, -CLIMBING_SPEED);

				// Debug.Log("Climbing down");

			} else { // Stopping on the leader
				rb.velocity = new Vector2(currentX, 0);
				rb.gravityScale = 0;

				// Debug.Log("Stopping");
			}
		}
	}

	private void OnTriggerStay2D(Collider2D collision) {

		if (collision.gameObject.CompareTag("Player")) {
			canClimb = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision) {
		if (collision.gameObject.CompareTag("Player")) {
			canClimb = false;
			rb.velocity = Vector2.zero;
			rb.gravityScale = originalGravityScale; // Revert back so the player doesn't go flying
		}
	}
}
