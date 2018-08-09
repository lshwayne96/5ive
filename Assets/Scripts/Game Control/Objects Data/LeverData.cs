/*
 * This class represents the data of a lever.
 * It is is used to restore a lver to
 * its previous rotation.
 */

using System;
using UnityEngine;

[Serializable]
public class LeverData {

    private float qPSX;
    private float qPSY;
    private float qPSZ;
    private float qPSW;

    private float qPEX;
    private float qPEY;
    private float qPEZ;
    private float qPEW;

    private float qOSX;
    private float qOSY;
    private float qOSZ;
    private float qOSW;

    private float qOEX;
    private float qOEY;
    private float qOEZ;
    private float qOEW;

    private Direction movementDirection;
    private bool hasSwitchedRotation;
    private bool isRotating;

    public LeverData(Quaternion prevStartRotation, Quaternion prevEndRotation,
                     Quaternion originalStartRotation, Quaternion originalEndRotation,
                     Direction movementDirection, bool hasSwitchedRotation, bool isRotating) {
        // Cache the prevStartRotation
        qPSX = prevStartRotation.x;
        qPSY = prevStartRotation.y;
        qPSZ = prevStartRotation.z;
        qPSW = prevStartRotation.w;

        // Cache the prevEndRotation
        qPEX = prevEndRotation.x;
        qPEY = prevEndRotation.y;
        qPEZ = prevEndRotation.z;
        qPEW = prevEndRotation.w;

        // Cache the originalStartRotation
        qOSX = originalStartRotation.x;
        qOSY = originalStartRotation.y;
        qOSZ = originalStartRotation.z;
        qOSW = originalStartRotation.w;

        // Cache the originalEndRotation
        qOEX = originalEndRotation.x;
        qOEY = originalEndRotation.y;
        qOEZ = originalEndRotation.z;
        qOEW = originalEndRotation.w;

        this.movementDirection = movementDirection;
        this.hasSwitchedRotation = hasSwitchedRotation;
        this.isRotating = isRotating;
    }

    private Quaternion GetRotation(float x, float y, float z, float w) {
        return new Quaternion(x, y, z, w);
    }

    public void Restore(Lever lever) {
        lever.Restore(new Quaternion(qPSX, qPSY, qPSZ, qPSW), new Quaternion(qPEX, qPEY, qPEZ, qPEW),
                      new Quaternion(qOSX, qOSY, qOSZ, qOSW), new Quaternion(qOEX, qOEY, qOEZ, qOEW),
                      movementDirection, hasSwitchedRotation, isRotating);
    }
}
