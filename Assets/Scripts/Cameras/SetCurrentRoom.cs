/*
 * This script caches the DetectRoom scripts
 * attached to both the player and the ball.
 * It uses GetCurrentRoom() to set the currentPlayerRoom
 * and the currentBallRoom.
 */

using UnityEngine;

public class SetCurrentRoom : MonoBehaviour {

    /*
     * Both currentPlayerRoom and currentBallRoom
     * are used in the Teleportation script.
     * currentPlayerRoom is used in PlayerCamera.
     * currentBallRoom is used in BallCamera.
     */

    // The room that the player is currently in
    public static Transform currentPlayerRoom;
    // The room the ball is currently in
    public static Transform currentBallRoom;
    public static GameObject ball;

    private DetectRoom playerRoomScript;
    private DetectRoom ballRoomScript;

    void Start() {
        // Gets the current room of the player
        playerRoomScript = GetComponent<DetectRoom>();
        currentPlayerRoom = playerRoomScript.GetCurrentRoom();

        ball = GameObject.FindWithTag("TeleportationBall");

        // Gets the current room of the ball
        ballRoomScript = ball.GetComponent<DetectRoom>();
        currentBallRoom = ballRoomScript.GetCurrentRoom();
    }

    void Update() {
        currentPlayerRoom = playerRoomScript.GetCurrentRoom();
    }

    private void OnTriggerExit2D(Collider2D collision) {
        currentPlayerRoom = playerRoomScript.GetCurrentRoom();
        currentBallRoom = ballRoomScript.GetCurrentRoom();
    }
}
