using System;
using UnityEngine;

[Serializable]
public class PlayerData {
    // Vector2 velocity
    public float vX;
    public float vY;

    // Vector3 position
    public float pX;
    public float pY;
    public float pZ;

    public PlayerData(Vector2 velocity, Vector3 position) {
        this.vX = velocity.x;
        this.vY = velocity.y;

        this.pX = position.x;
        this.pY = position.y;
        this.pZ = position.z;
    }

    // Reconstruct velocity
    public Vector2 velocity() {
        return new Vector2(vX, vY);
    }

    // Reconstruct position
    public Vector3 position() {
        return new Vector3(pX, pY, pZ);
    }
}
