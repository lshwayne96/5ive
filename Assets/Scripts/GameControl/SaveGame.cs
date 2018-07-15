/*
 * This script is used to save the game.
 * It is called when the user presses enter on the attached InputField gameObject.
 */

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class SaveGame : MonoBehaviour {

    private InputField inputField;
    private String saveFilePath;
    private GameObject player;
    private GameObject ball;
    private SaveMenuFileButtonManager buttonManager;

    private void Start() {
        inputField = GetComponent<InputField>();
        player = GameObject.FindWithTag("Player");
        ball = GameObject.FindWithTag("TeleportationBall");
        // Fragile code since it breaks when the hierarchy changes
        buttonManager =
            transform.parent.parent.GetComponentInChildren<SaveMenuFileButtonManager>();
    }

    // Serialise the game data and save it into a file as named by the user
    public void Save() {
        //Debug.Log("In Save()");

        if (inputField.text != "") {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            // Directory to save file into
            Directory.CreateDirectory(GameFile.GetSaveDirectoryPath());
            // Path of the file to be used for saving
            saveFilePath = GameFile.ConvertToPath(GameFile.AddIdentifier(inputField.text));
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

            buttonManager.UpdateButtons();
        }
    }
}
