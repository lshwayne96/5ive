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
    private FileButtonManager buttonManager;
    private Temp temp;

    private GameObject player;
    private GameObject ball;

    //private GameObject[] leverGameObjects;
    private int numLevers;
    private Lever[] levers;

    private GameObject[] standButtonGameObjects;
    private int numStandButtons;
    private StandButton[] standButtons;

    private void Start() {
        inputField = GetComponent<InputField>();
        // Fragile code since it breaks when the hierarchy changes
        buttonManager = transform.parent.parent.GetComponentInChildren<FileButtonManager>();
        GameObject tempObject = GameObject.FindGameObjectWithTag("Temp");
        if (tempObject)
        {
            temp = tempObject.GetComponent<Temp>();
        }

        player = GameObject.FindWithTag("Player");
        ball = GameObject.FindWithTag("TeleportationBall");

        /*
        leverGameObjects = GameObject.FindGameObjectsWithTag("Lever");
        numLevers = leverGameObjects.Length;
        levers = new Lever[numLevers];
        for (int i = 0; i < numLevers; i++) {
            levers[i] = leverGameObjects[i].GetComponent<Lever>();
        }
        */
        if (temp)
        {
            levers = temp.Return();
            numLevers = levers.Length;
        }

        standButtonGameObjects = GameObject.FindGameObjectsWithTag("StandButton");
        numStandButtons = standButtonGameObjects.Length;
        standButtons = new StandButton[numStandButtons];
        for (int i = 0; i < numStandButtons; i++) {
            standButtons[i] = standButtonGameObjects[i].GetComponent<StandButton>();
        }
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
        Scene scene = SceneManager.GetActiveScene();

        PlayerData playerData = CachePlayerData();
        BallData ballData = CacheBallData();

        LeverData[] leverDatas = CacheLeverData();

        StandButtonData[] standButtonDatas = CacheStandButtonData();

        // Package the game data into a LevelData instance
        LevelData levelData = new LevelData(scene, playerData,
                                            ballData, leverDatas,
                                            standButtonDatas);

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        // Directory to save file into
        Directory.CreateDirectory(GameFile.GetSaveDirectoryPath());
        // Path of the file to be used for saving
        string saveFilePath = GameFile.ConvertToPath(GameFile.AddTag(fileName));
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

    private LeverData[] CacheLeverData() {
        LeverData[] leverDatas = new LeverData[numLevers];
        for (int i = 0; i < numLevers; i++) {
            leverDatas[i] = levers[i].CacheData();
        }
        return leverDatas;
    }

    private StandButtonData[] CacheStandButtonData() {
        StandButtonData[] standButtonDatas = new StandButtonData[numStandButtons];
        for (int i = 0; i < numStandButtons; i++) {
            standButtonDatas[i] = standButtons[i].CacheData();
        }
        return standButtonDatas;
    }
}