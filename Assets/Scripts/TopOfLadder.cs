using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopOfLadder : MonoBehaviour {
	private bool nearTop;
	private Ladder ladderScript;
	public static BoxCollider2D ladderBoxCollider;

	// Use this for initialization
	void Start() {
		ladderScript = GetComponentInParent<Ladder>();
		ladderBoxCollider = ladderScript.boxCollider;
	}

	// Update is called once per frame
	void Update() {
		if (AtTopOfLadder()) {
			if (Input.GetKey(KeyCode.DownArrow)) {
				ladderBoxCollider.isTrigger = true;
			} else {
				ladderBoxCollider.isTrigger = false;
			}
		}
	}

	private bool AtTopOfLadder() {
		return ladderScript.outsideLadder && nearTop;
	}

	private void OnTriggerStay2D(Collider2D collision) {
		if (collision.gameObject.CompareTag("Player")) {
			nearTop = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision) {
		if (collision.gameObject.CompareTag("Player")) {
			nearTop = false;
		}
	}
}
