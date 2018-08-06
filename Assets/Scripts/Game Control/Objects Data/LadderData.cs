/*
 * This class represents the data of a ladder.
 * It is used to restore a ladder to its previous
 * set of boolean values.
 */

using System;

[Serializable]
public class LadderData {
    private bool isPassingThrough;
    private bool wasClimbing;
    private bool outsideLadder;
    private bool canClimb;
    private float originalGravityScale;
    private TopOfLadderData topOfLadderData;

    public LadderData(bool isPassingThrough, bool wasClimbing, bool outsideLadder,
                      bool canClimb, float originalGravityScale, TopOfLadder topOfLadder) {
        this.isPassingThrough = isPassingThrough;
        this.wasClimbing = wasClimbing;
        this.outsideLadder = outsideLadder;
        this.canClimb = canClimb;
        this.originalGravityScale = originalGravityScale;
        this.topOfLadderData = topOfLadder.CacheData();
    }

    public void Restore(Ladder ladder, TopOfLadder topOfLadder) {
        ladder.Restore(isPassingThrough, wasClimbing, outsideLadder,
                       canClimb, originalGravityScale);
        topOfLadderData.Restore(topOfLadder);
    }
}
