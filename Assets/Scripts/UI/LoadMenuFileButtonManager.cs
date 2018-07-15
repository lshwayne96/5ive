using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadMenuFileButtonManager : FileButtonManager {

    private string saveFileNameToDelete;

    // Initialise the buttons associated with any save game files
    public override void Initialise() {
        Debug.Log("From Load");
        gameObjectPool = GameObject.FindWithTag("GameObjectPool").GetComponent<GameObjectPool>();
        buttons = new HashSet<GameObject>();
        uniqueFileNames = new HashSet<string>();

        saveDirectoryPath = GameFile.GetSaveDirectoryPath();
        saveDirectoryInfo = Directory.CreateDirectory(saveDirectoryPath);

        // Initialise the delete and delete all buttons only if the parent menu is the load menu
        deleteButton = GameObject.FindWithTag("DeleteButton").GetComponent<Button>();
        deleteAllButton = GameObject.FindWithTag("DeleteAllButton").GetComponent<Button>();

        if (HaveFiles()) {
            deleteAllButton.interactable = true;
        } else {
            deleteAllButton.interactable = false;
        }

        CreateFileButtons();
    }

    // Add new or remove old buttons immediately whenever they are added or removed
    public override void UpdateButtons() {
        if (StillHaveFiles()) {
            deleteAllButton.interactable = true;
        } else {
            deleteAllButton.interactable = false;
        }

        CreateFileButtons();
    }

    // Recycle all the buttons on the Content gameObject and delete their associated files
    public override void DeleteAll() {
        foreach (GameObject button in buttons) {
            FileButton fileButton = button.GetComponent<FileButton>();
            // Delete the file associated with the button
            DeleteFile(fileButton);
            // Recycle the button to the SimpleObjectPool instance
            gameObjectPool.ReturnObject(button);
        }

        deleteAllButton.interactable = false;
        uniqueFileNames.Clear();
        buttons.Clear();
    }

    // Recycle one of the buttons on the Content gameObject and delete its associated file
    public void DeleteOne() {
        File.Delete(GameFile.ConvertToPath(saveFileNameToDelete));
        gameObjectPool.ReturnObject(fileButtonToDelete);

        uniqueFileNames.Remove(saveFileNameToDelete);
        buttons.Remove(fileButtonToDelete);
        deleteButton.interactable = false;

        if (!StillHaveFiles()) {
            deleteAllButton.interactable = false;
        }
    }

    /*
     * Cache the name of the file and the gameObject
     * attached to the FileButton instance to delete
     */
    public void SetFileButtonToDelete(FileButton fileButton) {
        saveFileNameToDelete = GameFile.AddIdentifier(fileButton.nameLabel.text);
        fileButtonToDelete = fileButton.gameObject;
        // Allow the player to click the delete button
        deleteButton.interactable = true;
    }

    private bool StillHaveFiles() {
        return uniqueFileNames.Count > 0;
    }
}
