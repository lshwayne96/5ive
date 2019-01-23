using System;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Diagnostics;

/// <summary>
/// This script represents the skeleton of the game.
/// </summary>
public class Game : RestorableMonoBehaviour {

	/// <summary>
	/// The instance of <see cref="this"/>.
	/// </summary>
	public static Game instance;

	/// <summary>
	/// Returns the number of levels completed.
	/// </summary>
	/// <value>The number of levels completed.</value>
	public int NumLevelsCompleted { get; private set; }

	/// <summary>
	/// Returns whether the game been saved before.
	/// </summary>
	/// <remarks>
	/// Used to know whether the button that starts the game
	/// should have the text <see cref="MainMenu.NewButtonText"/>
	/// or <see cref="MainMenu.NewButtonResumeText"/>.
	/// </remarks>
	/// <value><c>true</c> if the game has been saved before; otherwise, <c>false</c>.</value>
	public bool HasBeenSavedBefore { get; private set; }

	/// <summary>
	/// Returns the name of the last file saved.
	/// </summary>
	/// <remarks>
	/// Used to bring the user to the latest saved level when
	/// the user clicks on the button that starts the game.
	/// </remarks>
	/// <value>The name of the last file saved.</value>
	public string NameOfLastFileSaved { get; private set; }

	/// <summary>
	/// Returns the scene build index of the scene that corresponds
	/// to the last level unlocked.
	/// </summary>
	/// <value>The scene build index of the last level unlocked.</value>
	public int SceneBuildIndexOfLastLevelUnlocked { get; private set; }

	/// <summary>
	/// Returns the scene build index of the scene that corresponds
	/// to the last level saved.
	/// </summary>
	/// <remarks>
	/// Used to decide between loading the last saved level or the newest unlocked level
	/// when the user clicks on the button that starts the game.
	/// </remarks>
	/// <value>The scene build index of last level saved.</value>
	public int SceneBuildIndexOfLastLevelSaved { get; private set; }

	/// <summary>
	/// By default, there are no levels completed.
	/// </summary>
	private const int DefaultNumLevelCompleted = 0;

	/// <summary>
	/// By default, the game has not been advanced.
	/// </summary>
	private const bool DefaultAdvancement = false;

	/// <summary>
	/// By default, the last unlocked level is the main menu.
	/// </summary>
	private const int DefaultSceneBuildIndexOfLastLevelUnlocked = (int) LevelNames.Denial;

	private const int DefaultSceneBuildIndexOfLastLevelSaved = DefaultSceneBuildIndexOfLastLevelUnlocked;

	private Dictionary<int, string> levelToPathMapping;

	private Level level;

	private string gamePath;

	private bool hasInitGame;

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

	private void Start() {
		level = GetComponentInChildren<Level>();
	}

	/// <summary>
	/// Initialises the game.
	/// </summary>
	/// <remarks>
	/// This method is called in the main menu when the game
	/// first starts up.
	/// </remarks>
	private void InitGame() {
		NumLevelsCompleted = DefaultNumLevelCompleted;
		HasBeenSavedBefore = false;
		NameOfLastFileSaved = string.Empty;
		SceneBuildIndexOfLastLevelUnlocked = DefaultSceneBuildIndexOfLastLevelUnlocked;
		SceneBuildIndexOfLastLevelSaved = DefaultSceneBuildIndexOfLastLevelSaved;
		levelToPathMapping = new Dictionary<int, string>();
		gamePath = StorageUtil.GetDirectoryPath(FileType.Game) + StorageUtil.GameFilePath;
		print(gamePath);

		if (FileUtil.DoesFileExist(gamePath)) {
			InitSubsequentGame();
		} else {
			InitFirstGame();
		}
	}

	/// <summary>
	/// Initialise for the first game.
	/// </summary>
	private void InitFirstGame() {
		FileUtil.CreateFile(gamePath);
		HasBeenSavedBefore = false;
	}

	/// <summary>
	/// Initialise for the subsequent game.
	/// </summary>
	private void InitSubsequentGame() {
		GameData data = StorageUtil.Deserialise<GameData>(gamePath);
		data.Restore(this);
	}

