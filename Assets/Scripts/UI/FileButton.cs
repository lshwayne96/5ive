/*
 * This script describes a button in the save and load menus.
 * The buttons in both menus are highly similar
 * and hence grouped under the same script.
 * The public variables nameLabel, levelLabel and dataTimeLabel
 * have been referenced in the prefab.
 */

using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FileButton : MonoBehaviour, IPointerClickHandler {

    // Properties of a FileButton instance
    public Text nameLabel;
    public Text levelLabel;
    public Text dateTimeLabel;

    private string dateTimePattern;
    private bool isDoubleClick;
    private GameObject parentMenu;

    // Initialise the button
    public void SetUp(string fileName, int sceneBuildIndex, DateTime dateTime) {
        nameLabel.text = fileName;
        levelLabel.text = "Level " + sceneBuildIndex.ToString();
        // f stands for full date/time pattern (short time)
        dateTimeLabel.text = dateTime.ToLocalTime().ToString("f");
        parentMenu = GameMenu.SetParentMenu(parentMenu);
        transform.localScale = Vector3.one;
    }

    public void OnPointerClick(PointerEventData pointerEventData) {
        if (GameMenu.IsLoadMenu(parentMenu)) {

            // If the button is clicked once by the left button on the mouse
            if (IsSingleClick(pointerEventData)) {
                FileButtonManager buttonManager =
                    transform.parent.GetComponent<FileButtonManager>();
                /*
                 * Send the name of the file associated with the button to the ButtonManager instance
                 * The ButtonManager instance will then use that information to delete
                 * that file when the delete button is clicked on
                 */
                buttonManager.SetFileButtonToDelete(this);
            }

            // If the button is double clicked by the left button on the mouse
            if (IsDoubleClick(pointerEventData)) {
                LoadGame loadGame = GetComponent<LoadGame>();
                // Load the game file associated with that button
                loadGame.Load(nameLabel.text);
            }

        } else if (GameMenu.IsSaveMenu(parentMenu)) {

            // If the button is doubled clicked by the left button on the mouse
            if (IsDoubleClick(pointerEventData)) {
                SaveGame saveGame = GetComponent<SaveGame>();
                // Overwrite the old filew with new game data
                saveGame.Overwrite(nameLabel.text);
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
}
