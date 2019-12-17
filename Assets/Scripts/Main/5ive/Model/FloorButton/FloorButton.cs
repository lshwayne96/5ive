using System;
using System.Collections;
using Main._5ive.Commons;
using UnityEngine;

namespace Main._5ive.Model.FloorButton {

	/// <summary>
	/// This script represents a floor button and its interactions.
	/// </summary>
	/// <remarks>
	/// A floor button is simply a button on the floor; it depresses
	/// when it is pressed on.
	/// </remarks>
	public class FloorButton : PersistentObject {

		public GameObject[] interactables;

		private IActionable[] actionables;

		private int numActionables;

		private Vector3 startPosition;

		public Vector3 EndPosition { get; private set; }

		public Vector3 OriginalStartPosition { get; private set; }

		public Vector3 OriginalEndPosition { get; private set; }

		private Coroutine currentCoroutine;

		public Direction MovementDirection { get; private set; }

		private float startTime;

		private float journeyLength;

		private const float translationDistance_y = 0.15f;

		private const float speed = 1f;

		public float WaitDuration { get; private set; }

		public float OriginalWaitDuration { get; private set; }

		private bool hasInitialised;

		private bool toStartMoving;

		public bool IsMoving { get; private set; }

		private bool hasStartedMovingDown;

		private bool toStartWaiting;

		private bool isWaiting;

		private bool isBeingPressedDown;

		public bool IsDown { get; private set; }

		void Start() {
			if (!hasInitialised) {
				WaitDuration = 0.1f;
				OriginalWaitDuration = WaitDuration;

				startPosition = gameObject.transform.position;
				Vector3 vectorDifference = new Vector3(0, translationDistance_y, 0);
				EndPosition = startPosition - vectorDifference;

				OriginalStartPosition = startPosition;
				OriginalEndPosition = EndPosition;

				MovementDirection = Direction.Down;
			}

			numActionables = interactables.Length;
			actionables = new IActionable[numActionables];
			for (int i = 0; i < numActionables; i++) {
				actionables[i] = interactables[i].GetComponent<IActionable>();
			}

			journeyLength = Vector3.Distance(startPosition, EndPosition);
		}

		void Update() {

			if (IsMovingUp() && toStartMoving) {
				InterruptMovement();
				StartMovement();
			}

			if (toStartMoving) {
				StartMovement();
			}

			if (toStartWaiting) {
				StartWait();
			}

			if (isBeingPressedDown) {
				/* 
	             * Refresh the start time to make the stand button wait for
	             * the waitDuration even after it's not being pressed
	             */
				RefreshStartTime();
			}
		}

		private void OnCollisionEnter2D(Collision2D collision) {
			if (!isWaiting && !hasStartedMovingDown) {
				toStartMoving = true;

				// Only one movement coroutine can be started on the way down
				hasStartedMovingDown = true;
			}
		}

		private void OnCollisionStay2D(Collision2D collision) {
			// Only while waiting will the stand button be considered as being pressed
			if (isWaiting) {
				isBeingPressedDown = true;
			}
		}

		private void OnCollisionExit2D(Collision2D collision) {
			isBeingPressedDown = false;
		}

		private void StartMovement() {
			currentCoroutine = StartCoroutine(Move());
			toStartMoving = false;
		}

		private void StartWait() {
			currentCoroutine = StartCoroutine(Wait());
			toStartWaiting = false;
		}

		private void InterruptMovement() {
			StopCoroutine(currentCoroutine);
			ChangeMovementDirection();

			// Set the start position as the current position
			startPosition = transform.position;

			// Set the end position as the original end position
			EndPosition = OriginalEndPosition;
		}

		private bool IsMovingUp() {
			return IsMoving && MovementDirection == Direction.Up;
		}

		private void RefreshStartTime() {
			startTime = Time.time;
		}

		private IEnumerator Move() {
			float fracJourney = 0;
			IsMoving = true;
			startTime = Time.time;

			// Start moving
			while (fracJourney < 1) {
				/*
				if (PauseBehaviour.getInstance().IsPaused) {
					// Cache the current start position so that the movement can be resumed when unpaused
					startPosition = transform.position;
					// Refresh the start time to get accurate distance covered
					startTime = Time.time;
	
					yield return null;
				}
				*/

				float distCovered = (Time.time - startTime) * speed;
				fracJourney = distCovered / journeyLength;

				// Set our position as a fraction of the distance between the markers.
				transform.position = Vector3.Lerp(startPosition, EndPosition, fracJourney);

				yield return null;
			}

			// Stop moving
			IsMoving = false;
			ChangeMovementDirection();
			RefreshStartEndPositions();

			// The stand button has reached the bottom and will move upwards next
			if (MovementDirection == Direction.Up) {
				// Start waiting
				toStartWaiting = true;
				hasStartedMovingDown = false;
			} else {
				for (int i = 0; i < numActionables; i++) {
					actionables[i].EndAction();
				}
			}

			/*
	         * Extra setting of the boolean in response to it not being
	         * properly set back to false when the floor button experiences
	         * rapid contacts
	         */
			hasStartedMovingDown = false;
		}

