using System;
using UnityEngine;

/// <summary>
/// This script represents a ball and its interactions.
/// </summary>
public class Ball : RestorableMonoBehaviour {

	// Expose the speed variable to the editor
	public float speed = 10f;

	private Transform playerTf;

	public bool PlayerHasBall { get; private set; }
	public bool IsPlayerWithinRange { get; private set; }

	void Start() {
		playerTf = GameObject.FindWithTag(Tags.Player).transform;
	}

	void Update() {
		// Enable the ball to follow slightly behind the player when picked up
		if (PlayerHasBall && !PauseLevel.IsPaused) {
			// Stop the rotation of the ball
			transform.rotation = Quaternion.Euler(Vector3.zero);
			Vector3 targetPosition = playerTf.position;
			GetComponent<Rigidbody2D>().velocity = speed * (targetPosition - transform.position);
		}

		if (CanPickUpBall()) {
			PickUpBall();
		} else if (CanDropBall()) {
			DropBall();
		}
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.CompareTag(Tags.Player)) {
			IsPlayerWithinRange = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision) {
		if (collision.gameObject.CompareTag(Tags.Player)) {
			IsPlayerWithinRange = false;
		}
	}

	private bool CanPickUpBall() {
		return IsPlayerWithinRange && Input.GetKeyDown(KeyCode.E) && !PlayerHasBall && !PauseLevel.IsPaused;
	}

	private bool CanDropBall() {
		return Input.GetKeyDown(KeyCode.E) && PlayerHasBall && !PauseLevel.IsPaused;
	}

	private void PickUpBall() {
		transform.position = playerTf.position;
		GetComponent<Rigidbody2D>().gravityScale = 0f;
		PlayerHasBall = true;
	}

	private void DropBall() {
		GetComponent<Rigidbody2D>().gravityScale = 1f;
		PlayerHasBall = false;
	}

	public override Data Save() {
		return new BallData(this);
	}

	public override void RestoreWith(Data data) {
		BallData ballData = (BallData) data;
		GetComponent<Rigidbody2D>().velocity = ballData.PrevVelocity;
		transform.position = ballData.PrevPosition;
		// Restore ball camera
		GetComponent<RoomDetector>().UpdateRoom();
		PlayerHasBall = ballData.PlayerHasBall;
		IsPlayerWithinRange = ballData.IsPlayerWithinRange;
	}

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

		public override void Restore(RestorableMonoBehaviour restorable) {
			prevVelocity = new Vector2(vX, vY);
			prevPosition = new Vector3(pX, pY, pZ);
			restorable.RestoreWith(this);
		}
	}
}

