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
    private Ladder ladder;

    private BoxCollider2D ladderBoxCollider;
    private bool isLadderATrigger;
    private bool hasRestored;

    void Start() {
        ladder = GetComponentInParent<Ladder>();
        ladderBoxCollider = ladder.GetComponent<BoxCollider2D>();
        if (hasRestored) {
            Debug.Log("Called");
            ladderBoxCollider.isTrigger = isLadderATrigger;
            hasRestored = false;
        }
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
        return ladder.IsOutsideLadder() && isNearTop;
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

    public void Restore(bool isNearTop, bool isLadderATrigger) {
        this.isNearTop = isNearTop;
        this.isLadderATrigger = isLadderATrigger;
        //hasRestored = true;
    }

    public TopOfLadderData CacheData() {
        return new TopOfLadderData(isNearTop, ladderBoxCollider.isTrigger);
    }
}
