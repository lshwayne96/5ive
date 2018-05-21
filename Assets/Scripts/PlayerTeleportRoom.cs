using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleportRoom : MonoBehaviour
{
    public MeshRenderer preview;

	// Use this for initialization
	void Start()
	{
        
	}

	// Update is called once per frame
	void Update()
	{
        if (Input.GetKeyDown(KeyCode.T))
        {
            preview.enabled = true;
        }
        if (Input.GetKeyUp(KeyCode.T))
		{
            preview.enabled = false;
            Teleport();
		}
	}

	private void Teleport()
	{
        //Swap the Player's and ball's rooms
        transform.position += PlayerStats.ballRoom.position - PlayerStats.currentRoom.position;
        PlayerStats.ball.transform.position += PlayerStats.currentRoom.position - PlayerStats.ballRoom.position;

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
