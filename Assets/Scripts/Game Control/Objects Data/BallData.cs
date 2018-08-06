/*
 * This class represents a ball and the ball camera.
 * It is used to restore a ball and its saved position
 * and velocity in the saved level.
 * It is also used to reposition the ball camera
 * to the correct room.
 * a ball to its saved position and velocity in the saved game.
 */

using System;
using UnityEngine;

[Serializable]
public class BallData {

    // Vector2 velocity
    private float vX;
    private float vY;

    // Vector3 position
    private float pX;
    private float pY;
    private float pZ;

    public BallData(Vector2 velocity, Vector3 position) {
        this.vX = velocity.x;
        this.vY = velocity.y;

        this.pX = position.x;
        this.pY = position.y;
        this.pZ = position.z;
    }

    // Reconstruct the velocity
    private Vector2 GetVelocity() {
        return new Vector2(vX, vY);
    }

    // Reconstruct the position
    private Vector3 GetPosition() {
        return new Vector3(pX, pY, pZ);
    }

    public void Restore() {
        // Find the ball in the new scene
        GameObject ball = GameObject.FindWithTag("TeleportationBall");
        ball.GetComponent<Rigidbody2D>().velocity = GetVelocity();
        ball.transform.position = GetPosition();

        // Restore ball camera
        ball.GetComponent<DetectRoom>().SetCurrentRoom();
    }
}