using System;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadMenuButton : MonoBehaviour {
    public Text nameLabel;
    public Text levelLabel;
    public Text dateLabel;
    public Text timeLabel;

    // Use this for initialization
    void Start() {

    }

    public void SetUp(String fileName, int sceneBuildIndex, DateTime dateTime) {
        this.nameLabel.text = fileName;
        this.levelLabel.text = sceneBuildIndex.ToString();
        this.dateLabel.text = dateTime.Date.ToString();
        this.timeLabel.text = dateTime.TimeOfDay.ToString();
    }
}
