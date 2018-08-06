/*
 * This class represents the data of a ladder top.
 * It is used to restore a ladder top to its previous
 * set of boolean values.
 */

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
