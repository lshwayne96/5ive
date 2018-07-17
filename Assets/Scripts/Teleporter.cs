using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

    public bool isUsingCoordinates;
    public float x;
    public float y;

    private Transform teleportPosition;

	// Use this for initialization
	void Start () {
        teleportPosition = transform.GetChild(0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D (Collider2D collision) {
        if (isUsingCoordinates) {
            collision.transform.position = new Vector3(x, y, 0f);
        } else {
            collision.transform.position = teleportPosition.position;
        }
    }
}
