/*
 * The script attached to the camera pointing at the ball.
 * The camera knows where the ball is by using the currentBallRoom
 * from the SetCurrentRoom script.
 */

using UnityEngine;

public class BallCameraSnapFollow : MonoBehaviour {

    void Update() {
        gameObject.transform.position =
            new Vector3(SetCurrentRoom.currentBallRoom.position.x,
                        SetCurrentRoom.currentBallRoom.position.y,
                        -10f);
    }
}
