/*
 * This script allows both the player and ball
 * to teleport to each other's rooms
 * while maintaining their relative position in the room.
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleportation : MonoBehaviour {

    private GameObject mainCamera;
    private MeshRenderer preview;
    private Ball ball;

    private DetectRoom playerDetectRoom;
    private DetectRoom ballDetectRoom;

    private float startTime;
    private float previewDuration = 3f;
    private bool previewHasExpired;

    // Whether the scene allow teleportation freely
    private bool isInAllowedScene;

    /* Whether the locations that the player and ball are in
     * allow teleportation freely given that the scene does not
     */
    private bool areInAllowedLocations;

    void Start() {
        mainCamera = GameObject.FindWithTag("MainCamera");
        preview = mainCamera.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();
        ball = GameObject.FindGameObjectWithTag("TeleportationBall").GetComponent<Ball>();

        playerDetectRoom = GetComponent<DetectRoom>();
        ballDetectRoom = ball.GetComponent<DetectRoom>();

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
        bool isPlayerInSpecialRoom = playerDetectRoom.CurrentRoomTf.CompareTag("SpecialRoom");
        bool isBallInSpecialRoom = ballDetectRoom.CurrentRoomTf.CompareTag("SpecialRoom");
        if (isPlayerInSpecialRoom && isBallInSpecialRoom) {
            areInAllowedLocations = true;
        } else {
            areInAllowedLocations = false;
        }
    }

    private void Teleport() {
        // Swap the Player's and ball's rooms
        Vector3 positionDifference = SetCurrentRoom.currentBallRoomTf.position - SetCurrentRoom.currentPlayerRoomTf.position;

        // Offset of player from centre of room
        Vector3 playerOffset = transform.position - SetCurrentRoom.currentPlayerRoomTf.position;

        // Offset of ball from centre of room
        Vector3 ballOffset = SetCurrentRoom.ball.transform.position - SetCurrentRoom.currentBallRoomTf.position;

        BoxCollider2D playerRoomCollider = SetCurrentRoom.currentPlayerRoomTf.GetComponent<BoxCollider2D>();
        BoxCollider2D ballRoomCollider = SetCurrentRoom.currentBallRoomTf.GetComponent<BoxCollider2D>();

        // Difference in scale of 2 rooms
        float scaleFactor_x = playerRoomCollider.size.x / ballRoomCollider.size.x;
        float scaleFactor_y = playerRoomCollider.size.y / ballRoomCollider.size.y;

        transform.position += positionDifference
            + Vector3.Scale(playerOffset, new Vector3(1 / scaleFactor_x - 1, 1 / scaleFactor_y - 1, 0));

        SetCurrentRoom.ball.transform.position -= positionDifference
            - Vector3.Scale(ballOffset, new Vector3(scaleFactor_x - 1, scaleFactor_y - 1, 0));
    }

    private bool IsInAllowedScene() {
        int sceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        return !(sceneBuildIndex == (int)Level.Denial
                 || sceneBuildIndex == (int)Level.Anger);
    }

    private bool CanTeleport() {
        if (PauseLevel.IsPaused) {
            return false;
        }
        return isInAllowedScene || areInAllowedLocations;
    }
}
