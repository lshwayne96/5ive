/*
 * This script is used to load a game.
 */

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadScene : MonoBehaviour {

    private String saveFilePath;

    // Deserialise the game data and cache them in restoreGame
    public void Load(string fileName) {
        try {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            // Path of the file to be accessed and it's data deserialised
            saveFilePath = GameFile.ConvertToPath(GameFile.AddTag(fileName));
            FileStream fileStream = File.Open(saveFilePath, FileMode.Open);
            // Deserialised data is stored into levelData
            SceneData sceneData = (SceneData)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();

            // Load the scene of the saved game
            SceneManager.LoadScene(sceneData.GetSceneBuildIndex());

            // Cache the levelData reference in restoreGame
            RestoreScene.restoreScene.Cache(sceneData);

        } catch (FileNotFoundException) {
            Debug.Log("Game file has been deleted or moved.");
        }
    }
}