using System;
using Main._5ive.Commons;
using Main._5ive.Logic.Navigation;
using UnityEngine;

namespace Main._5ive.Model.Ball {

	/// <summary>
	/// /// This script represents a ball and its interactions.
	/// </summary>
	public class Ball : PersistentObject, IPausable {

		public float speed = 10f;

		private Transform playerTf;

		private Rigidbody2D rigidBody;

		/// <summary>
		/// The velocity before the rigid body is disabled.
		/// </summary>
		private Vector3 previousVelocity;

		private Camera watchingCamera;

		private RoomDetector roomDetector;

		public bool PlayerHasBall { get; private set; }

		public bool IsPlayerWithinRange { get; private set; }

		private void Start() {
			playerTf = GameObject.FindWithTag(Tags.Player).transform;
			rigidBody = GetComponent<Rigidbody2D>();
			watchingCamera = GameObject.FindWithTag(Tags.MainCamera).GetComponent<Camera>();
			roomDetector = GetComponent<RoomDetector>();
		}

		private void Update() {
			// Enable the ball to follow slightly behind the player when picked up
			if (PlayerHasBall) {
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
			return IsPlayerWithinRange && Input.GetKeyDown(KeyCode.E) && !PlayerHasBall;
		}

		private bool CanDropBall() {
			return Input.GetKeyDown(KeyCode.E) && PlayerHasBall;
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

		public override State Save() {
			return new BallState(this);
		}

		public override void RestoreWith(State state) {
			BallState ballState = (BallState) state;
			ballState.RebuildCompoundTypes();

			GetComponent<Rigidbody2D>().velocity = ballState.prevVelocity;
			transform.position = ballState.prevPosition;

			PlayerHasBall = ballState.playerHasBall;
			IsPlayerWithinRange = ballState.isPlayerWithinRange;
		}

		public void Pause() {
			previousVelocity = rigidBody.velocity;
			rigidBody.Sleep();
		}

		public void Resume() {
			rigidBody.velocity = previousVelocity;
			rigidBody.WakeUp();
		}

		public Camera GetCamera() {
			return watchingCamera;
		}

		public Transform GetCurrentRoomTransform() {
			return roomDetector.GetCurrentRoomTransform();
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
		public class BallState : State {

			[NonSerialized]
			public Vector2 prevVelocity;
			private readonly float vX;
			private readonly float vY;

			[NonSerialized] public Vector3 prevPosition;
			private readonly float pX;
			private readonly float pY;
			private readonly float pZ;

			public bool playerHasBall;

			public bool isPlayerWithinRange;

			/// <summary>
			/// Initializes a new instance of the <see cref="T:BallData"/> class.
			/// </summary>
			/// <param name="ball">Ball.</param>
			public BallState(Ball ball) {
				prevVelocity = ball.GetComponent<Rigidbody2D>().velocity;
				vX = prevVelocity.x;
				vY = prevVelocity.y;

				prevPosition = ball.transform.position;
				pX = prevPosition.x;
				pY = prevPosition.y;
				pZ = prevPosition.z;

				playerHasBall = ball.PlayerHasBall;
				isPlayerWithinRange = ball.IsPlayerWithinRange;
			}

			public void RebuildCompoundTypes() {
				prevVelocity = new Vector2(vX, vY);
				prevPosition = new Vector3(pX, pY, pZ);
			}
		}
	}

}