using System;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script represents the skeleton of the game.
/// </summary>
public class Game : MonoBehaviour {

	/// <summary>
	/// The instance of <see cref="this"/>.
	/// </summary>
	public static Game instance;

	public Level level;

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
	private const int DefaultSceneBuildIndexOfLastLevelUnlocked = (int) LevelName.Denial;

	private const int DefaultSceneBuildIndexOfLastLevelSaved = DefaultSceneBuildIndexOfLastLevelUnlocked;

	private Dictionary<int, string> levelToNameDictionary;

	private PauseBehaviour pauseBehaviour;

	public IStorage storage;

	private bool hasInitGame;

	private Logger logger = Logger.GetLogger();

	private void Awake() {
		if (instance == null) {
			DontDestroyOnLoad(gameObject);
			instance = this;
		}

		if (instance != this) {
			Destroy(gameObject);
			return;
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
		NumLevelsCompleted = DefaultNumLevelCompleted;
		HasBeenSavedBefore = false;
		NameOfLastFileSaved = string.Empty;
		SceneBuildIndexOfLastLevelUnlocked = DefaultSceneBuildIndexOfLastLevelUnlocked;
		SceneBuildIndexOfLastLevelSaved = DefaultSceneBuildIndexOfLastLevelSaved;
		levelToNameDictionary = new Dictionary<int, string>();

		storage = new StorageManager();

		if (storage.HasGame()) {
			InitSubsequentGame();
			logger.LogLow("Subsequent game initialised");
		} else {
			InitFirstGame();
			logger.LogLow("First game initialised");
		}
	}

	/// <summary>
	/// Initialise for the first game.
	/// </summary>
	private void InitFirstGame() {
		HasBeenSavedBefore = false;
	}

	/// <summary>
	/// Initialise for the subsequent game.
	/// </summary>
	private void InitSubsequentGame() {
		Data data = storage.FetchGame();
		RestoreWith(data);
	}

	private void Update() {
		if (SceneManager.GetActiveScene().buildIndex != 0) {
			if (Input.GetKeyDown(KeyCode.Escape)) {
				pauseBehaviour = new PauseBehaviour();
				if (pauseBehaviour != null) {
					if (pauseBehaviour.IsActive) {
						pauseBehaviour.Disable();
					} else {
						pauseBehaviour.Enable();
					}
				}
			}
		}
	}

	public void StartGame() {
		logger.LogLow("Starting new game");
		if (HasUnlockedAndSavedLevel()) {
			SceneManager.LoadScene(SceneBuildIndexOfLastLevelUnlocked);
		} else {
			SceneManager.LoadScene(SceneBuildIndexOfLastLevelSaved);
		}
	}

	public void ResetGame() {
		storage.DeleteAllFiles();
		InitFirstGame();
		logger.LogLow("Game reset done");
	}

	public void SaveLevel(string name) {
		Data data = level.Save();
		storage.StoreLevel(data, name);

		HasBeenSavedBefore = true;
		NameOfLastFileSaved = name;
		SceneBuildIndexOfLastLevelSaved = SceneManager.GetActiveScene().buildIndex;
	}

	public void LoadLevel(string name) {
		Data data = storage.FetchLevel(name);
		Level.LevelData levelData = (Level.LevelData) data;
		SceneManager.LoadScene(levelData.sceneBuildIndex);
	}

	public GameButtonInfo OverrideLevel(string name) {
		SaveLevel(name);
		return storage.FetchGameButton(name);
	}

	public void EndLevel(int sceneBuildIndex) {
		NumLevelsCompleted += 1;
		SceneBuildIndexOfLastLevelUnlocked = sceneBuildIndex;

		logger.LogLow("Ending level");
		SceneManager.LoadScene(sceneBuildIndex);
	}

	public void DeleteLevel(string name) {
		storage.DeleteLevel(name);
	}

	/// <summary>
	/// Checks if the last unlocked level has been saved before.
	/// </summary>
	/// <returns><c>true</c>, if the last unlocked level is equal to the
	/// last saved level, <c>false</c> otherwise.</returns>
	public bool HasUnlockedAndSavedLevel() {
		return SceneBuildIndexOfLastLevelUnlocked == SceneBuildIndexOfLastLevelSaved;
	}

	private Data Save() {
		return new GameData(this);
	}

	private void RestoreWith(Data data) {
		GameData gameData = (GameData) data;
		NumLevelsCompleted = gameData.numLevelsCompleted;
		HasBeenSavedBefore = gameData.hasBeenSavedBefore;
		NameOfLastFileSaved = gameData.nameOfLastFileSaved;
		SceneBuildIndexOfLastLevelUnlocked = gameData.sceneBuildIndexOfLastLevelUnlocked;
		SceneBuildIndexOfLastLevelSaved = gameData.sceneBuildIndexOfLastLevelSaved;
	}

	public void SaveBeforeExit() {
		Data data = Save();
		storage.StoreGame(data);
		logger.LogLow("Game data saved");
		logger.LogLow("Exiting game");
	}

	private void OnApplicationQuit() {
		SaveBeforeExit();
		Logger.GetLogger().Close();
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
		level = GetComponentInChildren<Level>();

		if (!LevelUtil.IsMainMenu(scene.buildIndex) && level.HasBeenSaved) {
			string levelName = levelToNameDictionary[level.SceneBuildIndex];
			Data data = storage.FetchLevel(levelName);
			level.RestoreWith(data);
		}
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
			levelPathMapping = game.levelToNameDictionary;
		}
	}
}



