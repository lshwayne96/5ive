/*
 * This script is written in response to the way the PlatformerCharacter2D script works.
 * After loading a saved level, if the character was previously saved as facing the left,
 * moving off would lead to the character moving about in a flipped state, where although
 * the character is moving left, the character would actually be facing right, and vice-versa.
 * 
 * With this script, the scale of the player gameObject is flipped in LateUpdate() to prevent
 * the above behaviour.
 */

using UnityEngine;

public class Player : MonoBehaviour {

    private bool hasRestoredOrientation;

    // LateUpdate() is used to allow Flip() in PlatformerCharacter2D to run first
    void LateUpdate() {
        if (IsFacingLeftAfterRestore()) {
            RestoreOrientation();
            hasRestoredOrientation = true;
        } else if (IsFacingRightAfterRestore()) {
            hasRestoredOrientation = true;
        }
    }

    private bool IsFacingLeftAfterRestore() {
        return RestoreLevel.restoreLevel.hasRestoredScene
                           && (FacingDirection() == Direction.Left) && !hasRestoredOrientation
                           && (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow));
    }

    private bool IsFacingRightAfterRestore() {
        return RestoreLevel.restoreLevel.hasRestoredScene
                           && (FacingDirection() == Direction.Right) && !hasRestoredOrientation
                           && (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow));
    }

    // Checks to see if the player is facing the left
    private Direction FacingDirection() {
        if (transform.localScale.x > 0) {
            return Direction.Right;
        } else if (transform.localScale.x < 0) {
            return Direction.Left;
        } else {
            return Direction.Undefined;
        }
    }

    private void RestoreOrientation() {
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }
}
