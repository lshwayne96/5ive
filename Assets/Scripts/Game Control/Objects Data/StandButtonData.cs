/*
 * This class represents the data of a stand button.
 * It is is used to restore a stand button to
 * its previous position.
 */

using System;
using UnityEngine;

[Serializable]
public class StandButtonData {

    private float pSX;
    private float pSY;
    private float pSZ;

    private float pEX;
    private float pEY;
    private float pEZ;

    private float oSX;
    private float oSY;
    private float oSZ;
    private float oSW;

    private float oEX;
    private float oEY;
    private float oEZ;
    private float oEW;

    private Direction movementDirection;
    private bool isDown;
    private bool isMoving;
    private float waitDuration;
    private float originalWaitDuration;

    public StandButtonData(Vector3 prevStartPosition, Vector3 prevEndPosition,
                           Vector3 originalStartPosition, Vector3 originalEndPosition,
                           Direction movementDirection, bool isDown, bool isMoving,
                           float waitDuration, float originalWaitDuration) {
        pSX = prevStartPosition.x;
        pSY = prevStartPosition.y;
        pSZ = prevStartPosition.z;

        pEX = prevEndPosition.x;
        pEY = prevEndPosition.y;
        pEZ = prevEndPosition.z;

        // Cache the originalStartPosition
        oSX = originalStartPosition.x;
        oSY = originalStartPosition.y;
        oSZ = originalStartPosition.z;

        // Cache the originalEndPosition
        oEX = originalEndPosition.x;
        oEY = originalEndPosition.y;
        oEZ = originalEndPosition.z;

        this.movementDirection = movementDirection;
        this.isDown = isDown;
        this.isMoving = isMoving;
        this.waitDuration = waitDuration;
        this.originalWaitDuration = originalWaitDuration;
    }

    public void Restore(StandButton standButton) {
        standButton.Restore(new Vector3(pSX, pSY, pSZ), new Vector3(pEX, pEY, pEZ),
                            new Vector3(oSX, oSY, oSZ), new Vector3(oEX, oEY, oEZ),
                            movementDirection, isDown, isMoving,
                            waitDuration, originalWaitDuration);
    }
}
