/*
 * This script describes a button in the save and load menus.
 * The buttons in both menus are highly similar
 * and hence grouped under the same script.
 * The public variables nameLabel, levelLabel, dataLabel and timeLabel
 * have been referenced in the prefab.
 */

using System;
using UnityEngine;
using UnityEngine.UI;


public class SaveLoadMenuButton : MonoBehaviour {

    public Text nameLabel;
    public Text levelLabel;
    public Text dateLabel;
    public Text timeLabel;

    private GameObject parentMenu;

    // Initialise the button
    public void SetUp(String fileName, int sceneBuildIndex, DateTime dateTime) {
        this.nameLabel.text = fileName;
        this.levelLabel.text = sceneBuildIndex.ToString();
        this.dateLabel.text = dateTime.Date.ToString();
        this.timeLabel.text = dateTime.TimeOfDay.ToString();

        // Determine if the parent of this button is the SaveMenu or the LoadMenu
        parentMenu = GameObject.FindWithTag("SaveMenu");
        if (parentMenu == null) {
            parentMenu = GameObject.FindWithTag("LoadMenu");
        }

        Button.ButtonClickedEvent buttonClickedEvent = GetComponent<Button>().onClick;
        if (parentMenu.CompareTag("SaveMenu")) {
            // Overwrite previous save file
            //GetComponent<Button>().onClick.AddListener()
        } else {
            // Load the save file with fileName
            LoadGame loadGameScript = GetComponent<LoadGame>();
            buttonClickedEvent.AddListener(delegate {
                loadGameScript.Load(fileName);
            });
        }
    }

}
