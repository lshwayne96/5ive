using System;

[Serializable]
public class TopOfLadderData {
    private bool isNearTop;
    private bool isLadderATrigger;

    public TopOfLadderData(bool isNearTop, bool isLadderATrigger) {
        this.isNearTop = isNearTop;
        this.isLadderATrigger = isLadderATrigger;
    }

    public void Restore(TopOfLadder topOfLadder) {
        topOfLadder.Restore(isNearTop, isLadderATrigger);
    }
}
