using System;
using UnityEngine;
using UnityEngine.UI;

public class SaveFileButttonManager : FileButtonManager {

    private Button overwriteButton;

    public override void Initialise() {
        base.Initialise();

        overwriteButton = GameObject.FindGameObjectWithTag("OverwriteButton").GetComponent<Button>();
        actionButtons = new Button[] { overwriteButton };
    }

    public override void UpdateButtons() {
        if (HaveFiles()) {
            DeleteOldFileButtons();
            CreateFileButtons();
        } else {
            DeleteAll();
            fileButtons.Clear();
        }
    }

    private void DeleteOldFileButtons() {
        foreach (String deletedTaggedFileName in deletedTaggedFileNames) {
            if (fileButtons.ContainsKey(deletedTaggedFileName)) {
                gameObjectPool.ReturnObject(fileButtons[deletedTaggedFileName]);
                fileButtons.Remove(deletedTaggedFileName);
            }
        }
        deletedTaggedFileNames.Clear();
    }

    public override void Overwrite() {
        FileButton fileButton = fileButtonGOToActOn.GetComponent<FileButton>();
        fileButton.OverwriteFile();

        String taggedFileName = LevelFile.AddTag(fileButton.nameLabel.text);
        String dateTimeText = fileButton.dateTimeLabel.text;
        try {
            modifiedTaggedFileNamesAndDateTime.Add(taggedFileName, DateTime.Parse(dateTimeText));
        } catch (ArgumentException) {
            modifiedTaggedFileNamesAndDateTime.Remove(taggedFileName);
            modifiedTaggedFileNamesAndDateTime.Add(taggedFileName, DateTime.Parse(dateTimeText));
        }

        overwriteButton.interactable = false;
    }

    public override void SetFileButtonToActOn(FileButton fileButton) {
        taggedFileNameToActOn = LevelFile.AddTag(fileButton.nameLabel.text);
        fileButtonGOToActOn = fileButton.gameObject;
        overwriteButton.interactable = true;
    }
}
