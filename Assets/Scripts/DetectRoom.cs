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
    private Transform currentRoom;
    private Collider2D currentRoomCollider;

    private Transform playerCamera;

    private void Awake() {
        /*
         * Initialise currentRoom here instead of Start() so that
         * dependent scripts like SetCurrentRoom work properly
         */
        UpdateCurrentRoom();
    }

    private void Start() {
        //playerCamera = GameObject.FindWithTag("MainCamera").transform;
    }


    private void OnTriggerExit2D(Collider2D collision) {
        // Update currentRoom
        UpdateCurrentRoom();
    }

    private void UpdateCurrentRoom() {
        // Get collider of current room
        currentRoomCollider = Physics2D.OverlapPoint(transform.position, roomLayer);
        // Update currentRoom
        currentRoom = currentRoomCollider.transform;

        if (CompareTag("Player"))
        {
            BoxCollider2D roomCollider = currentRoom.GetComponent<BoxCollider2D>();
            Bounds roomBounds = new Bounds(new Vector3(currentRoom.position.x, currentRoom.position.y, -10f),
            new Vector3(roomCollider.size.x - 18f,
            roomCollider.size.y - 10f,
            0f));

            playerCamera = GameObject.FindWithTag("MainCamera").transform;

            playerCamera.position = roomBounds.ClosestPoint(playerCamera.position);
        }
    }

    public Transform GetCurrentRoom() {
        return currentRoom;
    }
}
