using UnityEngine;

/// <summary>
/// This script represents a player and its interactions.
/// </summary>
public class Player : MonoBehaviour {

	public int MovementSpeed = 10;

	void Update() {
		if (Input.GetKey(KeyCode.UpArrow)) {
			Move(Direction.Up);
		} else if (Input.GetKey(KeyCode.DownArrow)) {
			Move(Direction.Down);
		} else if (Input.GetKey(KeyCode.LeftArrow)) {
			Move(Direction.Left);
		} else if (Input.GetKey(KeyCode.RightArrow)) {
			Move(Direction.Right);
		} else if (Input.GetKey(KeyCode.Space)) {
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
		transform.Translate(directionVector * MovementSpeed);
	}

	private void Jump() {

	}
}
