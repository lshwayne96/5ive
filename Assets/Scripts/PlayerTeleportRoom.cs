using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleportRoom : MonoBehaviour
{
	// Transform room;
	public GameObject ball;

    private DetectRoom ballRoomScript;
    private Transform ballRoom;

	// Use this for initialization
	void Start()
	{
        ballRoomScript = ball.GetComponent<DetectRoom>();
        ballRoom = ballRoomScript.currentRoom;
	}

	// Update is called once per frame
	void Update()
	{
        ballRoom = ballRoomScript.currentRoom;
        if (Input.GetKeyUp(KeyCode.T))
		{
			Teleport();
		}
	}

	private void Teleport()
	{
        //Swap the Player's and ball's rooms
        transform.position += ballRoom.position - PlayerStats.currentRoom.position;
        ball.transform.position += PlayerStats.currentRoom.position - ballRoom.position;

        /*
		float playerPositionX = transform.position.x;
		float ballPositionX = ball.transform.position.x;
		float difference = playerPositionX - ballPositionX;

		
         * Translate the transform of both the player and ball
         * along the x-axis by their difference in x.
         
		transform.Translate(new Vector3(-difference, 0, 0));
		ball.transform.Translate(new Vector3(difference, 0, 0));
        
		// targetRoom = PlayerStats.room;
		Debug.Log("Teleported");
        */
    }
}
