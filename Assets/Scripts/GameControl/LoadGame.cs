/*
 * This script is used to load a game.
 */

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadGame : MonoBehaviour {

    private String saveFilePath;
    private GameObject player;
    private GameObject ball;

    private void Start() {
        player = GameObject.FindWithTag("Player");
        ball = GameObject.FindWithTag("TeleportationBall");
    }

    // Deserialise the game data and cache them in restoreGame
    public void Load(string fileName) {
        try {
            //Debug.Log("In Load()");

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            // Path of the file to be accessed and it's data deserialised
            saveFilePath = GameFile.ConvertToPath(GameFile.AddTag(fileName));
            FileStream fileStream = File.Open(saveFilePath, FileMode.Open);
            // Deserialised data is stored into levelData
            LevelData levelData = (LevelData)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();

            // Cache the levelData reference in restoreGame
            RestoreGame.restoreGame.Cache(levelData);
            // Load the scene of the saved game
            SceneManager.LoadScene(levelData.GetSceneBuildIndex());

        } catch (FileNotFoundException) {
            Debug.Log("Game file has been deleted or moved.");
        }
    }
}