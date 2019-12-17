using System;
using Main._5ive.Commons;
using Main._5ive.Model;
using UnityEngine;

namespace Main._5ive.Logic.Navigation.Cameras {

	/// <summary>
	/// This script represents the camera pointing at the player.
	/// </summary>
	public class PlayerCamera : PersistentObject {

		private Transform playerTransform;

		private BoxCollider2D currentRoomCollider;

		private RoomDetector roomDetector;

		private Vector3 offset;

		public void SetPosition(Vector3 newPosition) {
			transform.position = new Vector3(newPosition.x, newPosition.y, CameraDimension.Depth);
		}
		
		public override State Save() {
			return new PlayerCameraState(this);
		}

		public override void RestoreWith(State state) {
			PlayerCameraState playerCameraState = (PlayerCameraState) state;
			playerCameraState.RebuildCompoundTypes();
			transform.position = playerCameraState.prevPosition;
		}

		public class PlayerCameraState : State {

			[NonSerialized]
			public Vector3 prevPosition;
			private readonly float x;
			private readonly float y;
			private readonly float z;

			public PlayerCameraState(PlayerCamera playerCamera) {
				prevPosition = playerCamera.transform.position;
				x = prevPosition.x;
				y = prevPosition.y;
				z = prevPosition.z;
			}

			public void RebuildCompoundTypes() {
				prevPosition = new Vector3(x, y, z);
			}

		}

		/*

		public float damping;

		public float lookAheadFactor;

		public float lookAheadReturnSpeed;

		public float lookAheadMoveThreshold;

		private Transform playerTf;

		private Vector3 prevPlayerPos;

		private Vector3 currentVelocity;

		private Vector3 lookAheadPos;

		private BoxCollider2D currentRoomBC;

		private RoomDetector roomDetector;

		private void Start() {
			playerTf = GameObject.FindWithTag(Tags.Player).transform;
			prevPlayerPos = playerTf.position;
			transform.parent = null;

			roomDetector = GameObject.FindWithTag(Tags.Player).GetComponent<RoomDetector>();
		}

		private void Update() {
			currentRoomBC = roomDetector.CurrentRoomTf.GetComponent<BoxCollider2D>();

			Vector3 futurePos = GetFuturePos();

			SetPos(futurePos);

			prevPlayerPos = playerTf.position;
		}

		private void UpdateLookAheadPos() {
			// only update look ahead position if accelerating or changing direction
			float moveDeltaX = (playerTf.position - prevPlayerPos).x;
			bool toUpdateLookAheadPos = Mathf.Abs(moveDeltaX) > lookAheadMoveThreshold;

			if (toUpdateLookAheadPos) {
				lookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(moveDeltaX);
			} else {
				lookAheadPos = Vector3.MoveTowards(lookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
			}
		}

		private Vector3 GetFuturePos() {
			UpdateLookAheadPos();

			float offsetX = Mathf.Abs(currentRoomBC.size.x - CameraDimension.CameraLength) / 2;
			float offsetY = Mathf.Abs(currentRoomBC.size.y - CameraDimension.CameraHeight) / 2;

			Vector3 futurePos = playerTf.position + lookAheadPos + Vector3.forward * CameraDimension.CameraDepth;
			futurePos = ModerateCameraPos(offsetX, offsetY, futurePos);
			return futurePos;
		}

		private void SetPos(Vector3 futurePos) {
			Vector3 newPos = Vector3.SmoothDamp(transform.position, futurePos, ref currentVelocity, damping);
			transform.position = newPos;
		}

		private Vector3 ModerateCameraPos(float offsetX, float offsetY, Vector3 futurePos) {
			float x = Mathf.Max(futurePos.x, currentRoomBC.transform.position.x - offsetX);
			float y = Mathf.Max(futurePos.y, currentRoomBC.transform.position.y - offsetY);
			futurePos = new Vector3(x, y, futurePos.z);

			x = Mathf.Min(futurePos.x, currentRoomBC.transform.position.x + offsetX);
			y = Mathf.Min(futurePos.y, currentRoomBC.transform.position.y + offsetY);
			futurePos = new Vector3(x, y, futurePos.z);

			return futurePos;
		}
		
		*/
	}

}
