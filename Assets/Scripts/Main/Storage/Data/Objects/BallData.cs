using System;
using UnityEngine;

/// <summary>
/// This class represents the data of a ball.
/// </summary>
/// <remarks>
/// It is used to restore the ball to its previous state.
/// </remarks>
/// The data includes:
/// <list type="number">
/// <item>The last position</item>
/// <item>The last velocity</item>
/// </list>
[Serializable]
public class BallData : Data {

	public Vector2 PrevVelocity {
		get { return prevVelocity; }
	}

	[NonSerialized]
	private Vector2 prevVelocity;
	private readonly float vX;
	private readonly float vY;

	public Vector3 PrevPosition {
		get { return prevPosition; }
	}

	[NonSerialized]
	private Vector3 prevPosition;
	private readonly float pX;
	private readonly float pY;
	private readonly float pZ;

	public bool PlayerHasBall { get; private set; }
	public bool IsPlayerWithinRange { get; private set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="T:BallData"/> class.
	/// </summary>
	/// <param name="ball">Ball.</param>
	public BallData(Ball ball) {
		prevVelocity = ball.GetComponent<Rigidbody2D>().velocity;
		vX = prevVelocity.x;
		vY = prevVelocity.y;

		prevPosition = ball.transform.position;
		pX = prevPosition.x;
		pY = prevPosition.y;
		pZ = prevPosition.z;

		PlayerHasBall = ball.PlayerHasBall;
		IsPlayerWithinRange = ball.IsPlayerWithinRange;
	}

	public override void Restore(IRestorable restorable) {
		prevVelocity = new Vector2(vX, vY);
		prevPosition = new Vector3(pX, pY, pZ);
		restorable.RestoreWith(this);
	}
}