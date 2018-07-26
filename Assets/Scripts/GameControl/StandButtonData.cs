using System;
using UnityEngine;

[Serializable]
public class StandButtonData {

    private float pX;
    private float pY;
    private float pZ;

    private float pEX;
    private float pEY;
    private float pEZ;

    private bool isDepressing;

    public StandButtonData(Vector3 prevPosition, Vector3 prevEndPosition, bool isDepressing) {
        pX = prevPosition.x;
        pY = prevPosition.y;
        pZ = prevPosition.z;

        pEX = prevEndPosition.x;
        pEY = prevEndPosition.y;
        pEZ = prevEndPosition.z;

        this.isDepressing = isDepressing;
    }
}
