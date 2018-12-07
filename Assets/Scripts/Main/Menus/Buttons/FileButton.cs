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

public class FileButton : MonoBehaviour, IPointerClickHandler {

	public Text nameLabel;
	public Text levelLabel;
	public Text dateTimeLabel;

	private string dateTimePattern;
	private bool isDoubleClick;

	private GameObject parentMenu;
	private FileButtonManager fileButtonManager;

	public void SetUp(string fileName, int sceneBuildIndex, DateTime dateTime) {
		nameLabel.text = fileName;
		int prefixLength = 2;
		string levelName = SceneUtil.GetLevelName(sceneBuildIndex).Substring(prefixLength);
		levelLabel.text = levelName;
		// f stands for full date/time pattern (short time)
		dateTimeLabel.text = dateTime.ToLocalTime().ToString("f");

		MenuUtil.Set(parentMenu);
		fileButtonManager = transform.parent.GetComponent<FileButtonManager>();

		transform.localScale = Vector3.one;
	}

	public void OnPointerClick(PointerEventData pointerEventData) {
		if (MenuUtil.IsLoadMenu(parentMenu)) {
			// If the button is clicked once by the left button on the mouse
			if (IsSingleClick(pointerEventData)) {
				/*
                 * Send the name of the file associated with the button to the ButtonManager instance
                 * The ButtonManager instance will then use that information to delete
                 * that file when the delete button is clicked on
                 */
				fileButtonManager.SetFileButtonToActOn(this);
			}

			// If the button is double clicked by the left button on the mouse
			if (IsDoubleClick(pointerEventData)) {
				fileButtonManager.Load();
			}

		}

		if (MenuUtil.IsSaveMenu(parentMenu)) {
			if (IsSingleClick(pointerEventData)) {
				/*
                 * Send the name of the file associated with the button to the ButtonManager instance
                 * The ButtonManager instance will then use that information to delete
                 * that file when the delete button is clicked on
                 */
				fileButtonManager.SetFileButtonToActOn(this);
			}

			// If the button is doubled clicked by the left button on the mouse
			if (IsDoubleClick(pointerEventData)) {
				fileButtonManager.Overwrite();
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

	public void DeleteFile() {
		string saveFilePath = StorageUtil.FileNameToPath(nameLabel.text, true);
		File.Delete(saveFilePath);
	}

	public void OverwriteFile() {
		string saveFilePath = StorageUtil.FileNameToPath(nameLabel.text, true);
		SaveLevel saveLevel = GetComponent<SaveLevel>();
		saveLevel.Overwrite(nameLabel.text);

		dateTimeLabel.text = File.GetLastWriteTimeUtc(saveFilePath).ToLocalTime().ToString("f");
		transform.SetAsFirstSibling();

		NotificationManager.Send(new OverwriteSuccessful());
	}

	public void LoadLevel() {
		LoadLevel loadLevel = GetComponent<LoadLevel>();
		loadLevel.Load(nameLabel.text);
	}
}
