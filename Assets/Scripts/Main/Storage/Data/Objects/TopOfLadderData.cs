/*
 * This class represents the data of a ladder top.
 * It is used to restore a ladder top to its previous
 * set of boolean values.
 */

using System;

[Serializable]
public class TopOfLadderData {
    public bool IsNearTop { get; private set; }
    public bool IsLadderATrigger { get; private set; }

    public TopOfLadderData(TopOfLadder topOfLadder) {
        IsNearTop = topOfLadder.IsNearTop;
        IsLadderATrigger = topOfLadder.IsLadderATrigger;
    }

    public void Restore(TopOfLadder topOfLadder) {
        topOfLadder.Restore(this);
    }
}
