using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour {

    private Vector3 moveDirection;
    public float moveSpeed = 3f;

	// Use this for initialization
	void Start () {
        moveDirection = Vector3.right;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += moveDirection * Time.deltaTime * moveSpeed;
	}

    void OnCollisionEnter2D (Collision2D collision) {
        moveDirection = Vector3.Scale(moveDirection, new Vector3(-1,0,0));
    }
}
