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
        if (RestoreLevel.restoreLevel.hasRestoredScene && IsFacingLeft() && !hasRestoredOrientation
            && (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))) {
            Vector3 playerScale = transform.localScale;
            playerScale.x *= -1;
            transform.localScale = playerScale;
            hasRestoredOrientation = true;
        }
    }

    // Checks to see if the player is facing the left
    private bool IsFacingLeft() {
        if (transform.localScale.x < 0) { // Sprite is facing left
            return true;
        } else if (transform.localScale.x > 0) { // Sprite is facing right
            return false;
        } else { // Sprite has no width
            return false;
        }
    }
}
