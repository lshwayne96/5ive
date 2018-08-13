/*
 * This class represents the data of a lever.
 * It is is used to restore a lver to
 * its previous rotation.
 */

using System;
using UnityEngine;

[Serializable]
public class LeverData {

    private float pSX;
    private float pSY;
    private float pSZ;
    private float pSW;

    private float pEX;
    private float pEY;
    private float pEZ;
    private float pEW;

    private float oSX;
    private float oSY;
    private float oSZ;
    private float oSW;

    private float oEX;
    private float oEY;
    private float oEZ;
    private float oEW;

    private Direction movementDirection;
    private bool hasSwitchedRotation;
    private bool isRotating;

    public LeverData(Quaternion prevStartRotation, Quaternion prevEndRotation,
                     Quaternion originalStartRotation, Quaternion originalEndRotation,
                     Direction movementDirection, bool hasSwitchedRotation, bool isRotating) {
        // Cache the prevStartRotation
        pSX = prevStartRotation.x;
        pSY = prevStartRotation.y;
        pSZ = prevStartRotation.z;
        pSW = prevStartRotation.w;

        // Cache the prevEndRotation
        pEX = prevEndRotation.x;
        pEY = prevEndRotation.y;
        pEZ = prevEndRotation.z;
        pEW = prevEndRotation.w;

        // Cache the originalStartRotation
        oSX = originalStartRotation.x;
        oSY = originalStartRotation.y;
        oSZ = originalStartRotation.z;
        oSW = originalStartRotation.w;

        // Cache the originalEndRotation
        oEX = originalEndRotation.x;
        oEY = originalEndRotation.y;
        oEZ = originalEndRotation.z;
        oEW = originalEndRotation.w;

        this.movementDirection = movementDirection;
        this.hasSwitchedRotation = hasSwitchedRotation;
        this.isRotating = isRotating;
    }

    public void Restore(Lever lever) {
        lever.Restore(new Quaternion(pSX, pSY, pSZ, pSW), new Quaternion(pEX, pEY, pEZ, pEW),
                      new Quaternion(oSX, oSY, oSZ, oSW), new Quaternion(oEX, oEY, oEZ, oEW),
                      movementDirection, hasSwitchedRotation, isRotating);
    }
}
