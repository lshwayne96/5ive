using System;
using UnityEngine;

/// <summary>
/// This class represents the data of a player.
/// </summary>
/// <remarks>
/// It is used to restore the player to its previous state.
/// </remarks>
/// /// The data includes:
/// <list type="number">
/// <item>The previous local scale</item>
/// <item>The previous velocity</item>
/// <item>The previous position</item>
/// <item>The previous gravity scale</item>
/// </list>
[Serializable]
public class PlayerData : Data {

	public Vector3 PrevLocalScale {
		get { return prevLocalScale; }
	}

	[NonSerialized]
	private Vector3 prevLocalScale;
	private readonly float sX;
	private readonly float sY;
	private readonly float sZ;

	public Vector2 PrevVelocity {
		get { return prevVelocity; }
	}

	[NonSerialized]
	private Vector2 prevVelocity;
	private readonly float vX;
	private readonly float vY;

	public Vector2 PrevPosition {
		get { return prevPosition; }
	}

	[NonSerialized]
	private Vector3 prevPosition;
	private readonly float pX;
	private readonly float pY;

	public float PrevGravityScale { get; private set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="T:PlayerData"/> class.
	/// </summary>
	/// <param name="player">Player.</param>
	public PlayerData(Player player) {
		prevLocalScale = player.transform.localScale;

		Rigidbody2D rigidbody2D = player.GetComponent<Rigidbody2D>();
		prevVelocity = rigidbody2D.velocity;
		prevPosition = rigidbody2D.position;

		// Previous local scale
		sX = prevLocalScale.x;
		sY = prevLocalScale.y;
		sZ = prevLocalScale.z;

		// Previous velocity
		vX = prevVelocity.x;
		vY = prevVelocity.y;

		// Previous position
		pX = prevPosition.x;
		pY = prevPosition.y;

		// Previous gravity scale
		PrevGravityScale = rigidbody2D.gravityScale;
	}

	public override void Restore(IRestorable restorable) {
		prevLocalScale = new Vector3(sX, sY, sZ);
		prevVelocity = new Vector2(vX, vY);
		prevPosition = new Vector3(pX, pY);
		restorable.RestoreWith(this);
	}
}
