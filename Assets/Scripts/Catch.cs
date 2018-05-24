using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catch : MonoBehaviour {
	public static bool nearTop;

	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {
		if (Input.GetKey(KeyCode.DownArrow) && LadderInteraction.outsideLadder && nearTop) {
			LadderInteraction.ladderBoxCollider.isTrigger = true;
		} else if (LadderInteraction.outsideLadder && nearTop && nearTop) {
			LadderInteraction.ladderBoxCollider.isTrigger = false;
		}


	}

	private void OnTriggerStay2D(Collider2D collision) {
		nearTop = true;
	}

	private void OnTriggerExit2D(Collider2D collision) {
		nearTop = false;
	}
}
