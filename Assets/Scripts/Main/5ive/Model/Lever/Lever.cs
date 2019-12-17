using System;
using System.Collections;
using Main._5ive.Commons;
using UnityEngine;

namespace Main._5ive.Model.Lever {

	/// <summary>
	/// This script represents a lever and its interactions.
	/// </summary>
	public class Lever : PersistentObject {

		public GameObject interactable;

		public float speed;

		private Quaternion startRotation;

		public Quaternion EndRotation { get; private set; }

		public Quaternion OriginalStartRotation { get; private set; }

		public Quaternion OriginalEndRotation { get; private set; }

		private Coroutine currentCoroutine;

		public Direction MovementDirection { get; private set; }

		public bool HasSwitchedRotation { get; private set; }

		private bool interactableState;

		private bool canPullLever;

		private bool toResumeRotation;

		public bool IsRotating { get; private set; }

		private bool hasFinishedRotating;

		private bool hasInitialised;

		private const float angleOfRotation = 90f;

		private float startTime;

		private float journeyLength;


		void Start() {
			Vector3 angleDifference = Vector3.back * angleOfRotation;
			Vector3 currentAngle = transform.eulerAngles;
			Vector3 targetAngle = transform.eulerAngles + angleDifference;

			/*
	         * The variables here are stored in the lever data
	         * so no need to initialise them again
	         * Initialising them again also overwrites the previous lever state
	         */
			if (!hasInitialised) {
				startRotation = transform.rotation;
				Vector3 eulerAngles = transform.eulerAngles;
				EndRotation = Quaternion.Euler(eulerAngles + angleDifference);

				OriginalStartRotation = startRotation;
				OriginalEndRotation = EndRotation;

				// Default movement direction initially since all levers start tilting to the left
				MovementDirection = Direction.Right;
			}

			speed = 300f;
			journeyLength = Vector3.Distance(targetAngle, currentAngle);

			/*
	         * It is possible that a number of levers control
	         * a gameObject in unison. In that case, no gameObject
	         * will be attached as an interactable.
	         * The InteractionManager will manage their collective
	         * interaction.
	         */
			if (interactable) {
				interactableState = interactable.activeSelf;
			}
		}

		/* 
	     * If the player is within the collider boundaries of the lever
	     * and the R key is pressed, the lever will rotate and
	     * and the interactable gameObject will disappear.
	    */
		void Update() {
			if (LeverIsPulled()) {
				// Start time for the rotation coroutine
				startTime = Time.time;

				if (IsRotating) {
					InterruptRotation();

					// Set the start rotation as the current rotation
					startRotation = transform.rotation;
					SetEndRotation();
					StartRotation();
				} else {
					IsRotating = true;
					StartRotation();
				}
			}

			// Resuming rotation from a saved game
			if (toResumeRotation) {
				ResumeRotation();
				StartRotation();
			}
		}

		private void StartRotation() {
			currentCoroutine = StartCoroutine(Rotate());
		}

		private bool LeverIsPulled() {
			return canPullLever && Input.GetKeyUp(KeyCode.R);
		}

		private void InterruptRotation() {
			StopCoroutine(currentCoroutine);

			// Set the lever to rotate in the opposite direction
			ChangeMovementDirection();
		}

		private void SetEndRotation() {
			// Set the end rotation, depending on the movement direction
			EndRotation = MovementDirection == Direction.Left ? OriginalStartRotation : OriginalEndRotation;
		}

		// When the player enters the lever
		private void OnTriggerEnter2D(Collider2D collision) {
			if (collision.gameObject.CompareTag(Tags.Player)) {
				canPullLever = true;
			}
		}

		// When the player leaves the lever
		private void OnTriggerExit2D(Collider2D collision) {
			if (collision.gameObject.CompareTag(Tags.Player)) {
				canPullLever = false;
			}
		}

		private IEnumerator Rotate() {
			float fracJourney = 0;

			// The lever cannot be both rotating and have its rotation switched simultaneously
			HasSwitchedRotation = false;

			while (fracJourney < 1) {
				/*
	            if (PauseBehaviour.IsPaused) {
	                // Cache the current startRotation so that the rotation can be resumed when unpaused
	                startRotation = transform.rotation;
	                // Refresh the start time to get accurate distance covered
	                startTime = Time.time;
	                yield return null;
	            }
	            */

				float distCovered = (Time.time - startTime) * speed;
				fracJourney = distCovered / journeyLength;
				transform.rotation = Quaternion.Slerp(startRotation, EndRotation, fracJourney);
				yield return null;
			}

			IsRotating = false;
			ChangeInteractableState();
			ChangeHasSwitchedRotation();

			ChangeMovementDirection();
			RefreshStartEndRotations();
		}

