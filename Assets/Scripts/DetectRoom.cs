using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectRoom : MonoBehaviour
{
	// The room that the current object is in
	public Transform currentRoom;

	// Acts as a trigger.
	public LayerMask roomLayer;

	// Use this for initialization
	void Start()
	{
		// Initialise the current room.
		currentRoom = Physics2D.OverlapPoint(transform.position, roomLayer).transform;
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		Collider2D currentRoomCollider = Physics2D.OverlapPoint(transform.position, roomLayer);
		if (currentRoomCollider)
		{
			// Set the current room of the player to this room.
			currentRoom = currentRoomCollider.transform;
		}
	}
}
