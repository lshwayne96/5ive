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

public class GameButton : MonoBehaviour, IPointerClickHandler {

	public Text NameLabel { get; private set; }
	public Text LevelLabel { get; private set; }
	public Text DateTimeLabel { get; private set; }

	private GameObject parentMenu;

	private Menu buttonManager;

	/// <summary>
	///  f stands for full date/time pattern (short time)
	/// </summary>
	private readonly string DateTimePattern = "f";

	public void SetUp(string fileName, int sceneBuildIndex, DateTime dateTime) {
		NameLabel.text = fileName;

		string levelName = LevelUtil.GetLevelName(sceneBuildIndex);
		LevelLabel.text = levelName;

		DateTimeLabel.text = dateTime.ToLocalTime().ToString(DateTimePattern);

		MenuUtil.Set(parentMenu);
		buttonManager = transform.parent.GetComponent<Menu>();

		transform.localScale = Vector3.one;
	}

	public void OnPointerClick(PointerEventData pointerEventData) {
		if (MenuUtil.IsLoadMenu(parentMenu)) {
			if (IsSingleClick(pointerEventData)) {
				buttonManager.SetTargetButton(this);
			}

			if (IsDoubleClick(pointerEventData)) {
				buttonManager.Load();
			}
		}

		if (MenuUtil.IsSaveMenu(parentMenu)) {
			if (IsSingleClick(pointerEventData)) {
				buttonManager.SetTargetButton(this);
			}

			if (IsDoubleClick(pointerEventData)) {
				buttonManager.OverwriteTargetButton();
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
		string path = StorageUtil.FileNameToPath(NameLabel.text, TagAddition.Enable);
		File.Delete(path);
	}

	public void OverwriteCorrespondingFile() {
		string path = StorageUtil.FileNameToPath(NameLabel.text, TagAddition.Enable);
		SaveLevel saveLevel = GetComponent<SaveLevel>();
		saveLevel.Overwrite(NameLabel.text);

		DateTimeLabel.text = File.GetLastWriteTimeUtc(path).ToLocalTime().ToString("f");
		transform.SetAsFirstSibling();

		NotificationManager.Send(new OverwriteSuccessfulMessage());
	}

	public void LoadLevel() {
		LoadLevel loadLevel = GetComponent<LoadLevel>();
		loadLevel.Load(NameLabel.text);
	}

	public void AttachToMenu(Transform menu) {
		transform.SetParent(menu);
	}

	public void MoveToTopOfMenu() {
		transform.SetAsFirstSibling();
	}

	public void SetDateTime(DateTime dateTime) {
		DateTimeLabel.text = dateTime.ToUniversalTime().ToLocalTime().ToString(DateTimePattern);
	}


}
