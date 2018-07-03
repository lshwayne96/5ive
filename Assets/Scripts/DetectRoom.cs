using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectRoom : MonoBehaviour {
    public Transform currentRoom; // The room that the current object is in
    public LayerMask roomLayer; // Acts as a trigger

    private void Awake() {
        // Initialise the current room
        currentRoom = Physics2D.OverlapPoint(transform.position, roomLayer).transform;
    }

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerExit2D(Collider2D collision) {
        Collider2D currentRoomCollider = Physics2D.OverlapPoint(transform.position, roomLayer);
        if (currentRoomCollider) {
            // Set the current room of the player to this room.
            currentRoom = currentRoomCollider.transform;
        }
    }
}
