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

    private bool hasSwitchedRotation;
    private bool isRotating;

    public LeverData(Quaternion prevRotation, Quaternion prevEndRotation,
                     bool hasSwitchedRotation, bool isRotating) {
        qPX = prevRotation.x;
        qPY = prevRotation.y;
        qPZ = prevRotation.z;
        qPW = prevRotation.w;

        qEX = prevEndRotation.x;
        qEY = prevEndRotation.y;
        qEZ = prevEndRotation.z;
        qEW = prevEndRotation.w;

        this.hasSwitchedRotation = hasSwitchedRotation;
        this.isRotating = isRotating;
    }

    private Quaternion GetPrevRotation() {
        return new Quaternion(qPX, qPY, qPZ, qPW);
    }

    private Quaternion GetPrevEndRotation() {
        return new Quaternion(qEX, qEY, qEZ, qEW);
    }

    public void Restore(Lever lever) {
        if (isRotating) {
            lever.ResumeRotation(GetPrevRotation(), GetPrevEndRotation());
        }

        if (hasSwitchedRotation) {
            lever.SwitchRotation();
        }
    }
}
