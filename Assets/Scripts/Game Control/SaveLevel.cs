/*
 * This script is used to save the game.
 * It is called when the user presses enter on the attached InputField gameObject.
 */

using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class SaveLevel : MonoBehaviour {

    private InputField inputField;
    private FileButtonManager fileButtonManager;

    private GameObject player;
    private GameObject ball;

    private Lever[] levers;
    private int numLevers;

    private StandButton[] standButtons;
    private int numStandButtons;

    private Ladder[] ladders;
    private int numLadders;

    private void Start() {
        int currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneBuildIndex == (int)Level.MainMenu) {
            fileButtonManager = transform.parent.GetComponent<FileButtonManager>();
        } else {
            fileButtonManager = GameObject.FindGameObjectWithTag("PauseControl")
                                  .GetComponentInChildren<FileButtonManager>();
        }

        player = GameObject.FindWithTag("Player");
        ball = GameObject.FindWithTag("TeleportationBall");

        GameObject componentManagerGO = GameObject.FindGameObjectWithTag("ComponentManager");
        if (componentManagerGO) {
            ComponentManager componentManager = componentManagerGO.GetComponent<ComponentManager>();

            levers = componentManager.GetScripts<Lever>();
            numLevers = levers.Length;

            standButtons = componentManager.GetScripts<StandButton>();
            numStandButtons = standButtons.Length;

            ladders = componentManager.GetScripts<Ladder>();
            numLadders = ladders.Length;
        }
    }

    public void NewSave() {
        if (!inputField.text.Equals(System.String.Empty)) {
            if (!fileButtonManager.DoesFileExist(inputField.text)) {
                Save(inputField.text);
            } else {
                MessageManager.Send(new FileAlreadyExists());
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
        LadderData[] ladderDatas = CacheLadderData();

        LevelData levelData = new LevelData(scene, playerData, ballData,
                                            leverDatas, standButtonDatas, ladderDatas);

        string saveFilePath = LevelFile.ConvertToPath(fileName, true);
        LevelFile.Serialise(saveFilePath, levelData);

        // Add new file buttons or update the information/ order of existing file buttons
        fileButtonManager.UpdateButtons();

        GameDataManager.Save(fileName, scene.buildIndex);

        // Deactvate the input field
        if (inputField) {
            inputField.DeactivateInputField();
        }
    }

    private PlayerData CachePlayerData() {
        Rigidbody2D playerRigidBody = player.GetComponent<Rigidbody2D>();
        return new PlayerData(playerRigidBody);
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

    private LadderData[] CacheLadderData() {
        LadderData[] ladderDatas = new LadderData[numLadders];
        for (int i = 0; i < numLadders; i++) {
            ladderDatas[i] = ladders[i].CacheData();
        }
        return ladderDatas;
    }

    private void OnEnable() {
        inputField = GetComponent<InputField>();
        // This script is used in both the input field game object and file button
        if (inputField) {
            inputField.ActivateInputField();
        }
    }
}