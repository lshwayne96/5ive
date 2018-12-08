using System;
using UnityEngine;

/// <summary>
/// This class is a response to the way the standard asset scripts,
/// that a player uses for movement, work.
/// </summary>
/// <remarks>
/// After loading a saved level, if the player was saved as facing the left,
/// moving off would lead to the character moving about in a flipped state, and vice-versa.
/// To solve this, the scale of the player gameObject is flipped in LateUpdate() to prevent
/// the above behaviour.
/// </remarks>
public class ReponsePlayer : MonoBehaviour {

	public Direction Orientation { get; private set; }
	private bool hasRestoredOrientation;

	// LateUpdate() is used to allow Flip() in PlatformerCharacter2D to run first
	void LateUpdate() {
		if ((IsFacingLeftAfterRestore() && Input.GetKeyDown(KeyCode.RightArrow)) ||
			 (IsFacingRightAfterRestore() && Input.GetKeyDown(KeyCode.LeftArrow))) {
			ToggleOrientation();
			hasRestoredOrientation = true;
			enabled = false;
		} else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) {
			hasRestoredOrientation = true;
		}

		if (FacingDirection() == Direction.Left) {
			Orientation = Direction.Left;
		}

		if (FacingDirection() == Direction.Right) {
			Orientation = Direction.Right;
		}
	}

	private bool IsFacingLeftAfterRestore() {
		return RestoreLevel.restoreLevel.hasRestoredScene
						   && (FacingDirection() == Direction.Left) && !hasRestoredOrientation;
	}

	private bool IsFacingRightAfterRestore() {
		return RestoreLevel.restoreLevel.hasRestoredScene
						   && (FacingDirection() == Direction.Right) && !hasRestoredOrientation;
	}

	public Direction FacingDirection() {
		if (transform.localScale.x > 0) {
			return Direction.Right;
		} else if (transform.localScale.x < 0) {
			return Direction.Left;
		} else {
			return Direction.Undefined;
		}
	}

	public void ToggleOrientation() {
		Vector3 playerScale = transform.localScale;
		playerScale.x *= -1;
		transform.localScale = playerScale;
	}

	public PlayerData CacheData() {
		return new PlayerData(this);
	}

	public void Restore(PlayerData playerData) {
		transform.localScale = playerData.PrevLocalScale;
		GetComponent<Rigidbody2D>().velocity = playerData.PrevVelocity;
		transform.position = playerData.PrevPosition;
		GetComponent<Rigidbody2D>().gravityScale = playerData.PrevGravityScale;
		// Restore player camera
		GetComponent<DetectRoom>().SetCurrentRoom();
	}

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
		public PlayerData(ReponsePlayer player) {
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

		public override void Restore(RestorableMonoBehaviour restorable) {
			prevLocalScale = new Vector3(sX, sY, sZ);
			prevVelocity = new Vector2(vX, vY);
			prevPosition = new Vector3(pX, pY);
			restorable.RestoreWith(this);
		}
	}
}
