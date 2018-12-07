using System;
using UnityEngine;

/// <summary>
/// This class represents the data of a ladder.
/// </summary>
/// <remarks>
/// It is used to restore the ladder to its previous state.
/// </remarks>
/// /// The data includes:
/// <list type="number">
/// <item></item>
/// <item></item>
/// </list>
[Serializable]
public class LadderData {

	public bool IsPassingThrough { get; private set; }
	public bool IsClimbing { get; private set; }
	public bool OutsideLadder { get; private set; }
	public bool CanClimb { get; private set; }
	public bool IsTrigger { get; private set; }
	public float OriginalGravityScale { get; private set; }
	public TopOfLadderData TopOfLadderData { get; private set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="T:LadderData"/> class.
	/// </summary>
	/// <param name="ladder">Ladder.</param>
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
