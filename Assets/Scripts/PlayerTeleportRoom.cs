using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleportRoom : MonoBehaviour
{
	// Transform room;
	public GameObject ball;


	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyUp(KeyCode.T))
		{
			Teleport();
		}
	}

	private void Teleport()
	{
		float playerPositionX = transform.position.x;
		float ballPositionX = ball.transform.position.x;
		float difference = playerPositionX - ballPositionX;

		/*
         * Translate the transform of both the player and ball
         * along the x-axis by their difference in x.
         */
		transform.Translate(new Vector3(-difference, 0, 0));
		ball.transform.Translate(new Vector3(difference, 0, 0));

		// targetRoom = PlayerStats.room;
		Debug.Log("Teleported");
	}
}
