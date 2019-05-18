using System;
using Main.Commons;
using Main.Logic.Navigation;
using UnityEngine;
using UnityStandardAssets._2D;

namespace Main.Model.Player {

	/// <summary>
	/// This script represents a player and its interactions.
	/// </summary>
	public class Player : RestorableMonoBehaviour, IPausable {

		private const float MovementSpeed = 10f;

		private const float JumpForce = 500f;

		private Rigidbody2D rigidBody;

		private Platformer2DUserControl movement;

		private Animator animator;

		/// <summary>
		/// The velocity before the rigidbody is disabled.
		/// </summary>
		private Vector3 previousVelocity;

		private void Start() {
			rigidBody = GetComponent<Rigidbody2D>();
			movement = GetComponent<Platformer2DUserControl>();
			animator = GetComponent<Animator>();
		}

		private void Update() {
			if (Input.GetKey(KeyCode.UpArrow)) {
				Move(Direction.Up);
			} else if (Input.GetKey(KeyCode.DownArrow)) {
				Move(Direction.Down);
			} else if (Input.GetKey(KeyCode.LeftArrow)) {
				Move(Direction.Left);
			} else if (Input.GetKey(KeyCode.RightArrow)) {
				Move(Direction.Right);
			}
		}

		private void FixedUpdate() {
			if (Input.GetKeyDown(KeyCode.Space)) {
				Jump();
			}

		}

		private void Move(Direction direction) {
			Vector3 directionVector;
			switch (direction) {
				case Direction.Up:
					directionVector = Vector3.up;
					break;
				case Direction.Down:
					directionVector = Vector3.down;
					break;
				case Direction.Left:
					directionVector = Vector3.left;
					break;
				case Direction.Right:
					directionVector = Vector3.right;
					break;
				default:
					Debug.Assert(false, "Invalid Direction");
					return;
			}

			transform.Translate(directionVector * MovementSpeed * Time.deltaTime);
		}

		private void Jump() {
			GetComponent<Rigidbody2D>().AddForce(Vector3.up * JumpForce * Time.deltaTime, ForceMode2D.Impulse);
		}

		public override Data Save() {
			return new PlayerData(this);
		}

		public override void RestoreWith(Data data) {
			PlayerData playerData = (PlayerData) data;
			playerData.RebuildCompoundTypes();

			GetComponent<Rigidbody2D>().velocity = playerData.prevVelocity;
			transform.position = playerData.prevPosition;
			GetComponent<Rigidbody2D>().gravityScale = playerData.prevGravityScale;

			// Restore player camera
			GetComponent<RoomDetector>().UpdateRoom();
		}

		public void Pause() {
			previousVelocity = rigidBody.velocity;
			rigidBody.Sleep();
			movement.enabled = false;
			animator.enabled = false;
		}

		public void Unpause() {
			rigidBody.velocity = previousVelocity;
			rigidBody.WakeUp();
			movement.enabled = true;
			animator.enabled = true;
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

			[NonSerialized] public Vector2 prevVelocity;

			private readonly float vX;

			private readonly float vY;

			[NonSerialized] public Vector3 prevPosition;

			private readonly float pX;

			private readonly float pY;

			public float prevGravityScale;

			/// <summary>
			/// Initializes a new instance of the <see cref="T:PlayerData"/> class.
			/// </summary>
			/// <param name="player">Player.</param>
			public PlayerData(Player player) {
				Rigidbody2D rigidbody2D = player.GetComponent<Rigidbody2D>();
				prevVelocity = rigidbody2D.velocity;
				prevPosition = rigidbody2D.position;

				// Previous velocity
				vX = prevVelocity.x;
				vY = prevVelocity.y;

				// Previous position
				pX = prevPosition.x;
				pY = prevPosition.y;

				// Previous gravity scale
				prevGravityScale = rigidbody2D.gravityScale;
			}

			public void RebuildCompoundTypes() {
				prevVelocity = new Vector2(vX, vY);
				prevPosition = new Vector3(pX, pY);
			}
		}
	}

}
