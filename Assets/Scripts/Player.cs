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

    public Direction Orientation { get; private set; }
    private bool hasRestoredOrientation;

    // LateUpdate() is used to allow Flip() in PlatformerCharacter2D to run first
    void LateUpdate() {
        if ((IsFacingLeftAfterRestore() && Input.GetKeyDown(KeyCode.RightArrow)) ||
             (IsFacingRightAfterRestore() && Input.GetKeyDown(KeyCode.LeftArrow))) {
            ToggleOrientation();
            hasRestoredOrientation = true;
            enabled = false;
        } else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) {
            hasRestoredOrientation = true;
        }

        if (FacingDirection() == Direction.Left) {
            Orientation = Direction.Left;
        }

        if (FacingDirection() == Direction.Right) {
            Orientation = Direction.Right;
        }
    }

    private bool IsFacingLeftAfterRestore() {
        return RestoreLevel.restoreLevel.hasRestoredScene
                           && (FacingDirection() == Direction.Left) && !hasRestoredOrientation;
    }

    private bool IsFacingRightAfterRestore() {
        return RestoreLevel.restoreLevel.hasRestoredScene
                           && (FacingDirection() == Direction.Right) && !hasRestoredOrientation;
    }

    public Direction FacingDirection() {
        if (transform.localScale.x > 0) {
            return Direction.Right;
        } else if (transform.localScale.x < 0) {
            return Direction.Left;
        } else {
            return Direction.Undefined;
        }
    }

    public void ToggleOrientation() {
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }

    public PlayerData CacheData() {
        return new PlayerData(this);
    }

    public void Restore(PlayerData playerData) {
        transform.localScale = playerData.PrevLocalScale;
        GetComponent<Rigidbody2D>().velocity = playerData.PrevVelocity;
        transform.position = playerData.PrevPosition;
        GetComponent<Rigidbody2D>().gravityScale = playerData.PrevGravityScale;
        // Restore player camera
        GetComponent<DetectRoom>().SetCurrentRoom();
    }
}
