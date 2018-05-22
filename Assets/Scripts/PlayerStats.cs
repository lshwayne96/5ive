using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
	public static Transform currentRoom; // The room that the player is currently in
	public static Transform currentBallRoom; // The room the ball is currently in
	public static GameObject ball;

	private DetectRoom roomScript;
	private DetectRoom ballRoomScript;


	// Use this for initialization
	void Start() {
		// Gets the current room of the player
		roomScript = GetComponent<DetectRoom>();
		currentRoom = roomScript.currentRoom;

		ball = GameObject.FindWithTag("TeleportationBall");

		// Gets the current room of the ball
		ballRoomScript = ball.GetComponent<DetectRoom>();
		currentBallRoom = ballRoomScript.currentRoom;
	}

	// Update is called once per frame
	void Update() {
		currentRoom = roomScript.currentRoom;
		// Debug.Log(currentRoom.tag);
	}

	private void OnTriggerExit2D(Collider2D collision) {
		currentRoom = roomScript.currentRoom;
		currentBallRoom = ballRoomScript.currentRoom;
	}
}
