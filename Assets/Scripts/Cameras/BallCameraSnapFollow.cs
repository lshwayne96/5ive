/*
 * The script attached to the camera pointing at the ball.
 * The camera knows where the ball is by using the currentBallRoom
 * from the SetCurrentRoom script.
 */

using UnityEngine;

public class BallCameraSnapFollow : MonoBehaviour {

    private Transform playerCamera;

    void Update() {

        playerCamera = GameObject.FindWithTag("MainCamera").transform;
        Vector3 offset = SetCurrentRoom.currentPlayerRoom.position - playerCamera.position;

        gameObject.transform.position =
            new Vector3(SetCurrentRoom.currentBallRoom.position.x - offset.x,
                        SetCurrentRoom.currentBallRoom.position.y - offset.y,
                        -10f);
    }
}