	public void StartGame() {
		if (HasUnlockedAndSavedLevel()) {
			SceneManager.LoadScene(SceneBuildIndexOfLastLevelUnlocked);
		} else {
			SceneManager.LoadScene(SceneBuildIndexOfLastLevelSaved);
		}
	}

	public void Reset() {
		DeleteGameFile();
		DeleteLevelFiles();
		InitFirstGame();
	}

	private void DeleteGameFile() {
		FileUtil.DeleteFile(gamePath);
	}

	private void DeleteLevelFiles() {
		string levelPaths = StorageUtil.GetDirectoryPath(FileType.Level);
		string[] paths = Directory.GetFiles(levelPaths);
		foreach (string path in paths) {
			File.Delete(path);
		}
	}

	public void Save(string fileName) {
		Scene scene = SceneManager.GetActiveScene();
		Level.LevelData levelData = (Level.LevelData) level.Save();

		string path = StorageUtil.FileNameToPath(fileName, TagAddition.Enable);
		StorageUtil.Serialise(FileType.Level, path, levelData);

		HasBeenSavedBefore = true;
		NameOfLastFileSaved = fileName;
		SceneBuildIndexOfLastLevelSaved = levelData.SceneBuildIndex;
	}

	public void LoadLevel(string fileName) {
		string path = StorageUtil.FileNameToPath(fileName, TagAddition.Enable);
		Level.LevelData data = StorageUtil.Deserialise<Level.LevelData>(path);
		SceneManager.LoadScene(data.SceneBuildIndex);
	}

	//TODO
	public void Override(string fileName) {

	}

	public void EndLevel(int sceneBuildIndex) {
		NumLevelsCompleted += 1;
		SceneBuildIndexOfLastLevelUnlocked = sceneBuildIndex;
		SceneManager.LoadScene(sceneBuildIndex);
	}

	public void SaveBeforeExit() {
		StorageUtil.Serialise(FileType.Game, gamePath, Save());
	}

	/// <summary>
	/// Checks if the last unlocked level has been saved before.
	/// </summary>
	/// <returns><c>true</c>, if the last unlocked level is equal to the
	/// last saved level, <c>false</c> otherwise.</returns>
	public bool HasUnlockedAndSavedLevel() {
		return SceneBuildIndexOfLastLevelUnlocked == SceneBuildIndexOfLastLevelSaved;
	}

	public override Data Save() {
		return new GameData(this);
	}

	public override void RestoreWith(Data data) {
		GameData gameData = (GameData) data;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
		if (!LevelUtil.IsMainMenu(scene.buildIndex) && level.HasBeenSaved) {
			string levelPath = levelToPathMapping[level.SceneBuildIndex];
			Data data = StorageUtil.Deserialise<Level.LevelData>(levelPath);
			level.RestoreWith(data);
		}
	}

	private void OnApplicationQuit() {
		StorageUtil.Serialise(FileType.Game, gamePath, Save());
	}

	private void OnDisable() {
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	/// <summary>
	/// This class represents the data of the game.
	/// </summary>
	[Serializable]
	class GameData : Data {

		public int numLevelsCompleted;

		public bool hasBeenSavedBefore;

		public string nameOfLastFileSaved;

		public int sceneBuildIndexOfLastLevelUnlocked;

		public int sceneBuildIndexOfLastLevelSaved;

		public Dictionary<int, string> levelPathMapping;

		public GameData(Game game) {
			numLevelsCompleted = game.NumLevelsCompleted;
			hasBeenSavedBefore = game.HasBeenSavedBefore;
			nameOfLastFileSaved = game.NameOfLastFileSaved;
			sceneBuildIndexOfLastLevelUnlocked = game.SceneBuildIndexOfLastLevelSaved;
			sceneBuildIndexOfLastLevelSaved = game.SceneBuildIndexOfLastLevelSaved;
			levelPathMapping = game.levelToPathMapping;
		}

		public override void Restore(RestorableMonoBehaviour restorable) {
			restorable.RestoreWith(this);
		}
	}
}



