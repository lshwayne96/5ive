/*
 * This script is used to detect which room
 * the attached gameObject is in.
 * SetCurrentRoom uses the currentRoom variable to set
 * currentPlayerRoom and currentBallRoom.
 */

using UnityEngine;

public class DetectRoom : MonoBehaviour {

    public LayerMask roomLayer;
    /*
     * The room that the current object is in
     * The currentRoom variable is used in the SetCurrentRoom script
     */
    public Transform CurrentRoomTf { get; private set; }
    private Collider2D currentRoomCollider;

    private Transform playerCamera;

    private void Awake() {
        InitialiseRoom();
    }

    private void OnTriggerExit2D(Collider2D collision) {
        MoveToNewRoom();
    }

    private void InitialiseRoom() {
        if (!playerCamera) {
            playerCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        }
        MoveToNewRoom();
    }

    private void MoveToNewRoom() {
        SetCurrentRoom();

        if (CompareTag("Player")) {
            /*
             * The code below is used to ensure that the transition of the camera
             * from one big room to the other is smooth.
             * However, the code works for small rooms as well
             */
            Bounds currentRoomBounds = GetCurrentRoomBounds();
            playerCamera.position = currentRoomBounds.ClosestPoint(playerCamera.position);
        }
    }

    public void SetCurrentRoom() {
        // Get collider of current room
        currentRoomCollider = Physics2D.OverlapPoint(transform.position, roomLayer);
        CurrentRoomTf = currentRoomCollider.transform;
    }

    private Bounds GetCurrentRoomBounds() {
        BoxCollider2D roomBoxCollider = CurrentRoomTf.GetComponent<BoxCollider2D>();

        Vector3 currentRoomCenter = GetRoomCenter();
        Vector3 currentRoomSize = GetRoomSizeSubset(roomBoxCollider);

        return new Bounds(currentRoomCenter, currentRoomSize);
    }

    private Vector3 GetRoomCenter() {
        return new Vector3(CurrentRoomTf.position.x, CurrentRoomTf.position.y, Constants.CAMERA_DEPTH);
    }

    private Vector3 GetRoomSizeSubset(BoxCollider2D roomBoxCollider) {
        return new Vector3(roomBoxCollider.size.x - Constants.CAMERA_LENGTH, roomBoxCollider.size.y - Constants.CAMERA_HEIGHT, 0f);
    }
}
