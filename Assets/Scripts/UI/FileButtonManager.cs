using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * This script populates the Content gameObject in a ScrollView gameObject
 * with buttons that link to saved games.
 * It is called whenever the player selects the Save or Load buttons
 * in the Pause Menu.
 * It requires a gameObject pool to be dragged to gameObjectPool.
 */
public abstract class FileButtonManager : MonoBehaviour {

    // A gameObject pool where buttons are recycled
    protected GameObjectPool gameObjectPool;
    protected HashSet<GameObject> buttons;
    // fileNames contain file names that have the GameFile identifier
    protected HashSet<string> uniqueFileNames;

    protected string saveDirectoryPath;
    protected DirectoryInfo saveDirectoryInfo;

    protected GameObject fileButtonToDelete;
    protected Button deleteButton;
    protected Button deleteAllButton;

    public abstract void Initialise();
    public abstract void UpdateButtons();
    public abstract void DeleteAll();

    // Populate the Content gameObject with buttons associated with saved game files
    protected void CreateFileButtons() {
        // Get the files in the save directory
        List<FileInfo> fileInfos = new List<FileInfo>(saveDirectoryInfo.GetFiles());
        // Sort files from earliest to lastest created 
        fileInfos.Sort((x, y) => DateTime.Compare(x.LastWriteTime, y.LastWriteTime));

        foreach (FileInfo fileInfo in fileInfos) {
            CreateFileButton(fileInfo);
        }
    }

    private void CreateFileButton(FileInfo fileInfo) {
        string filePath = fileInfo.FullName;
        string fileName = fileInfo.Name;
        if (!uniqueFileNames.Contains(fileName) && GameFile.IdentifierExists(fileName)) {
            uniqueFileNames.Add(fileName);
            LevelData levelData = GameFile.Deserialise(filePath);
            SetUpFileButtonInfo(levelData, fileInfo);
        }
    }

    private void SetUpFileButtonInfo(LevelData levelData, FileInfo fileInfo) {
        // Get information to be displayed on the SaveLoadMenuButton button
        int levelNo = levelData.GetSceneBuildIndex();
        DateTime dateTime = fileInfo.LastWriteTimeUtc;

        GameObject button = gameObjectPool.GetObject();
        // Add the button to the scroll view content
        button.transform.SetParent(gameObject.transform);
        // Add the button to the top
        button.transform.SetAsFirstSibling();
        // Cache the buttons for easy deletion
        buttons.Add(button);

        FileButton fileButton = button.GetComponent<FileButton>();
        string fileName = GameFile.RemoveIdentifier(GameFile.ConvertToName(fileInfo.FullName));
        fileButton.SetUp(fileName, levelNo, dateTime);
    }

    // Delete the file associated with the button
    protected void DeleteFile(FileButton fileButton) {
        string saveFilePath =
            GameFile.ConvertToPath(GameFile.AddIdentifier(fileButton.nameLabel.text));
        File.Delete(saveFilePath);
    }

    protected bool HaveFiles() {
        FileInfo[] fileInfos = saveDirectoryInfo.GetFiles();
        foreach (FileInfo fileInfo in fileInfos) {
            string fileName = fileInfo.Name;
            if (GameFile.IdentifierExists(fileName)) {
                return true;
            }
        }

        return false;
    }
}
