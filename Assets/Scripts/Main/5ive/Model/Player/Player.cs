using System;
using Main._5ive.Commons;
using Main._5ive.Logic.Navigation;
using Main._5ive.Logic.Navigation.Cameras;
using UnityEngine;

namespace Main._5ive.Model.Player {
	/// <summary>
	/// This script represents a player and its interactions.
	/// </summary>
	public class Player : PersistentObject, IPausable {
		private Rigidbody2D rigidBody;
		private PlayerCamera mainCamera;
		private RoomDetector roomDetector;

		/// <summary>
		/// The velocity before the rigid body is disabled.
		/// </summary>
		private Vector3 previousVelocity;

		private void Start() {
			rigidBody = GetComponent<Rigidbody2D>();
			mainCamera = GameObject.FindWithTag(Tags.MainCamera).GetComponent<PlayerCamera>();
		}

		public override State Save() {
			return new PlayerState(this);
		}

		public override void RestoreWith(State state) {
			PlayerState playerState = (PlayerState) state;
			playerState.Init();

			GetComponent<Rigidbody2D>().velocity = playerState.velocity;
			transform.position = playerState.position;
			GetComponent<Rigidbody2D>().gravityScale = playerState.gravityScale;
		}

		public void Pause() {
			previousVelocity = rigidBody.velocity;
			rigidBody.Sleep();
		}

		public void Resume() {
			rigidBody.velocity = previousVelocity;
			rigidBody.WakeUp();
		}

		public Transform GetCurrentRoomTransform() {
			return roomDetector.GetCurrentRoomTransform();
		}

		private void OnTriggerExit2D(Collider2D other) {
			if (other.gameObject.layer == (int) Layer.Room) {
				Vector3 newPosition = roomDetector.GetCurrentRoomPosition();
				mainCamera.SetPosition(newPosition);
			}
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
		public class PlayerState : State {
			[NonSerialized] public Vector2 velocity;
			private readonly float vX;
			private readonly float vY;

			[NonSerialized] public Vector3 position;
			private readonly float pX;
			private readonly float pY;

			public float gravityScale;

			/// <summary>
			/// Initializes a new instance of the <see cref="T:PlayerData"/> class.
			/// </summary>
			/// <param name="player">Player.</param>
			public PlayerState(Player player) {
				Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
				velocity = rb.velocity;
				position = rb.position;

				vX = velocity.x;
				vY = velocity.y;

				pX = position.x;
				pY = position.y;

				gravityScale = rb.gravityScale;
			}

			public void Init() {
				velocity = new Vector2(vX, vY);
				position = new Vector3(pX, pY);
			}
		}
	}
}
