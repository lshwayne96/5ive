using System;
using UnityEngine;

/// <summary>
/// This class represents the data of a lever.
/// </summary>
/// <remarks>
/// It is used to restore the lever to its previous state.
/// </remarks>
/// The data includes:
/// <list type="number">
/// <item>The previous start rotation</item>
/// <item>The previous end rotation</item>
/// <item>The original start rotation</item>
/// <item>The original end rotation</item>
/// </list>
[Serializable]
public class LeverData : Data {

	public Quaternion PrevStartRotation {
		get { return prevStartRotation; }
	}

	[NonSerialized]
	private Quaternion prevStartRotation;
	private readonly float pSX;
	private readonly float pSY;
	private readonly float pSZ;
	private readonly float pSW;

	public Quaternion PrevEndRotation {
		get { return prevEndRotation; }
	}

	[NonSerialized]
	private Quaternion prevEndRotation;
	private readonly float pEX;
	private readonly float pEY;
	private readonly float pEZ;
	private readonly float pEW;

	public Quaternion OriginalStartRotation {
		get { return originalStartRotation; }
	}

	[NonSerialized]
	private Quaternion originalStartRotation;
	private readonly float oSX;
	private readonly float oSY;
	private readonly float oSZ;
	private readonly float oSW;

	public Quaternion OriginalEndRotation {
		get { return originalEndRotation; }
	}

	[NonSerialized]
	private Quaternion originalEndRotation;
	private readonly float oEX;
	private readonly float oEY;
	private readonly float oEZ;
	private readonly float oEW;

	public Direction MovementDirection { get; private set; }

	public bool HasSwitchedRotation { get; private set; }

	public bool IsRotating { get; private set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="T:LeverData"/> class.
	/// </summary>
	/// <param name="lever">Lever.</param>
	public LeverData(Lever lever) {
		prevStartRotation = lever.transform.rotation;
		prevEndRotation = lever.EndRotation;
		originalStartRotation = lever.OriginalStartRotation;
		originalEndRotation = lever.OriginalEndRotation;

		// Previous start rotation
		pSX = prevStartRotation.x;
		pSY = prevStartRotation.y;
		pSZ = prevStartRotation.z;
		pSW = prevStartRotation.w;

		// Previous end rotation
		pEX = prevEndRotation.x;
		pEY = prevEndRotation.y;
		pEZ = prevEndRotation.z;
		pEW = prevEndRotation.w;

		// Original start rotation
		oSX = originalStartRotation.x;
		oSY = originalStartRotation.y;
		oSZ = originalStartRotation.z;
		oSW = originalStartRotation.w;

		// Original end rotation
		oEX = originalEndRotation.x;
		oEY = originalEndRotation.y;
		oEZ = originalEndRotation.z;
		oEW = originalEndRotation.w;

		MovementDirection = lever.MovementDirection;
		HasSwitchedRotation = lever.HasSwitchedRotation;
		IsRotating = lever.IsRotating;
	}

	public override void Restore(IRestorable restorable) {
		prevStartRotation = new Quaternion(pSX, pSY, pSZ, pSW);
		prevEndRotation = new Quaternion(pEX, pEY, pEZ, pEW);
		originalStartRotation = new Quaternion(oSX, oSY, oSZ, oSW);
		originalEndRotation = new Quaternion(oEX, oEY, oEZ, oEW);
		restorable.RestoreWith(this);
	}
}