		private IEnumerator Wait() {
			float currentWaitDuration = 0;
			bool hasAlreadyMinus = false;
			isWaiting = true;
			IsDown = true;
			startTime = Time.time;

			// The actionable will act when the stand button is at the bottom
			for (int i = 0; i < numActionables; i++) {
				actionables[i].StartAction();
			}

			// Start waiting
			while (currentWaitDuration < WaitDuration) {
				/*
				if (PauseBehaviour.getInstance().IsPaused) {
					if (!hasAlreadyMinus) {
						WaitDuration -= currentWaitDuration;
						hasAlreadyMinus = true;
					}
					startTime = Time.time;
					yield return null;
				}
				*/

				currentWaitDuration = Time.time - startTime;
				yield return null;
			}

			// The actionable will act when the stand button is at the bottom
			for (int i = 0; i < numActionables; i++) {
				actionables[i].EndAction();
			}

			// Reset the wait duration
			WaitDuration = OriginalWaitDuration;

			// Stop waiting and start moving up
			isWaiting = false;
			IsDown = false;
			toStartMoving = true;
		}

		private void RefreshStartEndPositions() {
			if (MovementDirection == Direction.Up) {
				startPosition = OriginalEndPosition;
				EndPosition = OriginalStartPosition;
			} else {
				startPosition = OriginalStartPosition;
				EndPosition = OriginalEndPosition;
			}
		}

		private void ChangeMovementDirection() {
			MovementDirection = MovementDirection == Direction.Up ? Direction.Down : Direction.Up;
		}

		public override State Save() {
			return new FloorButtonState(this);
		}

		public override void RestoreWith(State state) {
			FloorButtonState floorButtonState = (FloorButtonState) state;
			floorButtonState.RebuildCompoundTypes();

			startPosition = floorButtonState.prevStartPosition;
			EndPosition = floorButtonState.prevEndPosition;
			OriginalStartPosition = floorButtonState.originalStartPosition;
			OriginalEndPosition = floorButtonState.originalEndPosition;

			MovementDirection = floorButtonState.movementDirection;
			IsDown = floorButtonState.isDown;
			IsMoving = floorButtonState.isMoving;

			WaitDuration = floorButtonState.waitDuration;
			OriginalWaitDuration = floorButtonState.originalWaitDuration;

			hasInitialised = true;

			if (IsMoving) {
				toStartMoving = true;
			}

			if (IsDown) {
				isWaiting = true;
				toStartWaiting = true;
				transform.position = OriginalEndPosition;
			}
		}

		/// <summary>
		/// This class represents the data of a floor button.
		/// </summary>
		/// <remarks>
		/// It is used to restore the floor button to its previous state.
		/// </remarks>
		/// The data includes:
		/// <list type="number">
		/// <item>The previous start position</item>
		/// <item>The previous end position</item>
		/// <item>The original start position</item>
		/// <item>The original end position</item>
		/// <item>The movement direction</item>
		/// <item>And many more...</item>
		/// </list>
		[Serializable]
		public class FloorButtonState : State {

			[NonSerialized] public Vector3 prevStartPosition;

			private readonly float pSX;

			private readonly float pSY;

			private readonly float pSZ;

			[NonSerialized] public Vector3 prevEndPosition;

			private readonly float pEX;

			private readonly float pEY;

			private readonly float pEZ;

			[NonSerialized] public Vector3 originalStartPosition;

			private readonly float oSX;

			private readonly float oSY;

			private readonly float oSZ;

			private readonly float oSW;

			[NonSerialized] public Vector3 originalEndPosition;

			private readonly float oEX;

			private readonly float oEY;

			private readonly float oEZ;

			private readonly float oEW;

			public Direction movementDirection;

			public bool isDown;

			public bool isMoving;

			public float waitDuration;

			public float originalWaitDuration;

			/// <summary>
			/// Initializes a new instance of the <see cref="T:FloorButtonData"/> class.
			/// </summary>
			/// <param name="floorButton">Floor button.</param>
			public FloorButtonState(FloorButton floorButton) {
				prevStartPosition = floorButton.transform.position;
				prevEndPosition = floorButton.EndPosition;
				originalStartPosition = floorButton.OriginalStartPosition;
				originalEndPosition = floorButton.OriginalEndPosition;

				// Previous start position
				pSX = prevStartPosition.x;
				pSY = prevStartPosition.y;
				pSZ = prevStartPosition.z;

				// Previous end position
				pEX = prevEndPosition.x;
				pEY = prevEndPosition.y;
				pEZ = prevEndPosition.z;

				// Original start position
				oSX = originalStartPosition.x;
				oSY = originalStartPosition.y;
				oSZ = originalStartPosition.z;

				// Original end position
				oEX = originalEndPosition.x;
				oEY = originalEndPosition.y;
				oEZ = originalEndPosition.z;

				// Other data
				movementDirection = floorButton.MovementDirection;
				isDown = floorButton.IsDown;
				isMoving = floorButton.IsMoving;
				waitDuration = floorButton.WaitDuration;
				originalWaitDuration = floorButton.OriginalWaitDuration;
			}

			public void RebuildCompoundTypes() {
				prevStartPosition = new Vector3(pSX, pSY, pSZ);
				prevEndPosition = new Vector3(pEX, pEY, pEZ);
				originalStartPosition = new Vector3(oSX, oSY, oSZ);
				originalEndPosition = new Vector3(oEX, oEY, oEZ);
			}
		}

	}

}
