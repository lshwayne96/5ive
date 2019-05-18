using System;
using System.Collections.Generic;
using Main.Commons.Util;
using Main.Logic;
using Main.Model.Story.Messages;
using Main.UI.InGame.Menus;
using Main.UI.InGame.Menus.Load;
using Main.UI.InGame.Menus.Save;
using Main.UI.Notification;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Main.UI.Menus.Commons.Buttons.GameButton {

	/// <summary>
	/// This script describes a button to be used in both the save and load menus.
	/// </summary>
	public class GameButton : MonoBehaviour, IPointerClickHandler {

#pragma warning disable CS0649
		[SerializeField] private Text nameLabel;

		[SerializeField] private Text levelLabel;

		[SerializeField] private Text dateTimeLabel;
#pragma warning restore CS0649

		private InGameMenu parentMenu;

		/// <summary>
		///  f stands for full date/time pattern (short time)
		/// </summary>
		private const string DateTimePattern = "f";

		public void SetUp(GameButtonInfo info) {
			nameLabel.text = info.FileName;
			levelLabel.text = info.LevelName;
			dateTimeLabel.text = info.DateTime.ToLocalTime().ToString(DateTimePattern);

			parentMenu = GetComponentInParent<InGameMenu>();

			transform.localScale = Vector3.one;
		}

		public void OnPointerClick(PointerEventData pointerEventData) {
			if (IsSingleClick(pointerEventData)) {
				parentMenu.SetTargetButton(this);
			}

			if (MenuUtil.IsSaveMenu(parentMenu)) {
				SaveInGameMenu menu = (SaveInGameMenu) parentMenu;
				if (IsDoubleClick(pointerEventData)) {
					menu.OverwriteTargetButton();
				}
			}

			if (MenuUtil.IsLoadMenu(parentMenu)) {
				LoadInGameMenu menu = (LoadInGameMenu) parentMenu;
				if (IsDoubleClick(pointerEventData)) {
					menu.LoadTargetButton();
				}
			}
		}

		private bool IsSingleClick(PointerEventData pointerEventData) {
			return pointerEventData.button == PointerEventData.InputButton.Left &&
			       pointerEventData.clickCount == 1;
		}

		private bool IsDoubleClick(PointerEventData pointerEventData) {
			return pointerEventData.button == PointerEventData.InputButton.Left &&
			       pointerEventData.clickCount == 2;
		}

		public void DeleteLinkedFile() {
			string levelName = nameLabel.text;
			Game.instance.DeleteLevel(levelName);
		}

		public void OverwriteLinkedFile() {
			string levelName = nameLabel.text;
			GameButtonInfo gameButtonInfo = Game.instance.OverrideLevel(levelName);

			dateTimeLabel.text = gameButtonInfo.DateTime.ToLocalTime().ToString(DateTimePattern);
			transform.SetAsFirstSibling();

			NotificationManager.Send(new OverwriteSuccessfulMessage());
		}

		public void LoadLevel() {
			Game.instance.LoadLevel(nameLabel.text);
		}

		public void AttachTo(Transform content) {
			transform.SetParent(content);
		}

		public void MoveToTop() {
			transform.SetAsFirstSibling();
		}

		public void SetDateTime(DateTime dateTime) {
			dateTimeLabel.text = dateTime.ToUniversalTime().ToLocalTime().ToString(DateTimePattern);
		}

		public string GetName() {
			return nameLabel.text;
		}

		public DateTime GetDateTime() {
			return DateTime.Parse(dateTimeLabel.text);
		}

		public override bool Equals(object other) {
			if (this == (GameButton) other) {
				return true;
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
			hashCode = hashCode * -1521134295 + EqualityComparer<InGameMenu>.Default.GetHashCode(parentMenu);
			return hashCode;
		}
	}

}