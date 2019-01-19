using UnityEngine;

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
			playerCamTf = GameObject.FindGameObjectWithTag(Tags.PlayerCamera).transform;
		}
		MoveToRoom();
	}

	private void MoveToRoom() {
		CurrentRoomTf = GetCurrentRoom();

		if (CompareTag(Tags.Player)) {
			MovePlayerCamera();
		}
	}

	public Transform GetCurrentRoom() {
		// Get collider of the current room
		Collider2D c = Physics2D.OverlapPoint(transform.position, roomLayerMask);
		return c.transform;
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
		return new Vector3(CurrentRoomTf.position.x, CurrentRoomTf.position.y, CameraDimensions.CameraDepth);
	}

	private Vector3 GetRoomSizeSubset(BoxCollider2D roomBoxCollider) {
		return new Vector3(roomBoxCollider.size.x - CameraDimensions.CameraLength, roomBoxCollider.size.y - CameraDimensions.CameraHeight, 0f);
	}

	void OnTriggerExit2D(Collider2D collision) {
		MoveToRoom();
	}
}