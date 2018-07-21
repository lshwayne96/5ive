/*
 * This script allows both the player and ball
 * to teleport to each other's rooms
 * while maintaining their relative position in the room.
 */

using UnityEngine;

public class Teleportation : MonoBehaviour {

    private MeshRenderer preview;
    private GameObject mainCamera;

    void Start() {
        mainCamera = GameObject.FindWithTag("MainCamera");
        preview = mainCamera.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            preview.enabled = true;
        }
        if (Input.GetKeyUp(KeyCode.T)) {
            preview.enabled = false;
            Teleport();
        }
    }

    private void Teleport() {
        // Swap the Player's and ball's rooms
        Vector3 positionDifference =
            SetCurrentRoom.currentBallRoom.position - SetCurrentRoom.currentPlayerRoom.position;

        //Offset of player from centre of room
        Vector3 playerOffset = transform.position - SetCurrentRoom.currentPlayerRoom.position;

        //Offset of ball from centre of room
        Vector3 ballOffset = SetCurrentRoom.ball.transform.position - SetCurrentRoom.currentBallRoom.position;

        BoxCollider2D playerRoomCollider = SetCurrentRoom.currentPlayerRoom.GetComponent<BoxCollider2D>();
        BoxCollider2D ballRoomCollider = SetCurrentRoom.currentBallRoom.GetComponent<BoxCollider2D>();

        //Difference in scale of 2 rooms
        float scaleFactor_x = playerRoomCollider.size.x / ballRoomCollider.size.x;
        float scaleFactor_y = playerRoomCollider.size.y / ballRoomCollider.size.y;

        transform.position += positionDifference + Vector3.Scale(playerOffset, new Vector3(1/scaleFactor_x - 1, 1/scaleFactor_y - 1, 0));
        SetCurrentRoom.ball.transform.position -= positionDifference - Vector3.Scale(ballOffset, new Vector3(scaleFactor_x - 1, scaleFactor_y - 1, 0));
    }
}
