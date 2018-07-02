using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCurrentRoom : MonoBehaviour {
    public static Transform currentPlayerRoom; // The room that the player is currently in
    public static Transform currentBallRoom; // The room the ball is currently in
    public static GameObject ball;

    private DetectRoom playerRoomScript;
    private DetectRoom ballRoomScript;


    // Use this for initialization
    void Start() {
        // Gets the current room of the player
        playerRoomScript = GetComponent<DetectRoom>();
        currentPlayerRoom = playerRoomScript.currentRoom;

        ball = GameObject.FindWithTag("TeleportationBall");

        // Gets the current room of the ball
        ballRoomScript = ball.GetComponent<DetectRoom>();
        currentBallRoom = ballRoomScript.currentRoom;
    }

    // Update is called once per frame
    void Update() {
        currentPlayerRoom = playerRoomScript.currentRoom;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        currentPlayerRoom = playerRoomScript.currentRoom;
        currentBallRoom = ballRoomScript.currentRoom;
    }
}
