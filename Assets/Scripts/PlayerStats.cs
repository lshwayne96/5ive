using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	// The room that the player is currently in
	public static Transform currentRoom;
	private DetectRoom roomScript;


	private void Awake()
	{

	}

    public static GameObject ball;

    public static Transform ballRoom;
    private DetectRoom ballRoomScript;
    
	// Use this for initialization
	void Start()
	{
<<<<<<< HEAD
		roomScript = GetComponent<DetectRoom>();
		currentRoom = roomScript.currentRoom;
	}
=======
        ball = GameObject.FindWithTag("TeleportationBall");

        roomScript = GetComponent<DetectRoom>();
        currentRoom = roomScript.currentRoom;

        ballRoomScript = ball.GetComponent<DetectRoom>();
        ballRoom = ballRoomScript.currentRoom;
    }
>>>>>>> 8a012df8595a9cef6118512a2ba542328fa86ccc


	// Update is called once per frame
	void Update()
	{
<<<<<<< HEAD
		currentRoom = roomScript.currentRoom;
		// Debug.Log(currentRoom.tag);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{

	}
=======
        currentRoom = roomScript.currentRoom;

        ballRoom = ballRoomScript.currentRoom;
    }
>>>>>>> 8a012df8595a9cef6118512a2ba542328fa86ccc
}
