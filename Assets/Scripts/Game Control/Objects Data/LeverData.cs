/*
 * This class represents the data of a lever.
 * It is is used to restore a lver to
 * its previous rotation.
 */

using System;
using UnityEngine;

[Serializable]
public class LeverData {

    public Quaternion PrevStartRotation {
        get { return prevStartRotation; }
    }
    [NonSerialized]
    private Quaternion prevStartRotation;
    private float pSX;
    private float pSY;
    private float pSZ;
    private float pSW;

    public Quaternion PrevEndRotation {
        get { return prevEndRotation; }
    }
    [NonSerialized]
    private Quaternion prevEndRotation;
    private float pEX;
    private float pEY;
    private float pEZ;
    private float pEW;

    public Quaternion OriginalStartRotation {
        get { return originalStartRotation; }
    }
    [NonSerialized]
    private Quaternion originalStartRotation;
    private float oSX;
    private float oSY;
    private float oSZ;
    private float oSW;

    public Quaternion OriginalEndRotation {
        get { return originalEndRotation; }
    }
    [NonSerialized]
    private Quaternion originalEndRotation;
    private float oEX;
    private float oEY;
    private float oEZ;
    private float oEW;

    public Direction MovementDirection { get; private set; }
    public bool HasSwitchedRotation { get; private set; }
    public bool IsRotating { get; private set; }

    public LeverData(Lever lever) {
        prevStartRotation = lever.transform.rotation;
        prevEndRotation = lever.EndRotation;
        originalStartRotation = lever.OriginalStartRotation;
        originalEndRotation = lever.OriginalEndRotation;

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

        this.MovementDirection = lever.MovementDirection;
        this.HasSwitchedRotation = lever.HasSwitchedRotation;
        this.IsRotating = lever.IsRotating;
    }

    public void Restore(Lever lever) {
        prevStartRotation = new Quaternion(pSX, pSY, pSZ, pSW);
        prevEndRotation = new Quaternion(pEX, pEY, pEZ, pEW);
        originalStartRotation = new Quaternion(oSX, oSY, oSZ, oSW);
        originalEndRotation = new Quaternion(oEX, oEY, oEZ, oEW);
        lever.Restore(this);
    }
}
