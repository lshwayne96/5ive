using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour {

    private float cooldown;
    public bool vertical = true;
    public bool horizontal = false;
    public float verticalKnockback = 8f;
    public float horizontalKnockback = 2500f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        cooldown -= Time.deltaTime;
	}

    void OnTriggerEnter2D (Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            if (cooldown <= 0) {
                if (vertical) {
                    collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, verticalKnockback);
                }
                if (horizontal) {
                    Vector2 dir = (Vector2)collision.transform.position - (Vector2)transform.position;
                    dir = dir.normalized;
                    collision.GetComponent<Rigidbody2D>().AddForce(dir * new Vector2(horizontalKnockback, 0f));
                }
                cooldown = 0f;
            }
        }
    }
}
