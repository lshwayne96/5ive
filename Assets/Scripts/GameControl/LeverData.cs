using System;
using UnityEngine;

[Serializable]
public class LeverData {

    private float qPX;
    private float qPY;
    private float qPZ;
    private float qPW;

    private float qEX;
    private float qEY;
    private float qEZ;
    private float qEW;

    private float qCX;
    private float qCY;
    private float qCZ;
    private float qCW;

    private bool hasSwitchedPosition;
    private bool isRotating;

    public LeverData(Quaternion prevRotation, Quaternion prevEndRotation,
                     Quaternion currentRotation, bool hasSwitchedPosition, bool isRotating) {
        qPX = prevRotation.x;
        qPY = prevRotation.y;
        qPZ = prevRotation.z;
        qPW = prevRotation.w;

        qEW = prevEndRotation.x;
        qEY = prevEndRotation.y;
        qEZ = prevEndRotation.z;
        qEW = prevEndRotation.w;

        qCW = currentRotation.x;
        qCY = currentRotation.y;
        qCZ = currentRotation.z;
        qCW = currentRotation.w;

        this.hasSwitchedPosition = hasSwitchedPosition;
        this.isRotating = isRotating;
    }

    private Quaternion GetPrevRotation() {
        return new Quaternion(qCX, qCY, qCZ, qCW);
    }

    private Quaternion GetPrevEndRotation() {
        return new Quaternion(qCX, qCY, qCZ, qCW);
    }

    public void Restore(Lever lever) {
        if (isRotating) {
            lever.SetPrevRotation(GetPrevRotation());
            lever.SetPrevEndRotation(GetPrevEndRotation());
            lever.ResumeRotation();
        }

        if (hasSwitchedPosition) {
            lever.SwitchRotation();
        }
    }
}
