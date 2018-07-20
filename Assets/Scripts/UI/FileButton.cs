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

    public void OnPointerClick(PointerEventData eventData) {
        // If the parent menu is the load menu
        if (GameMenu.IsLoadMenu(parentMenu)) {

            // If the button is clicked once by the left button on the mouse
            if (eventData.button == PointerEventData.InputButton.Left &&
                eventData.clickCount == 1) {
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
            if (eventData.button == PointerEventData.InputButton.Left &&
                eventData.clickCount == 2) {
                // Load the save file with fileName
                LoadGame loadGameScript = GetComponent<LoadGame>();
                // Load the game file associated with that button
                loadGameScript.Load(nameLabel.text);
            }
        }
    }
}
