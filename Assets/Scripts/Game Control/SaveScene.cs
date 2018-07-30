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

public class SaveScene : MonoBehaviour {

    private InputField inputField;
    private FileButtonManager fileButtonManager;

    private GameObject player;
    private GameObject ball;

    private Lever[] levers;
    private int numLevers;

    private StandButton[] standButtons;
    private int numStandButtons;

    private void Start() {
        inputField = GetComponent<InputField>();
        fileButtonManager =
            GameObject.FindGameObjectWithTag("PauseControl").GetComponentInChildren<FileButtonManager>();

        player = GameObject.FindWithTag("Player");
        ball = GameObject.FindWithTag("TeleportationBall");

        GameObject componentManagerGO = GameObject.FindGameObjectWithTag("ComponentManager");
        if (componentManagerGO) {
            ComponentManager componentManager = componentManagerGO.GetComponent<ComponentManager>();

            levers = componentManager.GetScripts<Lever>();
            numLevers = levers.Length;

            standButtons = componentManager.GetScripts<StandButton>();
            numStandButtons = standButtons.Length;
        }
    }

    public void NewSave() {
        if (!inputField.text.Equals(System.String.Empty)) {
            if (fileButtonManager.DoesFileExist(inputField.text)) {
                Save(inputField.text);
            } else {
                NotificationManager.Notifiy(new FileAlreadyExists());
            }

            // Clear the input field
            inputField.text = System.String.Empty;
        }
    }

    public void Overwrite(String fileName) {
        Save(fileName);
    }

    /*
     * Caches all scene data into a SceneData instance,
     * creates a new or overwrite an old game file,
     * and serialise the scene data
     */
    private void Save(String fileName) {
        Scene scene = SceneManager.GetActiveScene();

        PlayerData playerData = CachePlayerData();
        BallData ballData = CacheBallData();
        LeverData[] leverDatas = CacheLeverData();
        StandButtonData[] standButtonDatas = CacheStandButtonData();

        SceneData levelData =
            new SceneData(scene, playerData, ballData, leverDatas, standButtonDatas);

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        Directory.CreateDirectory(GameFile.GetSaveDirectoryPath());

        string saveFilePath = GameFile.ConvertToPath(GameFile.AddTag(fileName));
        FileStream fileStream = File.Create(saveFilePath);

        binaryFormatter.Serialize(fileStream, levelData);
        fileStream.Close();

        fileButtonManager.UpdateButtons();
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