/*
 * The script attached to the camera pointing at the ball.
 * The camera knows where the ball is by using the currentBallRoomTf
 * from the SetCurrentRoom script.
 */

using UnityEngine;

public class BallCameraSnapFollow : MonoBehaviour {

    private Transform playerCameraTf;
    private GameObject playerGO;
    private GameObject playerCameraGO;
    private Transform player;

    private void Start() {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        playerCameraGO = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update() {
        playerCameraTf = playerCameraGO.transform;

        Vector3 offset = playerCameraTf.position - SetCurrentRoom.currentPlayerRoomTf.position;

        BoxCollider2D playerRoomCollider = SetCurrentRoom.currentPlayerRoomTf.GetComponent<BoxCollider2D>();
        BoxCollider2D ballRoomCollider = SetCurrentRoom.currentBallRoomTf.GetComponent<BoxCollider2D>();

        // Difference in scale of 2 rooms
        float scaleFactor_x = ballRoomCollider.size.x / playerRoomCollider.size.x;
        float scaleFactor_y = ballRoomCollider.size.y / playerRoomCollider.size.y;

        GetComponent<Camera>().orthographicSize = 5 * Mathf.Min(scaleFactor_x, scaleFactor_y);

        if (scaleFactor_x > scaleFactor_y) {
            player = playerGO.transform;
            float playerOffset = (player.position - playerCameraTf.position).x;
            float offSetLimit = (scaleFactor_x / scaleFactor_y / 4f + 0.5f) * 4.5f; //not sure if correct
            if (Mathf.Abs(playerOffset) > offSetLimit) {
                playerOffset = Mathf.Sign(playerOffset) * offSetLimit;
            }
            offset += new Vector3(playerOffset, 0, 0);
        }

        gameObject.transform.position =
            new Vector3(SetCurrentRoom.currentBallRoomTf.position.x + offset.x * scaleFactor_x,
                        SetCurrentRoom.currentBallRoomTf.position.y + offset.y * scaleFactor_y,
                        -10f);
    }
}
