using UnityEngine;

/// <summary>
/// This script represents the camera pointing at the ball.
/// </summary>
public class BallCamera : MonoBehaviour {

	private GameObject playerGO;

	private GameObject playerCamGO;

	private Transform playerCamTf;

	private Transform playerTf;

	RoomDetector roomDetector

	private void Start() {
		playerGO = GameObject.FindGameObjectWithTag(Tags.Player);
		playerCamGO = GameObject.FindGameObjectWithTag(Tags.PlayerCamera);
	}

	private void Update() {
		playerCamTf = playerCamGO.transform;

		Vector3 offset = playerCamTf.position - CurrentRoomSetter.currentPlayerRoomTf.position;

		BoxCollider2D playerRoomCollider = CurrentRoomSetter.currentPlayerRoomTf.GetComponent<BoxCollider2D>();
		BoxCollider2D ballRoomCollider = CurrentRoomSetter.currentBallRoomTf.GetComponent<BoxCollider2D>();

		// Difference in scale of 2 rooms
		float scaleFactor_x = ballRoomCollider.size.x / playerRoomCollider.size.x;
		float scaleFactor_y = ballRoomCollider.size.y / playerRoomCollider.size.y;

		GetComponent<Camera>().orthographicSize = 5 * Mathf.Min(scaleFactor_x, scaleFactor_y);

		if (scaleFactor_x > scaleFactor_y) {
			playerTf = playerGO.transform;
			float playerOffset = (playerTf.position - playerCamTf.position).x;
			float offSetLimit = (scaleFactor_x / scaleFactor_y / 4f + 0.5f) * 4.5f; //not sure if correct
			if (Mathf.Abs(playerOffset) > offSetLimit) {
				playerOffset = Mathf.Sign(playerOffset) * offSetLimit;
			}
			offset += new Vector3(playerOffset, 0, 0);
		}

		gameObject.transform.position =
			new Vector3(CurrentRoomSetter.currentBallRoomTf.position.x + offset.x * scaleFactor_x,
						CurrentRoomSetter.currentBallRoomTf.position.y + offset.y * scaleFactor_y,
						-10f);
	}
}
