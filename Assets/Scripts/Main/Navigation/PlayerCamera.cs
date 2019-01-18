using UnityEngine;

/// <summary>
/// This script represents the camera pointing at the player.
/// </summary>
public class PlayerCamera : MonoBehaviour {

	public float damping;
	public float lookAheadFactor;
	public float lookAheadReturnSpeed;
	public float lookAheadMoveThreshold;

	private Transform playerTf;
	private Vector3 lastPlayerTfPosition;
	private Vector3 currentVelocity;
	private Vector3 lookAheadPos;

	private BoxCollider2D currentRoomBoxCollider;

	private void Start() {
		playerTf = GameObject.FindWithTag("Player").transform;
		lastPlayerTfPosition = playerTf.position;
		transform.parent = null;
	}

	private void Update() {
		currentRoomBoxCollider = CurrentRoomSetter.currentPlayerRoomTf.GetComponent<BoxCollider2D>();

		float offset_x = Mathf.Abs(currentRoomBoxCollider.size.x - CameraDimensions.CameraLength) / 2;
		float offset_y = Mathf.Abs(currentRoomBoxCollider.size.y - CameraDimensions.CameraHeight) / 2;

		SetLookAheadPos();

		Vector3 potentialFuturePlayerPosition = GetPotentialFuturePlayerPosition(offset_x, offset_y);
		SetCameraPosition(potentialFuturePlayerPosition);

		lastPlayerTfPosition = playerTf.position;
	}

	private void SetLookAheadPos() {
		// only update lookahead pos if accelerating or changed direction
		float xMoveDelta = (playerTf.position - lastPlayerTfPosition).x;
		bool updateLookAheadPlayerTf = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

		if (updateLookAheadPlayerTf) {
			lookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
		} else {
			lookAheadPos = Vector3.MoveTowards(lookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
		}
	}

	private Vector3 GetPotentialFuturePlayerPosition(float offset_x, float offset_y) {
		Vector3 potentialFuturePlayerPosition = playerTf.position + lookAheadPos + Vector3.forward * CameraDimensions.CameraDepth;
		potentialFuturePlayerPosition = KeepCameraWithinRoomBounds(offset_x, offset_y, potentialFuturePlayerPosition);
		return potentialFuturePlayerPosition;
	}

	private void SetCameraPosition(Vector3 potentialFuturePlayerPosition) {
		Vector3 newPos = Vector3.SmoothDamp(transform.position, potentialFuturePlayerPosition, ref currentVelocity, damping);
		transform.position = newPos;
	}

	private Vector3 KeepCameraWithinRoomBounds(float offset_x, float offset_y, Vector3 potentialFuturePlayerPosition) {
		potentialFuturePlayerPosition = new Vector3(Mathf.Max(potentialFuturePlayerPosition.x, currentRoomBoxCollider.transform.position.x - offset_x),
					Mathf.Max(potentialFuturePlayerPosition.y, currentRoomBoxCollider.transform.position.y - offset_y),
					potentialFuturePlayerPosition.z);

		potentialFuturePlayerPosition = new Vector3(Mathf.Min(potentialFuturePlayerPosition.x, currentRoomBoxCollider.transform.position.x + offset_x),
			Mathf.Min(potentialFuturePlayerPosition.y, currentRoomBoxCollider.transform.position.y + offset_y),
			potentialFuturePlayerPosition.z);

		return potentialFuturePlayerPosition;
	}
}
