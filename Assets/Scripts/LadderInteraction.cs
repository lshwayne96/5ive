using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderInteraction : MonoBehaviour {
	public GameObject player;
	public float CLIMBING_SPEED;

	public static bool canClimb;
	public static bool outsideLadder;
	private Rigidbody2D playerRigidBody;
	public static BoxCollider2D ladderBoxCollider;
	private float originalGravityScale;

	// Use this for initialization
	void Start() {
		CLIMBING_SPEED = 6;
		playerRigidBody = player.GetComponent<Rigidbody2D>();
		ladderBoxCollider = GetComponent<BoxCollider2D>();
		originalGravityScale = playerRigidBody.gravityScale;
	}

	// Update is called once per frame
	void Update() {

	}

	// While on the ladder
	private void FixedUpdate() {
		if (canClimb) {
			playerRigidBody.gravityScale = originalGravityScale;
			float currentX = playerRigidBody.velocity.x; // x-component of velocity

			if (Input.GetKey(KeyCode.UpArrow)) { // Moving up
				playerRigidBody.velocity = new Vector2(currentX, CLIMBING_SPEED);

			} else if (Input.GetKey(KeyCode.DownArrow)) { // Moving down
				playerRigidBody.velocity = new Vector2(currentX, -CLIMBING_SPEED);

			} else { // Stopping on the leader
				playerRigidBody.velocity = new Vector2(currentX, 0);
				playerRigidBody.gravityScale = 0;
			}
		}
	}

	// When entering the ladder
	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.CompareTag("Player")) {
			canClimb = true;
			outsideLadder = false;
		}
	}

	private void OnTriggerExit2D(Collider2D collision) {
		if (collision.gameObject.CompareTag("Player")) {
			canClimb = false;
			playerRigidBody.gravityScale = originalGravityScale; // Revert back so the player doesn't go flying
			outsideLadder = true;
		}
	}
}
