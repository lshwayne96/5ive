using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectRoom : MonoBehaviour {
    public Transform currentRoom; // The room that the current object is in
    public LayerMask roomLayer; // Acts as a trigger
    private Collider2D currentRoomCollider;

    private void Awake() {
        // Initialise currentRoom
        GetCurrentRoom();
    }

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerExit2D(Collider2D collision) {
        // Update currentRoom
        GetCurrentRoom();

        /*
        if (currentRoomCollider) {
            // Set the current room of the player to this room.
            currentRoom = currentRoomCollider.transform;
        }
        */
    }

    public void GetCurrentRoom() {
        // Get collider of current room
        currentRoomCollider = Physics2D.OverlapPoint(transform.position, roomLayer);

        // Update currentRoom
        currentRoom = currentRoomCollider.transform;
    }
}
