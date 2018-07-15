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

public class FileButton : MonoBehaviour {

    // Properties of a FileButton instance
    public Text nameLabel;
    public Text levelLabel;
    public Text dateTimeLabel;

    // Initialise the button
    public void SetUp(string fileName, int sceneBuildIndex, DateTime dateTime) {
        this.nameLabel.text = fileName;
        this.levelLabel.text = sceneBuildIndex.ToString();
        // f stands for full date/time pattern (short time)
        this.dateTimeLabel.text = dateTime.ToLocalTime().ToString("f");
    }
}
