/*
 * This script is used to save the game.
 * It requires an InputField gameObject to be ragged to inputField.
 */

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class SaveGame : MonoBehaviour {

    public InputField inputField;
    private String saveFilePath;
    private String saveDirectoryPath;
    private GameObject player;
    private GameObject ball;

    private void Start() {
        saveDirectoryPath = Application.persistentDataPath + "/Saved Games";
        player = GameObject.FindWithTag("Player");
        ball = GameObject.FindWithTag("TeleportationBall");
    }

    // Serialise the game data and save it into a file as named by the user
    public void Save() {
        //Debug.Log("In Save()");

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        // Directory to save file into
        Directory.CreateDirectory(saveDirectoryPath);
        // Path of the file to be used for saving
        saveFilePath = saveDirectoryPath + "/" + inputField.text + ".dat";
        FileStream fileStream = File.Create(saveFilePath);
        // Clear the input field
        inputField.text = "";

        // Get the current scene
        Scene scene = SceneManager.GetActiveScene();
        // Package the game data into a LevelData instance
        LevelData levelData = new LevelData(scene, player, ball);

        // Serialise levelData
        binaryFormatter.Serialize(fileStream, levelData);
        fileStream.Close();
    }
}
