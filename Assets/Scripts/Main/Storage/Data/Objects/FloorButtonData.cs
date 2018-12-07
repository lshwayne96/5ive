using System;
using UnityEngine;

/// <summary>
/// This class represents the data of a floor button.
/// </summary>
/// <remarks>
/// It is used to restore the floor button to its previous state.
/// </remarks>
/// The data includes:
/// <list type="number">
/// <item>The previous start position</item>
/// <item>The previous end position</item>
/// <item>The original start position</item>
/// <item>The original end position</item>
/// <item>The movement direction</item>
/// <item>And many more...</item>
/// </list>
[Serializable]
public class FloorButtonData : Data {

	public Vector3 PrevStartPosition {
		get { return prevStartPosition; }
	}

	[NonSerialized]
	private Vector3 prevStartPosition;
	private readonly float pSX;
	private readonly float pSY;
	private readonly float pSZ;

	public Vector3 PrevEndPosition {
		get { return prevEndPosition; }
	}

	[NonSerialized]
	private Vector3 prevEndPosition;
	private readonly float pEX;
	private readonly float pEY;
	private readonly float pEZ;

	public Vector3 OriginalStartPosition {
		get { return originalStartPosition; }
	}

	[NonSerialized]
	private Vector3 originalStartPosition;
	private readonly float oSX;
	private readonly float oSY;
	private readonly float oSZ;
	private readonly float oSW;

	public Vector3 OriginalEndPosition {
		get { return originalEndPosition; }
	}

	[NonSerialized]
	private Vector3 originalEndPosition;
	private readonly float oEX;
	private readonly float oEY;
	private readonly float oEZ;
	private readonly float oEW;

	public Direction MovementDirection { get; private set; }
	public bool IsDown { get; private set; }
	public bool IsMoving { get; private set; }
	public float WaitDuration { get; private set; }
	public float OriginalWaitDuration { get; private set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="T:FloorButtonData"/> class.
	/// </summary>
	/// <param name="floorButton">Floor button.</param>
	public FloorButtonData(FloorButton floorButton) {
		prevStartPosition = floorButton.transform.position;
		prevEndPosition = floorButton.EndPosition;
		originalStartPosition = floorButton.OriginalStartPosition;
		originalEndPosition = floorButton.OriginalEndPosition;

		// Previous start position
		pSX = prevStartPosition.x;
		pSY = prevStartPosition.y;
		pSZ = prevStartPosition.z;

		// Previous end position
		pEX = prevEndPosition.x;
		pEY = prevEndPosition.y;
		pEZ = prevEndPosition.z;

		// Original start position
		oSX = originalStartPosition.x;
		oSY = originalStartPosition.y;
		oSZ = originalStartPosition.z;

		// Original end position
		oEX = originalEndPosition.x;
		oEY = originalEndPosition.y;
		oEZ = originalEndPosition.z;

		// Other data
		MovementDirection = floorButton.MovementDirection;
		IsDown = floorButton.IsDown;
		IsMoving = floorButton.IsMoving;
		WaitDuration = floorButton.WaitDuration;
		OriginalWaitDuration = floorButton.OriginalWaitDuration;
	}

	public override void Restore(IRestorable restorable) {
		prevStartPosition = new Vector3(pSX, pSY, pSZ);
		prevEndPosition = new Vector3(pEX, pEY, pEZ);
		originalStartPosition = new Vector3(oSX, oSY, oSZ);
		originalEndPosition = new Vector3(oEX, oEY, oEZ);
		restorable.RestoreWith(this);
	}
}
