using Main._5ive.Commons;
using Main._5ive.Model.Ball;
using Main._5ive.Model.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main._5ive.Logic.Navigation {

	/// <summary>
	/// This script allows both the player and ball to teleport
	/// to each other's rooms while maintaining their relative positions.
	/// </summary>
	/// <remarks>
	/// This script is to be attached to the player game object.
	/// </remarks>
	public class Teleportation : MonoBehaviour {

		private GameObject playerCameraGameObject;

		private MeshRenderer preview;

		private Player player;

		private Ball ball;

		private float startTime;

		private const float PreviewDuration = 3f;

		private bool hasPreviewExpired;

		private bool doesSceneAllowTeleportation;

		/// <summary>
		/// A location is a subset of a scene.
		/// In this case, the location is a room.
		/// </summary>
		private bool doesLocationAllowTeleportation;

		void Start() {
			playerCameraGameObject = GameObject.FindWithTag(Tags.MainCamera);
			preview = playerCameraGameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();

			player = GetComponent<Player>();
			ball = GameObject.FindWithTag(Tags.Ball).GetComponent<Ball>();

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
				}
			}
		}

		private void OnTriggerStay2D(Collider2D collision) {
			bool isPlayerInSpecialRoom = player.GetCurrentRoomTransform().CompareTag(Tags.SpecialRoom);
			bool isBallInSpecialRoom = ball.GetCurrentRoomTransform().CompareTag(Tags.SpecialRoom);

			if (isPlayerInSpecialRoom && isBallInSpecialRoom) {
				doesLocationAllowTeleportation = true;
			} else {
				doesLocationAllowTeleportation = false;
			}
		}

		private void Teleport() {
			Transform playerRoomTransform = player.GetCurrentRoomTransform();
			Transform ballRoomTransform = ball.GetCurrentRoomTransform();

			Vector3 playerRoomPosition = playerRoomTransform.position;
			Vector3 ballRoomPosition = ballRoomTransform.position;
			
			Vector3 posDif = ballRoomPosition - playerRoomPosition;

			Vector3 playerOffsetFromRoomCentre = transform.position - playerRoomPosition;
			Vector3 ballOffsetFromRoomCentre = ball.transform.position - ballRoomPosition;

			BoxCollider2D playerRoomCollider = playerRoomTransform.GetComponent<BoxCollider2D>();
			BoxCollider2D ballRoomCollider = ballRoomTransform.GetComponent<BoxCollider2D>();

			// Difference in scale of 2 rooms
			float scaleFactorX = playerRoomCollider.size.x / ballRoomCollider.size.x;
			float scaleFactorY = playerRoomCollider.size.y / ballRoomCollider.size.y;

			Vector3 playerDistToMove = Vector3.Scale(playerOffsetFromRoomCentre,
				new Vector3(1 / scaleFactorX - 1, 1 / scaleFactorY - 1, 0));
			transform.position += posDif + playerDistToMove;

			Vector3 ballDistToMove =
				Vector3.Scale(ballOffsetFromRoomCentre, new Vector3(scaleFactorX - 1, scaleFactorY - 1, 0));
			ball.transform.position -= posDif - ballDistToMove;
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

}