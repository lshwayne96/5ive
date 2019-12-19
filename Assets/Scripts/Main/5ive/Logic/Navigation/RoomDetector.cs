using Main._5ive.Commons;
using UnityEngine;

namespace Main._5ive.Logic.Navigation {

	/// <summary>
	/// This script detects the room it is in.
	/// </summary>
	/// <remarks>
	/// This script is made ot be attached to both the player
	/// and ball game objects.
	/// </remarks>
	public class RoomDetector : MonoBehaviour {
		private Transform currentRoomTransform;
		private Vector3 currentRoomPosition;

		public Transform GetCurrentRoomTransform() {
			return currentRoomTransform;
		}

		public Vector3 GetCurrentRoomPosition() {
			return currentRoomPosition;
		}

		private void OnTriggerEnter2D(Collider2D collision) {
			if (collision.gameObject.layer == (int) Layer.Room) {
				currentRoomTransform = collision.transform;
				currentRoomPosition = currentRoomTransform.position;
			}
		}

		/*

		public LayerMask roomLayerMask;

		private Transform currentRoomTransform;

		private Vector3 currentRoomPosition;

		private void Start() {
			currentRoomPosition = DetectCurrentRoom();
		}

		private Vector3 DetectCurrentRoom() {
			Bounds b = GetCurrentRoomBounds();
			Transform watchingCameraTransform = gameObject.GetComponent<IWatchable>().GetCamera().transform;
			return b.ClosestPoint(watchingCameraTransform.position);
		}

		private Bounds GetCurrentRoomBounds() {
			Collider2D currentRoomCollider = Physics2D.OverlapPoint(transform.position, roomLayerMask);
			currentRoomTransform = currentRoomCollider.transform;

			Vector3 currentRoomCenter = GetRoomCenter(currentRoomTransform);

			BoxCollider2D bc = currentRoomTransform.GetComponent<BoxCollider2D>();
			Vector3 currentRoomSize = GetRoomSize(bc);

			return new Bounds(currentRoomCenter, currentRoomSize);
		}

		private Vector3 GetRoomCenter(Transform t) {
			Vector3 p = t.position;
			return new Vector3(p.x, p.y, CameraDimension.CameraDepth);
		}

		private Vector3 GetRoomSize(BoxCollider2D roomBoxCollider) {
			Vector2 currentRoomSize = roomBoxCollider.size;
			return new Vector3(currentRoomSize.x - CameraDimension.CameraLength,
				currentRoomSize.y - CameraDimension.CameraHeight, 0f);
		}

		public Transform GetCurrentRoomTransform() {
			return currentRoomTransform;
		}

		public Vector3 GetCurrentRoomPosition() {
			return currentRoomPosition;
		}

		private void OnTriggerExit2D(Collider2D collision) {
			currentRoomPosition = DetectCurrentRoom();
			print("new room");
		}

		*/

		/*

		public LayerMask roomLayerMask;

		public Transform CurrentRoomTf { get; private set; }

		private Transform playerCamTf;

		private void Start() {
			InitCurrentRoomLocation();
		}

		private void InitCurrentRoomLocation() {
			playerCamTf = GameObject.FindWithTag(Tags.MainCamera).transform;
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

		private void OnTriggerExit2D(Collider2D collision) {
			MoveToRoom();
		}

		*/
	}

}