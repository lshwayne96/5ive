using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderInteraction : MonoBehaviour
{
	private bool canClimb;
	private float CLIMBING_SPEED;
	public static float originalGravityScale;
	public GameObject player;

	// Use this for initialization
	void Start()
	{
		CLIMBING_SPEED = 3;
	}

	// Update is called once per frame
	void Update()
	{
		if (canClimb)
		{
			if (Input.GetKey(KeyCode.UpArrow))
			{
				player.transform.Translate(Vector3.up * CLIMBING_SPEED * Time.deltaTime);
				Debug.Log("Climbing up");
			}
			if (Input.GetKey(KeyCode.DownArrow))
			{
				player.transform.Translate(Vector3.down);
				Debug.Log("Climbing down");
			}
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{

		if (collision.gameObject.CompareTag("Player"))
		{
			canClimb = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			canClimb = false;
		}
	}
}
