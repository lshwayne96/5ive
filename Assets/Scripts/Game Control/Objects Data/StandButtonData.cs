/*
 * This class represents the data of a stand button.
 * It is is used to restore a stand button to
 * its previous position.
 */

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

    private bool isMoving;

    public StandButtonData(Vector3 prevPosition, Vector3 prevEndPosition, bool isMoving) {
        pX = prevPosition.x;
        pY = prevPosition.y;
        pZ = prevPosition.z;

        pEX = prevEndPosition.x;
        pEY = prevEndPosition.y;
        pEZ = prevEndPosition.z;

        this.isMoving = isMoving;
    }

    private Vector3 GetPrevPosition() {
        return new Vector3(pX, pY, pZ);
    }

    private Vector3 GetPrevEndPosition() {
        return new Vector3(pEX, pEY, pEZ);
    }

    public void Restore(StandButton standButton) {
        if (isMoving) {
            standButton.Resume(GetPrevPosition(), GetPrevEndPosition());
        }
    }
}
