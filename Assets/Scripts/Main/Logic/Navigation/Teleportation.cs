using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script allows both the player and ball to teleport
/// to each other's rooms while maintaining their relative positions.
/// </summary>
/// <remarks>
/// This script is to be attached to the player game object.
/// </remarks>
public class Teleportation : MonoBehaviour {

	private GameObject playerCam;
	private MeshRenderer preview;
	private Ball ballGO;

	private RoomDetector playerRoomDetector;
	private RoomDetector ballRoomDetector;

	private float startTime;
	private readonly float PreviewDuration = 3f;
	private bool hasPreviewExpired;

	private bool doesSceneAllowTeleportation;

	/// <summary>
	/// A location is a subset of a scene.
	/// In this case, the location is a room.
	/// </summary>
	private bool doesLocationAllowTeleportation;

	void Start() {
		playerCam = GameObject.FindWithTag(Tags.MainCamera);
		preview = playerCam.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();
		ballGO = GameObject.FindGameObjectWithTag(Tags.Ball).GetComponent<Ball>();

		playerRoomDetector = GetComponent<RoomDetector>();
		ballRoomDetector = ballGO.GetComponent<RoomDetector>();

		doesSceneAllowTeleportation = CheckPermission();
	}

	void Update() {
		if (CanTeleport()) {

			if (startTime - 0 < float.MinValue) {
				hasPreviewExpired = Time.time - startTime >= PreviewDuration;
				if (hasPreviewExpired) {
					preview.enabled = false;
					startTime = 0;
				}
				return;
			}

			if (Input.GetKeyDown(KeyCode.T)) {
				hasPreviewExpired = false;
				preview.enabled = true;
				startTime = Time.time;
				return;
			}

			if (Input.GetKeyUp(KeyCode.T) && !hasPreviewExpired) {
				preview.enabled = false;
				Teleport();
				return;
			}
		}
	}

	private void OnTriggerStay2D(Collider2D collision) {
		bool isPlayerInSpecialRoom = playerRoomDetector.CurrentRoomTf.CompareTag(Tags.SpecialRoom);
		bool isBallInSpecialRoom = ballRoomDetector.CurrentRoomTf.CompareTag(Tags.SpecialRoom);

		if (isPlayerInSpecialRoom && isBallInSpecialRoom) {
			doesLocationAllowTeleportation = true;
		} else {
			doesLocationAllowTeleportation = false;
		}
	}

	private void Teleport() {
		Vector3 posDif = ballRoomDetector.CurrentRoomTf.position - playerRoomDetector.CurrentRoomTf.position;

		Vector3 playerOffsetFromRoomCentre = transform.position - playerRoomDetector.CurrentRoomTf.position;
		Vector3 ballOffsetFromRoomCentre = ballGO.transform.position - ballRoomDetector.CurrentRoomTf.position;

		BoxCollider2D playerRoomBC = playerRoomDetector.CurrentRoomTf.GetComponent<BoxCollider2D>();
		BoxCollider2D ballRoomBC = ballRoomDetector.CurrentRoomTf.GetComponent<BoxCollider2D>();

		// Difference in scale of 2 rooms
		float scaleFactorX = playerRoomBC.size.x / ballRoomBC.size.x;
		float scaleFactorY = playerRoomBC.size.y / ballRoomBC.size.y;

		Vector3 playerDistToMove = Vector3.Scale(playerOffsetFromRoomCentre, new Vector3(1 / scaleFactorX - 1, 1 / scaleFactorY - 1, 0));
		transform.position += posDif + playerDistToMove;

		Vector3 ballDistToMove = Vector3.Scale(ballOffsetFromRoomCentre, new Vector3(scaleFactorX - 1, scaleFactorY - 1, 0));
		ballGO.transform.position -= posDif - ballDistToMove;
	}

	private bool CheckPermission() {
		int sceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
		return !(sceneBuildIndex == (int) LevelName.Denial ||
		sceneBuildIndex == (int) LevelName.Anger);
	}

	private bool CanTeleport() {
		/*
		if (PauseBehaviour.IsPaused) {
			return false;
		}
		*/
		return doesSceneAllowTeleportation || doesLocationAllowTeleportation;
	}
}
