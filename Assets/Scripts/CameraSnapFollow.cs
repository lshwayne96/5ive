using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSnapFollow : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		gameObject.transform.position = new Vector3(PlayerStats.currentRoom.position.x,
													PlayerStats.currentRoom.position.y,
													-10f);
	}
}
