/*
 * This script caches the DetectRoom scripts
 * attached to both the player and the ball.
 * It uses GetCurrentRoom() to set the currentPlayerRoomTf
 * and the currentBallRoomTf.
 */

using UnityEngine;

public class SetCurrentRoom : MonoBehaviour {

    /*
     * Both currentPlayerRoomTf and currentBallRoomTf
     * are used in the Teleportation script.
     * currentPlayerRoomTf is used in PlayerCamera.
     * currentBallRoomTf is used in BallCamera.
     */

    // The room that the player is currently in
    public static Transform currentPlayerRoomTf;
    // The room the ball is currently in
    public static Transform currentBallRoomTf;
    public static GameObject ball;

    private DetectRoom playerRoomScript;
    private DetectRoom ballRoomScript;

    void Start() {
        // Gets the current room of the player
        playerRoomScript = GetComponent<DetectRoom>();
        currentPlayerRoomTf = playerRoomScript.CurrentRoomTf;

        ball = GameObject.FindWithTag("TeleportationBall");

        // Gets the current room of the ball
        ballRoomScript = ball.GetComponent<DetectRoom>();
        currentBallRoomTf = ballRoomScript.CurrentRoomTf;
    }

    void Update() {
        currentPlayerRoomTf = playerRoomScript.CurrentRoomTf;
        currentBallRoomTf = ballRoomScript.CurrentRoomTf;
    }
}
