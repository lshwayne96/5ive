using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

/// <summary>
/// This script represents the skeleton of the game.
/// </summary>
public class Game : MonoBehaviour {

	public static Game instance;

	private static GameData gameData;

	private static LevelData levelData;

	private static Text newButtonText;

	private static string gamePath;

	private bool hasInitGame;

	private bool hasSavedLevel;

	public static int NumLevelsCompleted {
		get { return gameData.numLevelsCompleted; }
		set { gameData.numLevelsCompleted = value; }
	}

	public static bool HasAdvancedInGame {
		get { return gameData.hasBeenSavedBefore; }
		set { gameData.hasBeenSavedBefore = value; }
	}

	public static string LastSavedFileName {
		get { return gameData.lastSavedFileName; }
		set { gameData.lastSavedFileName = value; }
	}

	public static int LastUnlockedLevel {
		get { return gameData.lastUnlockedLevel; }
		set { gameData.lastUnlockedLevel = value; }
	}

	public static int LastSavedLevel {
		set { gameData.lastSavedLevel = value; }
	}

	private void Awake() {
		if (instance == null) {
			DontDestroyOnLoad(gameObject);
			instance = this;

		} else if (instance != this) {
			Destroy(gameObject);
		}

		if (instance == this) {
			if (!hasInitGame) {
				InitGame();
				hasInitGame = true;
			}
			SceneManager.sceneLoaded += OnSceneLoaded;
		}
	}

	/// <summary>
	/// Initialises the game.
	/// </summary>
	/// <remarks>
	/// This method is called in the main menu when the game
	/// first starts up.
	/// </remarks>
	private void InitGame() {
		gamePath = StorageUtil.GetDirectoryPath(FileType.Game);
		newButtonText = GameObject.FindGameObjectWithTag(Tags.NewButton)
						  .GetComponentInChildren<Text>();

		if (File.Exists(gamePath)) {
			InitSubsequentGame();
		} else {
			InitFirstGame();
		}
	}

	/// <summary>
	/// Initialise for the first game.
	/// </summary>
	private static void InitFirstGame() {
		FileUtil.CreateFile(gamePath);
		gameData = new GameData();
		newButtonText.text = "New";
	}

	/// <summary>
	/// Initialise for the subsequent game.
	/// </summary>
	private static void InitSubsequentGame() {
		gameData = StorageUtil.Deserialise<GameData>(gamePath);
		if (gameData.hasBeenSavedBefore) {
			newButtonText.text = "Resume";
		}
	}

	/// <summary>
	/// When a level loads, check if the level is the main menu.
	/// If not, check if the collectible in that level has previously been collected.
	/// If it has, destroy it.
	/// </summary>
	/// <param name="scene">Scene.</param>
	/// <param name="loadSceneMode">Load scene mode.</param>
	private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
		bool isMainMenu = scene.buildIndex != (int) LevelNames.MainMenu;
		bool isCollectiblePresent = gameData.sceneCollectibleMapping[scene.buildIndex];
		bool hasCollectibleBeenCollected = !isMainMenu && isCollectiblePresent;

		if (hasCollectibleBeenCollected) {
			FindAndDestroyCollectible();
		}

