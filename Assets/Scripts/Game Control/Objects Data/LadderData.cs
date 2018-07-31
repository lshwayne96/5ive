using System;

[Serializable]
public class LadderData {
    private bool isPassingThrough;
    private bool wasClimbing;
    private bool outsideLadder;
    private bool canClimb;
    private float currentGravityScale;
    private TopOfLadderData topOfLadderData;

    public LadderData(bool isPassingThrough, bool wasClimbing, bool outsideLadder,
                      bool canClimb, float currentGravityScale,
                      TopOfLadder topOfLadder) {
        this.isPassingThrough = isPassingThrough;
        this.wasClimbing = wasClimbing;
        this.outsideLadder = outsideLadder;
        this.canClimb = canClimb;
        this.currentGravityScale = currentGravityScale;
        this.topOfLadderData = topOfLadder.CacheData();
    }

    public void Restore(Ladder ladder, TopOfLadder topOfLadder) {
        ladder.Restore(isPassingThrough, wasClimbing, outsideLadder,
                       canClimb, currentGravityScale);
        topOfLadderData.Restore(topOfLadder);
    }
}
