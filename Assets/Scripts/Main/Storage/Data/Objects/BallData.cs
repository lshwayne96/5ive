using System;
using UnityEngine;

/// <summary>
/// This class represents the data of ball.
/// </summary>
/// <remarks>
/// It is used to restore the ball to it's previous state
/// when the game starts.
/// </remarks>
/// The data includes:
/// <list type="number">
/// <item>The last position</item>
/// <item>The last velocity</item>
/// </list>
[Serializable]
public class BallData : IData {

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
	private float pX;
	private float pY;
	private float pZ;

	public bool PlayerHasBall { get; private set; }
	public bool IsPlayerWithinRange { get; private set; }

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

	public void Restore(IRestorable ball) {
		prevVelocity = new Vector2(vX, vY);
		prevPosition = new Vector3(pX, pY, pZ);
		ball.RestoreWith(this);
	}
}