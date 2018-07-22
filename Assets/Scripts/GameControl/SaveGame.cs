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
    private FileButtonManager buttonManager;

    private void Start() {
        inputField = GetComponent<InputField>();
        player = GameObject.FindWithTag("Player");
        ball = GameObject.FindWithTag("TeleportationBall");
        // Fragile code since it breaks when the hierarchy changes
        buttonManager =
            transform.parent.parent.GetComponentInChildren<FileButtonManager>();
    }

    // Save into a new game file
    public void NewSave() {
        if (!inputField.text.Equals(System.String.Empty)) {
            Save(inputField.text);
            // Clear the input field
            inputField.text = System.String.Empty;
        }
    }

    // Overwrite an old game file
    public void Overwrite(String fileName) {
        Save(fileName);
    }

    /*
     * Caches all game data into a LevelData instance,
     * creates a new or overwrite an old game file,
     * and serialise the game data
     */
    private void Save(String fileName) {
        // Get the current scene
        Scene scene = SceneManager.GetActiveScene();
        // Cache the player data
        PlayerData playerData = CachePlayerData();
        // Cache the ball data
        BallData ballData = CacheBallData();
        // Cache the state of interactables
        InteractablesData interactablesData = CacheInteractablesData();
        // Package the game data into a LevelData instance
        LevelData levelData = new LevelData(scene, playerData, ballData, interactablesData);

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        // Directory to save file into
        Directory.CreateDirectory(GameFile.GetSaveDirectoryPath());
        // Path of the file to be used for saving
        saveFilePath = GameFile.ConvertToPath(GameFile.AddTag(fileName));
        FileStream fileStream = File.Create(saveFilePath);

        // Serialise levelData
        binaryFormatter.Serialize(fileStream, levelData);
        fileStream.Close();

        buttonManager.UpdateButtons();
    }

    private PlayerData CachePlayerData() {
        Vector2 velocity = player.GetComponent<Rigidbody2D>().velocity;
        Vector3 position = player.transform.position;
        return new PlayerData(velocity, position);
    }

    private BallData CacheBallData() {
        Vector2 velocity = ball.GetComponent<Rigidbody2D>().velocity;
        Vector3 position = ball.transform.position;
        return new BallData(velocity, position);
    }

    private InteractablesData CacheInteractablesData() {
        InteractablesData interactablesData = new InteractablesData();
        interactablesData.ScreenShot();
        return interactablesData;
    }
}
