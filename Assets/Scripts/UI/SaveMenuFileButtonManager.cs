using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class SaveMenuFileButtonManager : FileButtonManager {

    // Initialise the buttons associated with any save game files
    public override void Initialise() {
        Debug.Log("From Save");
        gameObjectPool = GameObject.FindWithTag("GameObjectPool").GetComponent<GameObjectPool>();
        buttons = new HashSet<GameObject>();
        uniqueFileNames = new HashSet<string>();

        saveDirectoryPath = GameFile.GetSaveDirectoryPath();
        saveDirectoryInfo = Directory.CreateDirectory(saveDirectoryPath);

        CreateFileButtons();
    }

    // Add new or remove old buttons immediately whenever they are added or removed
    public override void UpdateButtons() {
        if (!HaveFiles()) {
            DeleteAll();
            uniqueFileNames.Clear();
            buttons.Clear();
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
    }
}
