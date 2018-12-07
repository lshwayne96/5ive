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

    public Vector3 PrevLocalScale {
        get { return prevLocalScale; }
    }
    [NonSerialized]
    private Vector3 prevLocalScale;
    private float sX;
    private float sY;
    private float sZ;

    public Vector2 PrevVelocity {
        get { return prevVelocity; }
    }
    [NonSerialized]
    private Vector2 prevVelocity;
    private float vX;
    private float vY;

    public Vector2 PrevPosition {
        get { return position; }
    }
    [NonSerialized]
    private Vector3 position;
    private float pX;
    private float pY;

    public float PrevGravityScale { get; private set; }

    public PlayerData(Player player) {
        prevLocalScale = player.transform.localScale;
        Rigidbody2D rigidbody2D = player.GetComponent<Rigidbody2D>();
        prevVelocity = rigidbody2D.velocity;
        position = rigidbody2D.position;

        sX = prevLocalScale.x;
        sY = prevLocalScale.y;
        sZ = prevLocalScale.z;

        vX = prevVelocity.x;
        vY = prevVelocity.y;

        pX = position.x;
        pY = position.y;

        PrevGravityScale = rigidbody2D.gravityScale;
    }

    public void Restore(Player player) {
        prevLocalScale = new Vector3(sX, sY, sZ);
        prevVelocity = new Vector2(vX, vY);
        position = new Vector3(pX, pY);
        player.Restore(this);
    }
}
