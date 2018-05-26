using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour {
	public GameObject player;
	public float climbingSpeed;

	// Used in TopOfLadderScript
	public BoxCollider2D boxCollider;
	public bool outsideLadder;

	private bool canClimb; // Can the player climb?
	private bool isPassingThrough; // Is the player simply passing through the ladder?
	private bool wasClimbing; // Was the player climbing (and has now stopped on it)?
	private float originalGravityScale;
	private Rigidbody2D playerRigidBody;

	// Initialises boxCollider first so that TopOfLadder can reference it properly
	private void Awake() {
		boxCollider = GetComponent<BoxCollider2D>();
	}

	// Use this for initialization
	void Start() {
		climbingSpeed = 6;
		playerRigidBody = player.GetComponent<Rigidbody2D>();
		originalGravityScale = playerRigidBody.gravityScale;
		isPassingThrough = true;
	}

	// Update is called once per frame
	void Update() {

	}

	// While on the ladder
	private void FixedUpdate() {
		if (canClimb) {
			float currentX = playerRigidBody.velocity.x; // x-component of velocity
			playerRigidBody.gravityScale = originalGravityScale;
			wasClimbing = true;

			if (Input.GetKey(KeyCode.UpArrow)) { // Moving up
				playerRigidBody.velocity = new Vector2(currentX, climbingSpeed);

			} else if (Input.GetKey(KeyCode.DownArrow)) { // Moving down
				playerRigidBody.velocity = new Vector2(currentX, -climbingSpeed);

			} else { // Stopping on the leader
				if (!isPassingThrough) {
					playerRigidBody.velocity = new Vector2(currentX, 0);
					playerRigidBody.gravityScale = 0; // Remove the effects of gravity on the player
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
			playerRigidBody.gravityScale = originalGravityScale; // Revert back so the player doesn't go flying
		}
	}
}
