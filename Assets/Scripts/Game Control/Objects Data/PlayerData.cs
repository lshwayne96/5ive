/*
 * This class represents a player and the player camera.
 * It is used to restore a player to its saved position
 * and velocity in the saved level.
 * It is also used to reposition the player camera
 * to the correct room.
 */

using System;
using UnityEngine;

[Serializable]
public class PlayerData {

    // Vector3 scale
    private float sX;
    private float sY;
    private float sZ;

    // Vector2 velocity
    private float vX;
    private float vY;

    // Vector3 position
    private float pX;
    private float pY;

    private float prevGravityScale;

    public PlayerData(Transform playerTf, Rigidbody2D playerRb) {
        Vector3 scale = playerTf.localScale;
        Vector2 velocity = playerRb.velocity;
        Vector3 position = playerRb.position;

        this.sX = scale.x;
        this.sY = scale.y;
        this.sZ = scale.z;

        this.vX = velocity.x;
        this.vY = velocity.y;

        this.pX = position.x;
        this.pY = position.y;

        this.prevGravityScale = playerRb.gravityScale;
    }

    public void Restore() {
        GameObject player = GameObject.FindWithTag("Player");
        player.transform.localScale = new Vector3(sX, sY, sZ);
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(vX, vY);
        player.transform.position = new Vector3(pX, pY);
        player.GetComponent<Rigidbody2D>().gravityScale = prevGravityScale;

        // Restore player camera
        player.GetComponent<DetectRoom>().SetCurrentRoom();
    }
}
