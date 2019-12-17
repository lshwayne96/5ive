using System;
using UnityEngine;

namespace Main._5ive.Model.Ladder {

	/// <summary>
	/// This script is attached to an empty gameObject
	/// at the top of a ladder.
	/// It checks to see if the player has climbed up the ladder fully,
	/// has begun descending it or is simply standing at the top.
	/// </summary>
	public class TopOfLadder : MonoBehaviour {

		private Ladder ladder;

		private BoxCollider2D ladderBC;

		public bool IsNearTop { get; private set; }

		public bool IsLadderATrigger { get; private set; }

		void Start() {
			ladder = GetComponentInParent<Ladder>();
			ladderBC = ladder.GetComponent<BoxCollider2D>();
		}

		void Update() {
			if (AtTopOfLadder()) {
				if (Input.GetKey(KeyCode.DownArrow)) {
					ladderBC.isTrigger = true;
				} else {
					ladderBC.isTrigger = false;
				}
			} else {
				ladderBC.isTrigger = true;
			}
		}

		private bool AtTopOfLadder() {
			return ladder.OutsideLadder && IsNearTop;
		}

		// When the player is standing or moving nearby the top
		private void OnTriggerStay2D(Collider2D collision) {
			if (collision.gameObject.CompareTag("Player")) {
				IsNearTop = true;
			}
		}

		// When the player has begun descending the ladder or walked away from the top
		private void OnTriggerExit2D(Collider2D collision) {
			if (collision.gameObject.CompareTag("Player")) {
				IsNearTop = false;
			}
		}

		public TopOfLadderData CacheData() {
			return new TopOfLadderData(this);
		}

		public void Restore(TopOfLadderData topOfLadderData) {
			IsNearTop = topOfLadderData.IsNearTop;
			IsLadderATrigger = topOfLadderData.IsLadderATrigger;
		}

		[Serializable]
		public class TopOfLadderData {
			public bool IsNearTop { get; private set; }

			public bool IsLadderATrigger { get; private set; }

			public TopOfLadderData(TopOfLadder topOfLadder) {
				IsNearTop = topOfLadder.IsNearTop;
				IsLadderATrigger = topOfLadder.IsLadderATrigger;
			}

			public void Restore(TopOfLadder topOfLadder) {
				topOfLadder.Restore(this);
			}
		}
	}

}