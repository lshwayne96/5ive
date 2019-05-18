using Main.Commons;
using UnityEngine;

namespace Main.Logic.Navigation {

	/// <summary>
	/// This script detects the room it is in.
	/// </summary>
	/// <remarks>
	/// This script is made ot be attached to both the player
	/// and ball game objects.
	/// </remarks>
	public class RoomDetector : MonoBehaviour {

		public Transform CurrentRoomTf { get; private set; }

		public LayerMask roomLayerMask;

		private Transform playerCamTf;

		private void Awake() {
			InitCurrentRoomLocation();
		}

		private void InitCurrentRoomLocation() {
			if (playerCamTf == null) {
				playerCamTf = GameObject.FindWithTag(Tags.MainCamera).transform;
			}

			MoveToRoom();
		}

		private void MoveToRoom() {
			UpdateRoom();

			if (CompareTag(Tags.Player)) {
				MovePlayerCamera();
			}
		}

		public void UpdateRoom() {
			// Get collider of the current room
			Collider2D c = Physics2D.OverlapPoint(transform.position, roomLayerMask);
			CurrentRoomTf = c.transform;
		}

		private void MovePlayerCamera() {
			Bounds b = GetCurrentRoomBounds();
			playerCamTf.position = b.ClosestPoint(playerCamTf.position);
		}

		private Bounds GetCurrentRoomBounds() {
			BoxCollider2D bc = CurrentRoomTf.GetComponent<BoxCollider2D>();

			Vector3 currentRoomCenter = GetRoomCenter();
			Vector3 currentRoomSize = GetRoomSizeSubset(bc);

			return new Bounds(currentRoomCenter, currentRoomSize);
		}

		private Vector3 GetRoomCenter() {
			return new Vector3(CurrentRoomTf.position.x, CurrentRoomTf.position.y, CameraDimension.CameraDepth);
		}

		private Vector3 GetRoomSizeSubset(BoxCollider2D roomBoxCollider) {
			return new Vector3(roomBoxCollider.size.x - CameraDimension.CameraLength,
				roomBoxCollider.size.y - CameraDimension.CameraHeight, 0f);
		}

		void OnTriggerExit2D(Collider2D collision) {
			MoveToRoom();
		}
	}

}