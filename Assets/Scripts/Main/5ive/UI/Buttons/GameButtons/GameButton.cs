using System;
using System.Collections.Generic;
using Main._5ive.Commons.Util;
using Main._5ive.Messaging;
using Main._5ive.Messaging.Events;
using Main._5ive.Model;
using Main._5ive.Model.Story.Messages;
using Main._5ive.UI.Menus;
using Main._5ive.UI.Notifications;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Main._5ive.UI.Buttons.GameButtons {

	/// <summary>
	/// This script describes a button to be used in both the save and load menus.
	/// </summary>
	public class GameButton : MonoBehaviour, IPointerClickHandler {
		public Text nameLabel;
		public Text levelLabel;
		public Text dateTimeLabel;

		private EventsCentre eventsCentre;

		private GameButtonState gameButtonState;

		private Menu parentMenu;

		/// <summary>
		///  f stands for full date/time pattern (short time)
		/// </summary>
		private const string DateTimePattern = "f";

		public void Start() {
			eventsCentre = EventsCentre.GetInstance();
		}

		public void RestoreWith(State state) {
			gameButtonState = (GameButtonState) state;
			nameLabel.text = gameButtonState.fileName;
			levelLabel.text = gameButtonState.levelName;
			dateTimeLabel.text = gameButtonState.dateTime.ToLocalTime().ToString(DateTimePattern);

			parentMenu = GetComponentInParent<Menu>();

			transform.localScale = Vector3.one;
		}

		public void OnPointerClick(PointerEventData pointerEventData) {
			if (IsSingleClick(pointerEventData)) {
				parentMenu.TargetButton = this;
			}

			if (MenuUtil.IsSaveMenu(parentMenu)) {
				SaveMenu menu = (SaveMenu) parentMenu;
				if (IsDoubleClick(pointerEventData)) {
					menu.OverwriteTargetButton();
				}
			}

			if (MenuUtil.IsLoadMenu(parentMenu)) {
				LoadMenu menu = (LoadMenu) parentMenu;
				if (IsDoubleClick(pointerEventData)) {
					menu.LoadGameButton();
				}
			}
		}

		private static bool IsSingleClick(PointerEventData pointerEventData) {
			return pointerEventData.button == PointerEventData.InputButton.Left
			       && pointerEventData.clickCount == 1;
		}

		private static bool IsDoubleClick(PointerEventData pointerEventData) {
			return pointerEventData.button == PointerEventData.InputButton.Left
			       && pointerEventData.clickCount == 2;
		}

		public void DeleteLinkedFile() {
			string levelName = nameLabel.text;
			eventsCentre.Publish(new FileDeleteEvent(levelName));
		}

		public void OverwriteLinkedFile() {
			string levelName = nameLabel.text;
			eventsCentre.Publish(new FileOverrideEvent(levelName));

			dateTimeLabel.text = gameButtonState.dateTime.ToLocalTime().ToString(DateTimePattern);
			transform.SetAsFirstSibling();

			NotificationManager.Send(new OverwriteSuccessfulMessage());
		}

		public void LoadLevel() {
			string levelName = nameLabel.text;
			eventsCentre.Publish(new LevelLoadEvent(levelName));
		}

		public string GetName() {
			return nameLabel.text;
		}

		public DateTime GetDateTime() {
			return DateTime.Parse(dateTimeLabel.text);
		}

		public override bool Equals(object other) {
			if (this == other) {
				return true;
			}

			if (other == null) {
				return false;
			}

			if (!(other is GameButton)) {
				return false;
			}

			GameButton gameButton = (GameButton) other;
			return nameLabel.Equals(gameButton.nameLabel)
			       && levelLabel.Equals(gameButton.levelLabel)
			       && dateTimeLabel.Equals(gameButton.dateTimeLabel);
		}

		public override int GetHashCode() {
			var hashCode = 2061284631;
			hashCode = hashCode * -1521134295 + base.GetHashCode();
			hashCode = hashCode * -1521134295 + EqualityComparer<Text>.Default.GetHashCode(nameLabel);
			hashCode = hashCode * -1521134295 + EqualityComparer<Text>.Default.GetHashCode(levelLabel);
			hashCode = hashCode * -1521134295 + EqualityComparer<Text>.Default.GetHashCode(dateTimeLabel);
			hashCode = hashCode * -1521134295 + EqualityComparer<Menu>.Default.GetHashCode(parentMenu);
			return hashCode;
		}

		private class GameButtonState : State {
			public string fileName;

			public string levelName;

			public DateTime dateTime;

			public GameButtonState(string fileName, string levelName, DateTime dateTime) {
				this.fileName = fileName;
				this.levelName = levelName;
				this.dateTime = dateTime;
			}
		}
	}

}