﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCameraSnapFollow : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position = new Vector3(PlayerStats.ballRoom.position.x,
                                                    PlayerStats.ballRoom.position.y,
                                                    -10f);
    }
}