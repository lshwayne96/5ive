using UnityEngine;

/// <summary>
/// This script represents the camera pointing at the ball.
/// </summary>
public class BallCamera : MonoBehaviour {

	private GameObject playerGO;

	private GameObject playerCamGO;

	private GameObject ballGO;

	private Transform playerCamTf;

	private Transform playerTf;

	private RoomDetector playerRoomDetector;

	private RoomDetector ballRoomDetector;

	private void Start() {
		playerGO = GameObject.FindGameObjectWithTag(Tags.Player);
		playerCamGO = GameObject.FindGameObjectWithTag(Tags.PlayerCamera);

		ballGO = GameObject.FindGameObjectWithTag(Tags.Ball);

		playerRoomDetector = playerGO.GetComponent<RoomDetector>();
		ballRoomDetector = ballGO.GetComponent<RoomDetector>();
	}

	private void Update() {
		playerCamTf = playerCamGO.transform;

		Vector3 offset = playerCamTf.position - playerRoomDetector.CurrentRoomTf.position;

		BoxCollider2D playerRoomBC = playerRoomDetector.CurrentRoomTf.GetComponent<BoxCollider2D>();
		BoxCollider2D ballRoomBC = ballRoomDetector.CurrentRoomTf.GetComponent<BoxCollider2D>();

		// Difference in scale of 2 rooms
		float scaleFactorX = ballRoomBC.size.x / playerRoomBC.size.x;
		float scaleFactorY = ballRoomBC.size.y / playerRoomBC.size.y;

		GetComponent<Camera>().orthographicSize = 5 * Mathf.Min(scaleFactorX, scaleFactorY);

		if (scaleFactorX > scaleFactorY) {
			playerTf = playerGO.transform;
			float playerOffset = (playerTf.position - playerCamTf.position).x;
			float offSetLimit = (scaleFactorX / scaleFactorY / 4f + 0.5f) * 4.5f; //not sure if correct
			if (Mathf.Abs(playerOffset) > offSetLimit) {
				playerOffset = Mathf.Sign(playerOffset) * offSetLimit;
			}
			offset += new Vector3(playerOffset, 0, 0);
		}

		gameObject.transform.position =
			new Vector3(ballRoomDetector.CurrentRoomTf.position.x + offset.x * scaleFactorX,
						ballRoomDetector.CurrentRoomTf.position.y + offset.y * scaleFactorY,
						-10f);
	}
}
