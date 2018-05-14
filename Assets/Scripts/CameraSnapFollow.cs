using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSnapFollow : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(PlayerStats.room.position.x, PlayerStats.room.position.y, -10f);
	}
}
