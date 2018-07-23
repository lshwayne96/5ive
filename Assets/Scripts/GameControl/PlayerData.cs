/*
 * This class represents a player and the player camera.
 * It is used to restore a player to its saved position
 * and velocity in the saved game.
 * It is also used to reposition the player camera
 * to the correct room.
 */

using System;
using UnityEngine;

[Serializable]
public class PlayerData {

    // Vector2 velocity
    private float vX;
    private float vY;

    // Vector3 position
    private float pX;
    private float pY;

    private float sX;
    private float sY;
    private float sZ;

    public PlayerData(Vector2 velocity, Vector3 position, Vector3 localScale) {
        this.vX = velocity.x;
        this.vY = velocity.y;

        this.pX = position.x;
        this.pY = position.y;

        this.sX = localScale.x;
        this.sY = localScale.y;
    }

    // Reconstruct the velocity
    private Vector2 GetVelocity() {
        return new Vector2(vX, vY);
    }

    // Reconstruct the position
    private Vector3 GetPosition() {
        return new Vector3(pX, pY);
    }

    private Vector3 GetLocalScale() {
        return new Vector3(sX, sY);
    }

    public void Restore() {
        // Find the player in the new scene
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<Rigidbody2D>().velocity = GetVelocity();
        player.transform.position = GetPosition();
        player.transform.localScale = GetLocalScale();

        // Restore player camera
        player.GetComponent<DetectRoom>().SetCurrentRoom();
    }
}
