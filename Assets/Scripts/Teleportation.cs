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
        transform.position += positionDifference;
        SetCurrentRoom.ball.transform.position -= positionDifference;
    }
}