		if (hasSavedLevel) {
			levelData.Restore();
			hasSavedLevel = false;
		}
	}

	/// <summary>
	/// Finds and destroys the <code>Collectible</code> game object
	/// in the current level.
	/// </summary>
	private static void FindAndDestroyCollectible() {
		Collectible c = GameObject.FindGameObjectWithTag(Tags.ComponentManager)
			.GetComponent<ComponentManager>()
			.GetScript<Collectible>();
		Destroy(c.gameObject);
	}

	/* Game Data is saved when the editor is closed
     * Does not concern end users
     * Only for our own convenience
     */
	private void OnApplicationQuit() {
		StorageUtil.Serialise(FileType.Game, gamePath, gameData);
	}

	private void OnDisable() {
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	public static void ResetProgress() {
		ResetGame();
		newButtonText.text = "New";
	}

	public static void ResetGame() {
		DeleteGameFile();
		DeleteLevelFiles();
		InitFirstGame();
	}

	private static void DeleteGameFile() {
		File.Delete(gamePath);
	}

	private static void DeleteLevelFiles() {
		string levelPaths = StorageUtil.GetDirectoryPath(FileType.Level);
		string[] paths = Directory.GetFiles(levelPaths);
		foreach (string path in paths) {
			File.Delete(path);
		}
		gameData.hasBeenSavedBefore = false;
	}

	public static void EndLevel(int sceneBuildIndex) {
		NumLevelsCompleted += 1;
		LastUnlockedLevel = sceneBuildIndex;
		SceneManager.LoadScene(sceneBuildIndex);
	}

	public static void Save(string fileName, Data[] datas) {
		Scene scene = SceneManager.GetActiveScene();
		LevelData levelData = new LevelData(scene, datas);

		string path = StorageUtil.FileNameToPath(fileName, TagAddition.Enable);
		StorageUtil.Serialise(FileType.Level, path, levelData);

		// Used to know when to change the "New" text in the New Button to "Resume"
		gameData.hasBeenSavedBefore = true;

		// Used to allow clicking on the Resume button to load the latest saved level
		LastSavedFileName = fileName;

		// Used to decide between loading the last saved level or the newest unlocked level
		LastSavedLevel = levelData.SceneBuildIndex;
	}

	public static void SaveBeforeExit() {
		string path = StorageUtil.GetDirectoryPath(FileType.Game);
		StorageUtil.Serialise(FileType.Game, path, gameData);
	}

	public static void Load(string fileName) {
		string path = StorageUtil.FileNameToPath(fileName, TagAddition.Enable);
		LevelData data = StorageUtil.Deserialise<LevelData>(path);
		SceneManager.LoadScene(data.SceneBuildIndex);
		levelData = data;
	}


	public static void MarkSceneCollectibleAsCollected(int sceneBuildIndex) {
		gameData.sceneCollectibleMapping[sceneBuildIndex] = false;
	}

	/// <summary>
	/// Checks if the last unlocked level has been saved before.
	/// </summary>
	/// <returns><c>true</c>, if the last unlocked level is equal to the
	/// last saved level, <c>false</c> otherwise.</returns>
	public static bool HasUnlockedAndSavedLevel() {
		return gameData.lastUnlockedLevel == gameData.lastSavedLevel;
	}

	/// <summary>
	/// This class represents the data of the game.
	/// </summary>
	[Serializable]
	class GameData {
		public int numLevelsCompleted;
		public Dictionary<int, bool> sceneCollectibleMapping;

		public bool hasBeenSavedBefore;
		public string lastSavedFileName;
		public int lastSavedLevel;
		public int lastUnlockedLevel;

		/// <summary>
		/// By default, there is only a single level completed.
		/// The single level completed refers to the main menu level.
		/// </summary>
		private readonly int DefaultNumLevelCompleted = 1;

		/// <summary>
		/// By default, the game has not been advanced.
		/// </summary>
		private readonly bool DefaultAdvancementLevel = false;

		/// <summary>
		/// By default, the last unlocked level is the main menu.
		/// </summary>
		private readonly int DefaultLastUnlockedLevel = (int) LevelNames.MainMenu;

		/// <summary>
		/// Initialises a new instance of the <see cref="T:GameDataManager.GameData"/> class.
		/// </summary>
		public GameData() {
			numLevelsCompleted = DefaultNumLevelCompleted;
			hasBeenSavedBefore = DefaultAdvancementLevel;
			lastUnlockedLevel = DefaultLastUnlockedLevel;

			int numLevels = SceneManager.sceneCountInBuildSettings;
			sceneCollectibleMapping = new Dictionary<int, bool>(numLevels);
			for (int i = DefaultNumLevelCompleted; i < numLevels; i++) {
				sceneCollectibleMapping.Add(i, true);
			}
		}
	}
}



