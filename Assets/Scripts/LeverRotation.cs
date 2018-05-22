﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverRotation : MonoBehaviour
{
	private bool startRotation;
	private Vector3 currentAngle;
	private Vector3 targetAngle;

	// How much the lever should rotate
	private float ANGLE;

	// Use this for initialization
	void Start()
	{
		ANGLE = 90f;

		// Cache both the currentAngle and targetAngle for convenience
		currentAngle = transform.eulerAngles;
		targetAngle = transform.eulerAngles + Vector3.back * ANGLE;
	}

	// Update is called once per frame
	void Update()
	{
		if (startRotation)
		{
			RotateLever();
		}
	}


	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			if (Input.GetKeyUp(KeyCode.R))
			{
				startRotation = true;

				// Debug.Log("Start rotation");
			}
		}
	}

	private void RotateLever()
	{
		transform.eulerAngles = Vector3.Lerp(currentAngle, targetAngle, 1);
		startRotation = false;

		// Swap the currentAngle and targetAngle
		Vector3 temp;
		temp = currentAngle;
		currentAngle = targetAngle;
		targetAngle = temp;

		// Debug.Log("Rotating");
	}
}