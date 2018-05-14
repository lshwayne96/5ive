using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleportRoom : MonoBehaviour {
    public Transform targetRoom;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.T)) {
            transform.position += targetRoom.position - PlayerStats.room.position;
            //targetRoom = PlayerStats.room;
        }
	}
}
