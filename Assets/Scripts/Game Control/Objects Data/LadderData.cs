using System;
using UnityEngine;

[Serializable]
public class LadderData {
    private bool isPassingThrough;
    private bool wasClimbing;
    private bool outsideLadder;
    private bool canClimb;
    private float currentGravityScale;

    public LadderData(bool isPassingThrough, bool wasClimbing, bool outsideLadder,
                      bool canClimb, float currentGravityScale) {
        this.isPassingThrough = isPassingThrough;
        this.wasClimbing = wasClimbing;
        this.outsideLadder = outsideLadder;
        this.canClimb = canClimb;
        this.currentGravityScale = currentGravityScale;

    }
}
