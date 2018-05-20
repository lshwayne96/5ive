using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Functionality **/

/*
 * 1. Picking up of ball.
 * 2. Dropping of ball.
 */

public class BallInteraction : MonoBehaviour
{
	private Transform player;
	private bool playerHasBall;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyUp(KeyCode.H) && playerHasBall)
		{
			PlayerDropsBall();
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			if (Input.GetKeyUp(KeyCode.G) && !playerHasBall)
			{
				PlayerPicksUpBall(collision);
			}
		}
	}

	private void PlayerPicksUpBall(Collider2D collision)
	{
		// Get the transform of the player.
		player = collision.gameObject.transform;

		// Set the parent of the ball transform to the player transform.
		transform.parent = player;

		transform.position = collision.gameObject.transform.position + new Vector3(2, 0, 0);
		GetComponent<Rigidbody2D>().isKinematic = true;

		/*
         * Set isTrigger to false to prevent the ball from
         * going through the wall.
         */
		GetComponent<CircleCollider2D>().isTrigger = false;
		playerHasBall = true;

		// Debug.Log("Ball picked up");
	}

	private void PlayerDropsBall()
	{
		// To release the ball from the player
		transform.parent = null;

		// Return the ball to being a trigger.
		GetComponent<CircleCollider2D>().isTrigger = true;
		playerHasBall = false;

		// Debug.Log("Ball dropped");
	}
}
