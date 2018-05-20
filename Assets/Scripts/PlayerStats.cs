using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	// The room that the current player is in
	public static Transform currentRoom;

	// Acts as a trigger.
	public LayerMask roomLayer;

	// Use this for initialization
	void Start()
	{
		// Initialise the current room of the player.
		currentRoom = Physics2D.OverlapPoint(transform.position, roomLayer).transform;
	}


	// Update is called once per frame
	void Update()
	{

	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//room = collision.transform;
	}


	// Collider of the current room the player is in.
	Collider2D currentRoomCollider;

	private void OnTriggerExit2D(Collider2D collision)
	{
		currentRoomCollider = Physics2D.OverlapPoint(transform.position, roomLayer);
		if (currentRoomCollider)
		{
			// Set the current room of the player to this room.
			currentRoom = currentRoomCollider.transform;
		}
	}
}
