using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoadFileButtonManager : FileButtonManager {

    private Button deleteButton;
    private Button deleteAllButton;
    private Button loadButton;
    private string saveTaggedFileNameToDelete;

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
            CreateFileButtons();
        } else {
            deleteAllButton.interactable = false;

        }
    }

    public override void Load() {
        FileButton fileButton = fileButtonToActOn.GetComponent<FileButton>();
        fileButton.LoadLevel();
        loadButton.interactable = false;
    }

    public override void DeleteOne() {
        string filePath = LevelFile.ConvertToPath(saveTaggedFileNameToDelete, false);
        File.Delete(filePath);
        gameObjectPool.ReturnObject(fileButtonToActOn);

        uniqueTaggedFileNames.Remove(saveTaggedFileNameToDelete);
        buttons.Remove(fileButtonToActOn);
        EnableDisableActionButtons(false, loadButton, deleteButton);

        if (!StillHaveFiles()) {
            deleteAllButton.interactable = false;
        }
    }

    public override void DeleteAll() {
        base.DeleteAll();
        loadButton.interactable = false;
        EnableDisableActionButtons(false, deleteButton, deleteAllButton);
        ClearCache();
    }

    public override void SetFileButtonToActOn(FileButton fileButton) {
        saveTaggedFileNameToDelete = LevelFile.AddTag(fileButton.nameLabel.text);
        fileButtonToActOn = fileButton.gameObject;
        // Allow the player to click the load and delete button
        EnableDisableActionButtons(true, loadButton, deleteButton);
    }
}