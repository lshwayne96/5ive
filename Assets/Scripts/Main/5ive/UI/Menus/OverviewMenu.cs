using System.Collections.Generic;
using Main._5ive.Messaging;
using Main._5ive.Messaging.Events;
using UnityEngine;
using UnityEngine.UI;

namespace Main._5ive.UI.Menus {

	/// <summary>
	/// This script represents the overview menu in the main menu level.
	/// </summary>
	public class OverviewMenu : MonoBehaviour, ISubscriberDefault {
		private const int DefaultUnlockedLevelsCount = 1;
		private EventsCentre eventsCentre;
		private int unlockedLevelsCount;

		/// <summary>
		/// Unlock all the levels that the player has completed
		/// with the exception of the always-unlocked first level
		/// </summary>
		private void Start() {
			var levelButtons = GetComponentsInChildren<Button>();
			eventsCentre = EventsCentre.GetInstance();
			unlockedLevelsCount = DefaultUnlockedLevelsCount;
			eventsCentre.Subscribe(new Topic("UnlockedLevelsCountReply"), this);
			UnlockLevelButtons(levelButtons);
		}

		/// <summary>
		/// Unlocks all the levels that the play has completed,
		/// with the exception of the always unlocked main menu level.
		/// </summary>
		private void UnlockLevelButtons(IReadOnlyList<Button> levelButtons) {
			eventsCentre.Publish(new UnlockedLevelsCountRequestEvent());
			for (int i = 0; i < unlockedLevelsCount; i++) {
				levelButtons[i].interactable = true;
			}
		}

		public void Notify(IEvent @event) {
			UnlockedLevelsCountReplyEvent unlockedLevelsCountReplyEvent = (UnlockedLevelsCountReplyEvent) @event;
			unlockedLevelsCount = unlockedLevelsCountReplyEvent.Count;
		}
	}

}