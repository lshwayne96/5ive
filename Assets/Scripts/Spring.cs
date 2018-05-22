using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour {
    public float springForce; //multiplier for how high spring will bounce
    public float velocityCap; //maximum velocity spring should allow

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D rbody = collision.GetComponent<Rigidbody2D>();
        float verticalSpeed = Vector2.Dot(rbody.velocity, Vector2.down); //Get downward component of velocity
        if (verticalSpeed > 0) //only bounce if object was falling
        {
            float bounceVelocity = Mathf.Min(verticalSpeed * springForce, velocityCap); //cap the velocity
            rbody.velocity = Vector2.up * bounceVelocity;
        }
    }
}
