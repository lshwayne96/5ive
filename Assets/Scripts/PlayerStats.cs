using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	// The room that the current player is in
	public static Transform currentRoom;
    private DetectRoom roomScript;

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
	}
}
