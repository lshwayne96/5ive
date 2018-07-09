/*
 * This script is attached to the camera pointing at the player.
 * The camera knows where the player is by using the currentPlayerRoom
 * from the SetCurrentRoom script.
 */

using UnityEngine;

public class PlayerCameraSnapFollow : MonoBehaviour {

    void Update() {
        gameObject.transform.position =
            new Vector3(SetCurrentRoom.currentPlayerRoom.position.x,
                        SetCurrentRoom.currentPlayerRoom.position.y,
                        -10f);
    }
}
