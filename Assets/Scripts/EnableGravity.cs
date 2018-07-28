using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableGravity : MonoBehaviour {

    private static bool isEnabled = false;

    private Rigidbody2D rbody;

	// Use this for initialization
	void Start () {
        rbody = GetComponent<Rigidbody2D>();
        if (rbody) {
            rbody.gravityScale = 0;
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (isEnabled && rbody)
        {
            rbody.gravityScale = 1;
        }
	}

    private void OnTriggerEnter2D (Collider2D collision)
    {
        isEnabled = true;
    }
}
