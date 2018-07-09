using System.IO;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/*
 * This script populates the Content gameObject in a ScrollView gameObject
 * with buttons that link to saved games.
 * It is called whenever the player selects the Save or Load buttons
 * in the Pause Menu.
 * It requires a gameObject pool to be dragged to gameObjectPool.
 */
public class ButtonManager : MonoBehaviour {

    // A gameObject pool where buttons are recycled
    public SimpleObjectPool gameObjectPool;
    private List<string> fileNames;
    private DirectoryInfo directoryInfo;
    private string saveDirectoryPath;

    void Start() {
        //Debug.Log("In ButtonManager");

        saveDirectoryPath = Application.persistentDataPath + "/Saved Games";
        directoryInfo = Directory.CreateDirectory(saveDirectoryPath);
        FileInfo[] fileInfos = directoryInfo.GetFiles();

        foreach (FileInfo fileInfo in fileInfos) {
            // Get the path of the file
            string filePath = fileInfo.FullName;
            string fileName = fileInfo.Name;

            if (fileName != ".DS_Store") {
                // Get the fileName without the extension
                fileName = fileName.Substring(0, fileName.Length - 4);
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream fileStream = File.Open(filePath, FileMode.Open);
                // Get the levelData object serialised in the file
                LevelData levelData = (LevelData)binaryFormatter.Deserialize(fileStream);
                fileStream.Close();

                /*
                 * Get information to be displayed on the SaveLoadMenuButton button
                 */
                int levelNo = levelData.GetSceneBuildIndex();
                DateTime dateTime = fileInfo.LastWriteTimeUtc;

                // Get a button from the pool
                GameObject button = gameObjectPool.GetObject();

                // Add the button to the scroll view content
                button.transform.SetParent(gameObject.transform);

                SaveLoadMenuButton saveloadMenuButton = button.GetComponent<SaveLoadMenuButton>();
                saveloadMenuButton.SetUp(fileName, levelNo, dateTime);
            }
        }
    }
}
