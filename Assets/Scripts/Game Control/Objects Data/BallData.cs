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

    public Vector2 PrevVelocity {
        get { return prevVelocity; }
    }
    [NonSerialized]
    private Vector2 prevVelocity;
    private float vX;
    private float vY;

    public Vector3 PrevPosition {
        get { return prevPosition; }
    }
    [NonSerialized]
    private Vector3 prevPosition;
    private float pX;
    private float pY;
    private float pZ;

    public bool PlayerHasBall { get; private set; }
    public bool PlayerIsWithinRange { get; private set; }

    public BallData(Ball ball) {
        prevVelocity = ball.GetComponent<Rigidbody2D>().velocity;
        vX = prevVelocity.x;
        vY = prevVelocity.y;

        prevPosition = ball.transform.position;
        pX = prevPosition.x;
        pY = prevPosition.y;
        pZ = prevPosition.z;

        PlayerHasBall = ball.PlayerHasBall;
        PlayerIsWithinRange = ball.PlayerIsWithinRange;
    }

    public void Restore(Ball ball) {
        prevVelocity = new Vector2(vX, vY);
        prevPosition = new Vector3(pX, pY, pZ);
        ball.Restore(this);
    }
}