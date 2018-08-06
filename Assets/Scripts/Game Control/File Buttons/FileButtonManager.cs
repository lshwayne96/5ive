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
 */
public class FileButtonManager : MonoBehaviour {

    // A gameObject pool where buttons are recycled
    private GameObjectPool gameObjectPool;
    private HashSet<GameObject> buttons;
    // fileNames contain file names that have the LevelFile tag
    private HashSet<string> uniqueTaggedFileNames;

    private string saveTaggedFileNameToDelete;
    private string saveDirectoryPath;
    private DirectoryInfo saveDirectoryInfo;
    private GameObject parentMenu;

    private GameObject fileButtonToDelete;
    private Button deleteButton;
    private Button deleteAllButton;

    // Initialise the buttons associated with any save game files
    public void Initialise() {
        gameObjectPool = GameObject.FindWithTag("GameObjectPool").GetComponent<GameObjectPool>();
        buttons = new HashSet<GameObject>();
        uniqueTaggedFileNames = new HashSet<string>();

        saveDirectoryPath = LevelFile.GetSaveDirectoryPath();
        saveDirectoryInfo = Directory.CreateDirectory(saveDirectoryPath);
        parentMenu = LevelMenu.SetParentMenu(parentMenu);

        if (LevelMenu.IsLoadMenu(parentMenu)) {
            deleteButton = GameObject.FindWithTag("DeleteButton").GetComponent<Button>();
            deleteAllButton = GameObject.FindWithTag("DeleteAllButton").GetComponent<Button>();

            if (HaveFiles()) {
                deleteAllButton.interactable = true;
            } else {
                deleteAllButton.interactable = false;
            }
        }

        CreateFileButtons();
    }

    // Add new or remove old buttons immediately whenever they are added or removed
    public void UpdateButtons() {
        if (LevelMenu.IsSaveMenu(parentMenu)) {
            if (HaveFiles()) {
                CreateFileButtons();
            } else {
                DeleteAll();
                ClearCache();
            }
        } else if (LevelMenu.IsLoadMenu(parentMenu)) {
            if (HaveFiles()) {
                deleteAllButton.interactable = true;
                CreateFileButtons();
            } else {
                deleteAllButton.interactable = false;

            }
        }
    }

    // Populate the Content gameObject with buttons associated with saved game files
    private void CreateFileButtons() {
        // Get the files in the save directory
        List<FileInfo> fileInfos = new List<FileInfo>(saveDirectoryInfo.GetFiles());
        // Sort files from earliest to lastest created 
        fileInfos.Sort((x, y) => DateTime.Compare(x.LastWriteTime, y.LastWriteTime));

        foreach (FileInfo fileInfo in fileInfos) {
            CreateFileButton(fileInfo);
        }
    }

    // Creates a button associated with the fileInfo
    private void CreateFileButton(FileInfo fileInfo) {
        string filePath = fileInfo.FullName;
        string fileName = LevelFile.ConvertToName(filePath);

        if (!uniqueTaggedFileNames.Contains(fileName) && LevelFile.ContainsTag(fileName)) {
            uniqueTaggedFileNames.Add(fileName);
            LevelData sceneData = LevelFile.Deserialise<LevelData>(filePath);
            SetUpFileButtonInfo(sceneData, fileInfo);
        }
    }

    // Recycle all the buttons on the Content gameObject and delete their associated files
    public void DeleteAll() {
        foreach (GameObject button in buttons) {
            FileButton fileButton = button.GetComponent<FileButton>();
            DeleteFile(fileButton);
            // Recycle the button to the SimpleObjectPool instance
            gameObjectPool.ReturnObject(button);
        }

        if (LevelMenu.IsLoadMenu(parentMenu)) {
            deleteAllButton.interactable = false;
            deleteButton.interactable = false;
            ClearCache();
        }
    }

    // Recycle one of the buttons on the Content gameObject and delete its associated file
    public void DeleteOne() {
        if (LevelMenu.IsLoadMenu(parentMenu)) {
            File.Delete(LevelFile.ConvertToPath(saveTaggedFileNameToDelete, false));
            gameObjectPool.ReturnObject(fileButtonToDelete);

            uniqueTaggedFileNames.Remove(saveTaggedFileNameToDelete);
            buttons.Remove(fileButtonToDelete);
            deleteButton.interactable = false;

            if (!StillHaveFiles()) {
                deleteAllButton.interactable = false;
            }
        }
    }


    // Removes all references or values stored
    private void ClearCache() {
        uniqueTaggedFileNames.Clear();
        buttons.Clear();
    }

    /*
     * Retrieves a button from the game object pool and initialises it
     * with information from the levelData and fileInfo
     * */
    private void SetUpFileButtonInfo(LevelData levelData, FileInfo fileInfo) {
        // Get information to be displayed on the SaveLoadMenuButton button
        int sceneBuildIndex = levelData.GetSceneBuildIndex();
        DateTime dateTime = fileInfo.LastWriteTimeUtc;

        GameObject button = gameObjectPool.GetObject();
        // Add the button to the scroll view content
        button.transform.SetParent(transform);
        // Add the button to the top
        button.transform.SetAsFirstSibling();
        // Cache the buttons for easy deletion
        buttons.Add(button);

        FileButton fileButton = button.GetComponent<FileButton>();
        string fileName = LevelFile.RemoveTag(LevelFile.ConvertToName(fileInfo.FullName));
        fileButton.SetUp(fileName, sceneBuildIndex, dateTime);
    }

    /*
    * Cache the name of the file and the gameObject
    * attached to the FileButton instance to delete
    */
    public void SetFileButtonToDelete(FileButton fileButton) {
        saveTaggedFileNameToDelete = LevelFile.AddTag(fileButton.nameLabel.text);
        fileButtonToDelete = fileButton.gameObject;
        // Allow the player to click the delete button
        deleteButton.interactable = true;
    }


    // Delete the file associated with the button
    private void DeleteFile(FileButton fileButton) {
        string saveFilePath =
            LevelFile.ConvertToPath(fileButton.nameLabel.text, true);
        File.Delete(saveFilePath);
    }

    // Checks to see if there are still saved game files on the local machine
    private bool HaveFiles() {
        FileInfo[] fileInfos = saveDirectoryInfo.GetFiles();
        foreach (FileInfo fileInfo in fileInfos) {
            string fileName = fileInfo.Name;
            if (LevelFile.ContainsTag(fileName)) {
                return true;
            }
        }

        return false;
    }

    /*
     * Checks to see if there are still saved game files
     * by checking the local cache uniqueTaggedFileNames
     */
    private bool StillHaveFiles() {
        return uniqueTaggedFileNames.Count > 0;
    }

    public bool DoesFileExist(string fileName) {
        return uniqueTaggedFileNames.Contains(LevelFile.AddTag(fileName));
    }
}
