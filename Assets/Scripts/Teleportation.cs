/*
 * This script allows both the player and ball
 * to teleport to each other's rooms
 * while maintaining their relative position in the room.
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleportation : MonoBehaviour {

    private MeshRenderer preview;
    private GameObject mainCamera;
    private PauseScene pauseScene;

    private float startTime;
    private float previewDuration;
    private bool previewHasExpired;

    // Whether the scene allow teleportation freely
    private bool isInAllowedScene;
    // Whether the location allows teleportation freely given that the scene does not
    private bool isInAllowedLocation;

    void Start() {
        mainCamera = GameObject.FindWithTag("MainCamera");
        preview = mainCamera.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();
        pauseScene = GameObject.FindWithTag("Pause").GetComponent<PauseScene>();

        previewDuration = 3f;
        isInAllowedScene = IsInAllowedScene();
    }

    void Update() {
        if (CanTeleport()) {
            if (Input.GetKeyDown(KeyCode.T)) {
                previewHasExpired = false;
                preview.enabled = true;
                startTime = Time.time;
            }

            if (Time.time - startTime >= previewDuration) {
                previewHasExpired = true;
                preview.enabled = false;
            }

            if (Input.GetKeyUp(KeyCode.T) && !previewHasExpired) {
                preview.enabled = false;
                Teleport();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("SpecialRoom")) {
            isInAllowedLocation = true;
        } else {
            isInAllowedLocation = false;
        }
    }

    private void Teleport() {
        // Swap the Player's and ball's rooms
        Vector3 positionDifference =
            SetCurrentRoom.currentBallRoom.position - SetCurrentRoom.currentPlayerRoom.position;

        // Offset of player from centre of room
        Vector3 playerOffset = transform.position - SetCurrentRoom.currentPlayerRoom.position;

        // Offset of ball from centre of room
        Vector3 ballOffset = SetCurrentRoom.ball.transform.position - SetCurrentRoom.currentBallRoom.position;

        BoxCollider2D playerRoomCollider = SetCurrentRoom.currentPlayerRoom.GetComponent<BoxCollider2D>();
        BoxCollider2D ballRoomCollider = SetCurrentRoom.currentBallRoom.GetComponent<BoxCollider2D>();

        // Difference in scale of 2 rooms
        float scaleFactor_x = playerRoomCollider.size.x / ballRoomCollider.size.x;
        float scaleFactor_y = playerRoomCollider.size.y / ballRoomCollider.size.y;

        transform.position += positionDifference + Vector3.Scale(playerOffset, new Vector3(1 / scaleFactor_x - 1, 1 / scaleFactor_y - 1, 0));
        SetCurrentRoom.ball.transform.position -= positionDifference - Vector3.Scale(ballOffset, new Vector3(scaleFactor_x - 1, scaleFactor_y - 1, 0));
    }

    private bool IsInAllowedScene() {
        int sceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        return !(sceneBuildIndex == (int)Scenes.Denial
                 || sceneBuildIndex == (int)Scenes.Anger);
    }

    private bool CanTeleport() {
        if (!pauseScene.IsScenePaused()) {
            return isInAllowedScene || isInAllowedLocation;
        } else {
            return false;
        }
    }
}

enum Scenes {
    Denial = 1, Anger, Bargaining, Depression, Acceptance
}
