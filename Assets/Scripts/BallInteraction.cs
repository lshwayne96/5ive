using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallInteraction : MonoBehaviour {
	public Transform player;
	private bool playerHasBall;
	public float speed = 10f;

	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {
		if (playerHasBall) {
			Vector3 targetPosition = player.position;
			GetComponent<Rigidbody2D>().velocity = speed * (targetPosition - transform.position);
		}

		if (Input.GetKeyUp(KeyCode.H) && playerHasBall) {
			PlayerDropsBall();
		}
	}

	private void OnTriggerStay2D(Collider2D collision) {
		if (collision.gameObject.CompareTag("Player")) {
			if (Input.GetKeyUp(KeyCode.G) && !playerHasBall) {
				PlayerPicksUpBall();
			}
		}
	}

	private void PlayerPicksUpBall() {

		// Set the parent of the ball transform to the player transform.
		//transform.parent = player;

		transform.position = player.position;
		//GetComponent<Rigidbody2D>().isKinematic = true;
		GetComponent<Rigidbody2D>().gravityScale = 0f;

		/*
         * Set isTrigger to false to prevent the ball from
         * going through the wall.
         */
		//GetComponent<CircleCollider2D>().isTrigger = false;
		playerHasBall = true;

		// Debug.Log("Ball picked up");
	}

	private void PlayerDropsBall() {
		// To release the ball from the player
		//transform.parent = null;

		// Return the ball to being a trigger.
		// GetComponent<CircleCollider2D>().isTrigger = true;
		// GetComponent<Rigidbody2D>().isKinematic = false;
		GetComponent<Rigidbody2D>().gravityScale = 1f;
		playerHasBall = false;

		// Debug.Log("Ball dropped");
	}
}
