using System.IO;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {
    public SimpleObjectPool gameObjectPool;
    private List<string> fileNames;
    private DirectoryInfo directoryInfo;
    private string saveDirectoryPath;

    // Use this for initialization
    void Start() {
        saveDirectoryPath = Application.persistentDataPath + "/Saved Games";
        directoryInfo = Directory.CreateDirectory(saveDirectoryPath);
        FileInfo[] fileInfos = directoryInfo.GetFiles();

        foreach (FileInfo fileInfo in fileInfos) {
            // Get the path of the file
            string filePath = fileInfo.FullName;
            string fileName = fileInfo.Name;

            if (fileName != ".DS_Store") {
                Debug.Log(filePath);
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                // Open the file
                FileStream fileStream = File.Open(filePath, FileMode.Open);
                // Get the levelData object serialised in the file
                LevelData levelData = (LevelData)binaryFormatter.Deserialize(fileStream);
                fileStream.Close();

                int levelNo = levelData.sceneBuildIndex;
                DateTime dateTime = fileInfo.LastWriteTimeUtc;

                // Get a button from the pool
                GameObject button = gameObjectPool.GetObject();
                Debug.Log(button);
                // Add the button to the scroll view content
                button.transform.SetParent(gameObject.transform);

                SaveLoadMenuButton saveloadMenuButton = button.GetComponent<SaveLoadMenuButton>();
                saveloadMenuButton.SetUp(fileName, levelNo, dateTime);
            }
        }
    }
}
