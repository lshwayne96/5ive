/*
 * This script is used to save the game.
 * It is called when the user presses enter on the attached InputField gameObject.
 * 
 * This script is used in both the input field game object and file button.
 */

using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class SaveLevel : MonoBehaviour {

    private InputField inputField;
    private FileButtonManager fileButtonManager;

    private Player player;
    private Ball ball;
    private Lever[] levers;
    private StandButton[] standButtons;
    private Ladder[] ladders;
    private StoryLine[] storyLines;

    private void Start() {
        int currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneBuildIndex == (int)Level.MainMenu) {
            fileButtonManager = transform.parent.GetComponent<FileButtonManager>();
        } else {
            fileButtonManager = GameObject.FindGameObjectWithTag("FileButtonManager")
                                  .GetComponentInChildren<FileButtonManager>();
        }

        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        ball = GameObject.FindWithTag("TeleportationBall").GetComponent<Ball>();

        GameObject componentManagerGO = GameObject.FindGameObjectWithTag("ComponentManager");
        if (componentManagerGO) {
            ComponentManager componentManager = componentManagerGO.GetComponent<ComponentManager>();

            levers = componentManager.GetScripts<Lever>();
            standButtons = componentManager.GetScripts<StandButton>();
            ladders = componentManager.GetScripts<Ladder>();
            storyLines = componentManager.GetScripts<StoryLine>();
        }
    }

    public void NewSave() {
        if (!inputField.text.Equals(System.String.Empty)) {
            if (!fileButtonManager.DoesFileExist(inputField.text)) {
                Save(inputField.text);
            } else {
                NotificationManager.Send(new FileAlreadyExists());
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

        PlayerData playerData = new PlayerData(player);
        BallData ballData = ball.CacheData();

        LeverData[] leverDatas = CacheData(levers, levers.Length);
        StandButtonData[] standButtonDatas = CacheData(standButtons, standButtons.Length);
        LadderData[] ladderDatas = CacheData(ladders, ladders.Length);
        StoryLineData[] storyLineDatas = CacheData(storyLines, storyLines.Length);

        LevelData levelData = new LevelData(scene, playerData, ballData,
                                            leverDatas, standButtonDatas,
                                            ladderDatas, storyLineDatas);

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

    private T[] CacheData<T>(ICacheable<T>[] cacheable, int count) {
        T[] datas = new T[count];
        for (int i = 0; i < count; i++) {
            datas[i] = cacheable[i].CacheData();
        }
        return datas;
    }

    private void OnEnable() {
        inputField = GetComponent<InputField>();
        if (inputField) {
            inputField.ActivateInputField();
        }
    }
}