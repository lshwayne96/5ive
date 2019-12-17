using Main._5ive.Commons;
using UnityEngine;

namespace Main._5ive.Model.Player {

	public class PlayerMovement : MonoBehaviour {
		private const float MovementSpeed = 10f;
		private const float JumpForce = 500f;

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

			transform.Translate(MovementSpeed * Time.deltaTime * directionVector);
		}

		private void Jump() {
			GetComponent<Rigidbody2D>().AddForce(JumpForce * Time.deltaTime * Vector3.up, ForceMode2D.Impulse);
		}
	}

}
