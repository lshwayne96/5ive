using System;
using Main._5ive.Commons;
using Main._5ive.Model;
using UnityEngine;

namespace Main._5ive.Logic.Navigation.Cameras {

	/// <summary>
	/// This script represents the camera pointing at the ball.
	/// </summary>
	public class BallCamera : PersistentObject {

		private Transform ballTransform;

		private Vector3 offset;

		private void Start() {
			ballTransform = GameObject.FindWithTag(Tags.Ball).transform;

			offset = transform.position - ballTransform.position;
		}

		private void Update() {
			transform.position = ballTransform.position + offset;
		}

		/*
		
		private GameObject ballGameObject;

		private GameObject playerGameObject;
		private Transform playerTransform;
		
		private GameObject playerCameraGameObject;
		private Transform playerCameraTransform;

		private RoomDetector playerRoomDetector;
		private RoomDetector ballRoomDetector;

		private void Start() {
			ballGameObject = GameObject.FindWithTag(Tags.Ball);
			
			playerGameObject = GameObject.FindWithTag(Tags.Player);
			playerCameraGameObject = GameObject.FindWithTag(Tags.MainCamera);

			playerRoomDetector = playerGameObject.GetComponent<RoomDetector>();
			ballRoomDetector = ballGameObject.GetComponent<RoomDetector>();
		}

		private void Update() {
			playerCameraTransform = playerCameraGameObject.transform;

			Vector3 offset = playerCameraTransform.position - playerRoomDetector.CurrentRoomTf.position;

			BoxCollider2D playerRoomBC = playerRoomDetector.CurrentRoomTf.GetComponent<BoxCollider2D>();
			BoxCollider2D ballRoomBC = ballRoomDetector.CurrentRoomTf.GetComponent<BoxCollider2D>();

			// Difference in scale of 2 rooms
			float scaleFactorX = ballRoomBC.size.x / playerRoomBC.size.x;
			float scaleFactorY = ballRoomBC.size.y / playerRoomBC.size.y;

			GetComponent<Camera>().orthographicSize = 5 * Mathf.Min(scaleFactorX, scaleFactorY);

			if (scaleFactorX > scaleFactorY) {
				playerTransform = playerGameObject.transform;
				float playerOffset = (playerTransform.position - playerCameraTransform.position).x;
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
		
		*/
		
		public override State Save() {
			return new BallCameraState(this);
		}

		public override void RestoreWith(State state) {
			BallCameraState ballCameraState = (BallCameraState) state;
			ballCameraState.RebuildCompoundTypes();
			transform.position = ballCameraState.prevPosition;
		}

		public class BallCameraState : State {
			
			[NonSerialized]
			public Vector3 prevPosition;
			private readonly float x;
			private readonly float y;
			private readonly float z;

			public BallCameraState(BallCamera ballCamera) {
				prevPosition = ballCamera.transform.position;
				x = prevPosition.x;
				y = prevPosition.y;
				z = prevPosition.z;
			}

			public void RebuildCompoundTypes() {
				prevPosition = new Vector3(x, y, z);
			}
		}
	}

}
