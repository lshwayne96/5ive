/*
 * This script is attached to an empty prefab called StartGame
 * and will not destroyed when a new scene is loaded.
 * 
 * It can be said to encapsulates a GameData instance,
 * where some methods pertaining to the GameData class is
 * encapsulated into static methods in this script.
 * The aim is to reduce code complexity.
 *
 * It also handles the creation, deseralisation or destruction of
 * a GameData instance.
 */

using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameDataManager : MonoBehaviour {
    // The Singleton StartGame instance
    public static GameDataManager gameDataManager;

    private static GameData gameData;
    private static Text newButtonText;
    private static string saveFilePath;
    private bool hasInitialised;

    public static int numLevelsCompleted {
        get { return gameData.numLevelsCompleted; }
        set { gameData.numLevelsCompleted = value; }
    }

    public static bool hasAdvancedInGame {
        get { return gameData.hasAdvancedInGame; }
        set { gameData.hasAdvancedInGame = value; }
    }

    public static string lastSavedFileName {
        get { return gameData.lastSavedFileName; }
        set { gameData.lastSavedFileName = value; }
    }

    public static int lastUnlockedLevel {
        get { return gameData.lastUnlockedLevel; }
        set { gameData.lastUnlockedLevel = value; }
    }

    public static int lastSavedLevel {
        set { gameData.lastSavedLevel = value; }
    }

    // Ensures that there is only one StartGame instance
    private void Awake() {
        if (gameDataManager == null) {
            DontDestroyOnLoad(gameObject);
            gameDataManager = this;

        } else if (gameDataManager != this) {
            Destroy(gameObject);
        }

        if (gameDataManager == this) {
            if (!hasInitialised) {
                InitialiseGame();
                hasInitialised = true;
            }

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    private void InitialiseGame() {
        saveFilePath = GameFile.GetSaveFilePath();
        newButtonText = GameObject.FindGameObjectWithTag("NewButton")
                          .GetComponentInChildren<Text>();
        if (!File.Exists(saveFilePath)) { // First time setting up
            FirstGameSetUp();
        } else { // Subsequent times setting up
            SubsequentGamesSetUp();
        }
    }

    private static void FirstGameSetUp() {
        // Create the file, and then close the resulting file stream
        File.Create(saveFilePath).Close();
        gameData = new GameData(SceneManager.sceneCountInBuildSettings);
        newButtonText.text = "New";
    }

    private static void SubsequentGamesSetUp() {
        gameData = GameFile.Deserialise<GameData>(saveFilePath);
        if (gameData.hasAdvancedInGame) {
            newButtonText.text = "Resume";
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
        if (scene.buildIndex != 0 && !gameData.collectibleLocations[scene.buildIndex]) {
            Collectible collectible = GameObject.FindGameObjectWithTag("ComponentManager")
                                    .GetComponent<ComponentManager>()
                                    .GetScript<Collectible>();
            Destroy(collectible.gameObject);

        } else {
            int currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
            if (gameData.hasAdvancedInGame && currentSceneBuildIndex == (int)Level.MainMenu) {
                newButtonText = GameObject.FindGameObjectWithTag("NewButton")
                                          .GetComponentInChildren<Text>();
                newButtonText.text = "Resume";
            }
        }
    }

    /* Game Data is saved when the editor is closed
     * Does not concern end users
     * Only for our own convenience
     */
    private void OnApplicationQuit() {
        GameFile.Serialise(saveFilePath, gameData);
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public static void ResetProgress() {
        ResetGameData();
        newButtonText.text = "New";
    }

    public static void ResetGameData() {
        File.Delete(saveFilePath);
        string[] levelFileNames = Directory.GetFiles(LevelFile.GetSaveDirectoryPath());
        foreach (string levelFileName in levelFileNames) {
            File.Delete(levelFileName);
        }
        gameData.hasAdvancedInGame = false;
        FirstGameSetUp();
    }

    public static void EndLevel(int sceneBuildIndex) {
        numLevelsCompleted += 1;
        lastUnlockedLevel = sceneBuildIndex;
    }

    public static void Save(string fileName, int sceneBuildIndex) {
        // Used to know when to change the "New" text in the New Button to "Resume"
        gameData.hasAdvancedInGame = true;
        // Used to allow clicking on the Resume button to load the latest saved level
        lastSavedFileName = fileName;
        // Used to decide between loading the last saved level or the newest unlocked level
        lastSavedLevel = sceneBuildIndex;
    }

    public static void SaveGame(string saveFilePath) {
        GameFile.Serialise(saveFilePath, gameData);
    }

    public bool DoesSceneContainCollectible(int sceneBuildIndex) {
        return gameData.collectibleLocations[sceneBuildIndex];
    }

    public static void UpdateCollectibleLocations(int sceneBuildIndex) {
        gameData.collectibleLocations[sceneBuildIndex] = false;
    }

    public static bool HasUnlockedNewLevelWithoutSaving() {
        return gameData.lastUnlockedLevel > gameData.lastSavedLevel;
    }

    /*
     * This class represents the data of the game and is used to restore
     * the game to its saved state.
     */
    [Serializable]
    class GameData {
        public int numLevelsCompleted;
        public bool[] collectibleLocations;

        public bool hasAdvancedInGame;
        public string lastSavedFileName;
        public int lastSavedLevel;
        public int lastUnlockedLevel;

        public GameData(int numLevels) {
            numLevelsCompleted = 1;
            hasAdvancedInGame = false;
            collectibleLocations = new bool[numLevels + 1];
            for (int i = 1; i < collectibleLocations.Length; i++) {
                collectibleLocations[i] = true;
            }
            lastUnlockedLevel = (int)Level.MainMenu;
        }
    }
}



