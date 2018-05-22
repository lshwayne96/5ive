using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	// The room that the player is currently in
	public static Transform currentRoom;
	private DetectRoom roomScript;


	private void Awake()
	{

	}

	// Use this for initialization
	void Start()
	{
		roomScript = GetComponent<DetectRoom>();
		currentRoom = roomScript.currentRoom;
	}


	// Update is called once per frame
	void Update()
	{
		currentRoom = roomScript.currentRoom;
		// Debug.Log(currentRoom.tag);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{

	}
}
