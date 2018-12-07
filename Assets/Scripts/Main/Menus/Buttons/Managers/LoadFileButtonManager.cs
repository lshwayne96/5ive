using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadFileButtonManager : FileButtonManager {

    private Button deleteButton;
    private Button deleteAllButton;
    private Button loadButton;

    public override void Initialise() {
        base.Initialise();

        deleteButton = GameObject.FindWithTag("DeleteButton").GetComponent<Button>();
        deleteAllButton = GameObject.FindWithTag("DeleteAllButton").GetComponent<Button>();
        loadButton = GameObject.FindWithTag("LoadButton").GetComponent<Button>();
        actionButtons = new Button[] { deleteButton, deleteAllButton, loadButton };

        if (HaveFiles()) {
            deleteAllButton.interactable = true;
        } else {
            deleteAllButton.interactable = false;
        }
    }

    public override void UpdateButtons() {
        if (HaveFiles()) {
            deleteAllButton.interactable = true;
            ModifyButtons();
            CreateFileButtons();
        } else {
            deleteAllButton.interactable = false;

        }
    }

    private void ModifyButtons() {
        List<KeyValuePair<String, DateTime>> keyValuePairs = new List<KeyValuePair<String, DateTime>>(modifiedTaggedFileNamesAndDateTime);
        keyValuePairs.Sort((pair1, pair2) => DateTime.Compare(pair1.Value, pair2.Value));

        foreach (KeyValuePair<String, DateTime> keyValuePair in keyValuePairs) {
            if (fileButtons.ContainsKey(keyValuePair.Key)) {
                FileButton fileButton = fileButtons[keyValuePair.Key].GetComponent<FileButton>();
                // Change the date and time shown on the button
                fileButton.dateTimeLabel.text = keyValuePair.Value.ToUniversalTime().ToLocalTime().ToString("f");
                fileButton.transform.SetAsFirstSibling();
            }
        }
        modifiedTaggedFileNamesAndDateTime.Clear();
    }

    public override void Load() {
        FileButton fileButton = fileButtonGOToActOn.GetComponent<FileButton>();
        fileButton.LoadLevel();
        loadButton.interactable = false;
    }

    public override void DeleteOne() {
        string filePath = StorageUtil.FileNameToPath(taggedFileNameToActOn, false);
        File.Delete(filePath);
        gameObjectPool.ReturnObject(fileButtonGOToActOn);

        fileButtons.Remove(taggedFileNameToActOn);
        deletedTaggedFileNames.Add(taggedFileNameToActOn);
        EnableDisableActionButtons(false, loadButton, deleteButton);

        if (!StillHaveFiles()) {
            deleteAllButton.interactable = false;
        }
    }

    public override void DeleteAll() {
        base.DeleteAll();
        loadButton.interactable = false;
        EnableDisableActionButtons(false, deleteButton, deleteAllButton);
        fileButtons.Clear();
    }

    public override void SetFileButtonToActOn(FileButton fileButton) {
        taggedFileNameToActOn = StorageUtil.AddTag(fileButton.nameLabel.text);
        fileButtonGOToActOn = fileButton.gameObject;
        // Allow the player to click the load and delete button
        EnableDisableActionButtons(true, loadButton, deleteButton);
    }
}
