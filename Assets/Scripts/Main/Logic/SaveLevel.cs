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
	private FloorButton[] floorButtons;
	private Ladder[] ladders;
	private StoryLine[] storyLines;

	private void Start() {
		int currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
		if (currentSceneBuildIndex == (int) LevelNames.MainMenu) {
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
			floorButtons = componentManager.GetScripts<FloorButton>();
			ladders = componentManager.GetScripts<Ladder>();
			storyLines = componentManager.GetScripts<StoryLine>();
		}
	}

	public void Save() {
		if (!inputField.text.Equals(string.Empty)) {
			if (!fileButtonManager.DoesFileExist(inputField.text)) {
				Save(inputField.text);
				fileButtonManager.UpdateButtons();
				inputField.DeactivateInputField();

			} else {
				NotificationManager.Send(new FileAlreadyExists());
			}

			// Clear the input field
			inputField.text = string.Empty;
		}
	}

	public void Overwrite(string fileName) {
		Save(fileName);
		fileButtonManager.UpdateButtons();
	}

	/*
     * Caches all scene data into a SceneData instance,
     * creates a new or overwrite an old game file,
     * and serialise the scene data
     */
	private void Save(string fileName) {
		Scene scene = SceneManager.GetActiveScene();

		PlayerData playerData = new PlayerData(player);
		BallData ballData = ball.CacheData();

		LeverData[] leverDatas = Save(levers, levers.Length);
		Data[] floorButtonDatas = Save(floorButtons, floorButtons.Length);
		LadderData[] ladderDatas = Save(ladders, ladders.Length);
		StoryLineData[] storyLineDatas = Save(storyLines, storyLines.Length);



		LevelData levelData = new LevelData(scene, playerData, ballData,
											leverDatas, floorButtonDatas,
											ladderDatas, storyLineDatas);

		string saveFilePath = StorageUtil.FileNameToPath(fileName, true);
		StorageUtil.Serialise(FileType.Level, saveFilePath, levelData);

		GameDataManager.Save(fileName, scene.buildIndex);
	}

	private Data[] Save(IPersistent[] items, int count) {
		Data[] datas = new Data[count];
		for (int i = 0; i < count; i++) {
			datas[i] = items[i].Save();
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