		private void RefreshStartEndRotations() {
			if (MovementDirection == Direction.Left) {
				startRotation = OriginalEndRotation;
				EndRotation = OriginalStartRotation;
			} else {
				startRotation = OriginalStartRotation;
				EndRotation = OriginalEndRotation;
			}
		}

		// Controls the interactable assigned to the lever
		private void ChangeInteractableState() {
			if (interactable) {
				interactableState = !interactableState;
				interactable.SetActive(interactableState);
			}
		}

		private void ChangeMovementDirection() {
			MovementDirection = MovementDirection == Direction.Right ? Direction.Left : Direction.Right;
		}

		private void ChangeHasSwitchedRotation() {
			HasSwitchedRotation = MovementDirection == Direction.Right ? true : false;
		}

		public override State Save() {
			return new LeverState(this);
		}

		public override void RestoreWith(State state) {
			LeverState leverState = (LeverState) state;
			leverState.RebuildCompoundTypes();

			startRotation = leverState.prevStartRotation;
			EndRotation = leverState.prevEndRotation;
			OriginalStartRotation = leverState.originalStartRotation;
			OriginalEndRotation = leverState.originalEndRotation;
			MovementDirection = leverState.movementDirection;
			HasSwitchedRotation = leverState.hasSwitchedRotation;
			IsRotating = leverState.isRotating;
			hasInitialised = true;

			if (IsRotating) {
				toResumeRotation = true;
			}

			if (HasSwitchedRotation) {
				SwitchRotation();
			}
		}

		private void ResumeRotation() {
			IsRotating = true;

			// Refresh the start time for the rotation coroutine
			startTime = Time.time;
			toResumeRotation = false;
		}

		private void SwitchRotation() {
			transform.rotation = startRotation;
			ChangeInteractableState();
		}

		/// <summary>
		/// This class represents the data of a lever.
		/// </summary>
		/// <remarks>
		/// It is used to restore the lever to its previous state.
		/// </remarks>
		/// The data includes:
		/// <list type="number">
		/// <item>The previous start rotation</item>
		/// <item>The previous end rotation</item>
		/// <item>The original start rotation</item>
		/// <item>The original end rotation</item>
		/// </list>
		[Serializable]
		public class LeverState : State {

			[NonSerialized] public Quaternion prevStartRotation;

			private readonly float pSX;

			private readonly float pSY;

			private readonly float pSZ;

			private readonly float pSW;

			[NonSerialized] public Quaternion prevEndRotation;

			private readonly float pEX;

			private readonly float pEY;

			private readonly float pEZ;

			private readonly float pEW;

			[NonSerialized] public Quaternion originalStartRotation;

			private readonly float oSX;

			private readonly float oSY;

			private readonly float oSZ;

			private readonly float oSW;

			[NonSerialized] public Quaternion originalEndRotation;

			private readonly float oEX;

			private readonly float oEY;

			private readonly float oEZ;

			private readonly float oEW;

			public Direction movementDirection;

			public bool hasSwitchedRotation;

			public bool isRotating;

			/// <summary>
			/// Initializes a new instance of the <see cref="T:LeverData"/> class.
			/// </summary>
			/// <param name="lever">Lever.</param>
			public LeverState(Lever lever) {
				prevStartRotation = lever.transform.rotation;
				prevEndRotation = lever.EndRotation;
				originalStartRotation = lever.OriginalStartRotation;
				originalEndRotation = lever.OriginalEndRotation;

				// Previous start rotation
				pSX = prevStartRotation.x;
				pSY = prevStartRotation.y;
				pSZ = prevStartRotation.z;
				pSW = prevStartRotation.w;

				// Previous end rotation
				pEX = prevEndRotation.x;
				pEY = prevEndRotation.y;
				pEZ = prevEndRotation.z;
				pEW = prevEndRotation.w;

				// Original start rotation
				oSX = originalStartRotation.x;
				oSY = originalStartRotation.y;
				oSZ = originalStartRotation.z;
				oSW = originalStartRotation.w;

				// Original end rotation
				oEX = originalEndRotation.x;
				oEY = originalEndRotation.y;
				oEZ = originalEndRotation.z;
				oEW = originalEndRotation.w;

				movementDirection = lever.MovementDirection;
				hasSwitchedRotation = lever.HasSwitchedRotation;
				isRotating = lever.IsRotating;
			}

			public void RebuildCompoundTypes() {
				prevStartRotation = new Quaternion(pSX, pSY, pSZ, pSW);
				prevEndRotation = new Quaternion(pEX, pEY, pEZ, pEW);
				originalStartRotation = new Quaternion(oSX, oSY, oSZ, oSW);
				originalEndRotation = new Quaternion(oEX, oEY, oEZ, oEW);
			}
		}
	}

}
