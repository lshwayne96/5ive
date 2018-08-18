/*
 * This class represents the data of a stand button.
 * It is is used to restore a stand button to
 * its previous position.
 */

using System;
using UnityEngine;

[Serializable]
public class StandButtonData {

    public Vector3 PrevStartPosition {
        get { return prevStartPosition; }
    }
    [NonSerialized]
    private Vector3 prevStartPosition;
    private float pSX;
    private float pSY;
    private float pSZ;

    public Vector3 PrevEndPosition {
        get { return prevEndPosition; }
    }
    [NonSerialized]
    private Vector3 prevEndPosition;
    private float pEX;
    private float pEY;
    private float pEZ;

    public Vector3 OriginalStartPosition {
        get { return originalStartPosition; }
    }
    [NonSerialized]
    private Vector3 originalStartPosition;
    private float oSX;
    private float oSY;
    private float oSZ;
    private float oSW;

    public Vector3 OriginalEndPosition {
        get { return originalEndPosition; }
    }
    [NonSerialized]
    private Vector3 originalEndPosition;
    private float oEX;
    private float oEY;
    private float oEZ;
    private float oEW;

    public Direction MovementDirection { get; private set; }
    public bool IsDown { get; private set; }
    public bool IsMoving { get; private set; }
    public float WaitDuration { get; private set; }
    public float OriginalWaitDuration { get; private set; }

    public StandButtonData(StandButton standButton) {
        prevStartPosition = standButton.transform.position;
        prevEndPosition = standButton.EndPosition;
        originalStartPosition = standButton.OriginalStartPosition;
        originalEndPosition = standButton.OriginalEndPosition;

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

        MovementDirection = MovementDirection;
        IsDown = IsDown;
        IsMoving = IsMoving;
        WaitDuration = WaitDuration;
        OriginalWaitDuration = OriginalWaitDuration;
    }

    public void Restore(StandButton standButton) {
        standButton.Restore(this);
    }
}
