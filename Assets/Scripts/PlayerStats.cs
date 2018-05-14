using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
    public static Transform room;
    public LayerMask roomLayer;

	// Use this for initialization
	void Start () {
        room = Physics2D.OverlapPoint(transform.position, roomLayer).transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
        //room = collision.transform;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
        Collider2D roomCollider = Physics2D.OverlapPoint(transform.position, roomLayer);
        if (roomCollider) {
            room = roomCollider.transform;
        }
	}
}
