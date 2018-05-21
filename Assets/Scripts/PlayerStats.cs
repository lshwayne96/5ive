using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	// The room that the current player is in
	public static Transform currentRoom;
    private DetectRoom roomScript;

    public static GameObject ball;

    public static Transform ballRoom;
    private DetectRoom ballRoomScript;
    
	// Use this for initialization
	void Start()
	{
        ball = GameObject.FindWithTag("TeleportationBall");

        roomScript = GetComponent<DetectRoom>();
        currentRoom = roomScript.currentRoom;

        ballRoomScript = ball.GetComponent<DetectRoom>();
        ballRoom = ballRoomScript.currentRoom;
    }


	// Update is called once per frame
	void Update()
	{
        currentRoom = roomScript.currentRoom;

        ballRoom = ballRoomScript.currentRoom;
    }
}
