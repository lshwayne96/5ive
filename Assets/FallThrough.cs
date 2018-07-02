using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallThrough : MonoBehaviour {

    private BoxCollider2D collider;

	// Use this for initialization
	void Start () {
        collider = GetComponent<BoxCollider2D>();

    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.DownArrow)) {
            collider.isTrigger = true;
        } else {
            collider.isTrigger = false;
        }
	}
}
