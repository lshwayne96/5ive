/*
 * This script describes a button in the save and load menus.
 * The buttons in both menus are highly similar
 * and hence grouped under the same script.
 * The public variables nameLabel, levelLabel and dataTimeLabel
 * have been referenced in the prefab.
 */

using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class GameButton : MonoBehaviour, IPointerClickHandler {

	public Text NameLabel { get; private set; }
	public Text LevelLabel { get; private set; }
	public Text DateTimeLabel { get; private set; }

	private Menu parentMenu;

	/// <summary>
	///  f stands for full date/time pattern (short time)
	/// </summary>
	private const string DateTimePattern = "f";

	public void SetUp(GameButtonInfo info) {
		NameLabel.text = info.FileName;
		LevelLabel.text = info.LevelName;
		DateTimeLabel.text = info.DateTime.ToLocalTime().ToString(DateTimePattern);

		parentMenu = MenuUtil.GetSuitableMenu();
		parentMenu = transform.parent.GetComponent<Menu>();

		transform.localScale = Vector3.one;
	}

	public void OnPointerClick(PointerEventData pointerEventData) {
		if (IsSingleClick(pointerEventData)) {
			parentMenu.SetTargetButton(this);
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

	public void DeleteCorrespondingFile() {
		string levelName = NameLabel.text;
		Game.instance.DeleteLevel(levelName);
	}

	public void OverwriteCorrespondingFile() {
		string levelName = NameLabel.text;
		GameButtonInfo gameButtonInfo = Game.instance.OverrideLevel(levelName);

		DateTimeLabel.text = gameButtonInfo.DateTime.ToLocalTime().ToString(DateTimePattern);
		transform.SetAsFirstSibling();

		NotificationManager.Send(new OverwriteSuccessfulMessage());
	}

	public void LoadLevel() {
		Game.instance.LoadLevel(NameLabel.text);
	}

	public void AttachTo(Transform menu) {
		transform.SetParent(menu);
	}

	public void MoveToTopOfMenu() {
		transform.SetAsFirstSibling();
	}

	public void SetDateTime(DateTime dateTime) {
		DateTimeLabel.text = dateTime.ToUniversalTime().ToLocalTime().ToString(DateTimePattern);
	}
}