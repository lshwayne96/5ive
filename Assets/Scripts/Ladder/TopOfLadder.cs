/*
 * This script is attached to an empty gameObject
 * at the top of a ladder.
 * It checks to see if the player has climbed up the ladder fully,
 * has begun descending it or is simplying standing at the top.
 */

using UnityEngine;

public class TopOfLadder : MonoBehaviour {

    // Whether the player is nearing or at the top
    private bool isNearTop;
    private Ladder ladderScript;
    private BoxCollider2D ladderBoxCollider;

    void Start() {
        ladderScript = GetComponentInParent<Ladder>();
        ladderBoxCollider = ladderScript.GetComponent<BoxCollider2D>();
    }

    void Update() {
        if (AtTopOfLadder()) {
            if (Input.GetKey(KeyCode.DownArrow)) {
                ladderBoxCollider.isTrigger = true;
            } else {
                ladderBoxCollider.isTrigger = false;
            }
        } else {
            ladderBoxCollider.isTrigger = true;
        }
    }

    private bool AtTopOfLadder() {
        return ladderScript.IsOutsideLadder() && isNearTop;
    }

    // When the player is standing or moving nearby the top
    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            isNearTop = true;
        }
    }

    // When the player has begun descending the ladder or walked away from the top
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            isNearTop = false;
        }
    }
}
