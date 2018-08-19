/*
 * This class represents the data of a ladder.
 * It is used to restore a ladder to its previous
 * set of boolean values.
 */

using System;
using UnityEngine;

[Serializable]
public class LadderData {
    public bool IsPassingThrough { get; private set; }
    public bool IsClimbing { get; private set; }
    public bool OutsideLadder { get; private set; }
    public bool CanClimb { get; private set; }
    public bool IsTrigger { get; private set; }
    public float OriginalGravityScale { get; private set; }
    public TopOfLadderData TopOfLadderData { get; private set; }

    public LadderData(Ladder ladder) {
        IsPassingThrough = ladder.IsPassingThrough;
        IsClimbing = ladder.IsClimbing;
        OutsideLadder = ladder.OutsideLadder;
        CanClimb = ladder.CanClimb;
        IsTrigger = ladder.GetComponent<BoxCollider2D>().isTrigger;
        OriginalGravityScale = ladder.OriginalGravityScale;
        TopOfLadderData = ladder.TopOfLadder.CacheData();
    }

    public void Restore(Ladder ladder, TopOfLadder topOfLadder) {
        ladder.Restore(this);
        TopOfLadderData.Restore(topOfLadder);
    }
}
