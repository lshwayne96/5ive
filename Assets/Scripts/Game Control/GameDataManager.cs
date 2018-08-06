/*
 * This script is attached to an empty prefab called StartGame.
 * 
 * It can be said to encapsulates a GameData instance,
 * where some methods pertaining to the GameData class is
 * encapsulated into static methods in this script.
 * The aim is to reduce code complexity.
 *
 * It also handles the creation, deseralisation or destruction of
 * a GameData instance.
 */

using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameDataManager : MonoBehaviour {
    // The Singleton StartGame instance
    public static GameDataManager gameDataManager;
    private static GameData gameData;
    private static Collectible collectible;
    private static Text newButtonText;
    private static string saveFilePath;
    private bool hasInitialised;

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
        if (gameData.HasSavedBefore()) {
            newButtonText.text = "Resume";
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
        if (scene.buildIndex != 0 && !gameData.DoesSceneContainCollectible(scene.buildIndex)) {
            collectible = GameObject.FindGameObjectWithTag("ComponentManager")
                                    .GetComponent<ComponentManager>()
                                    .GetScript<Collectible>();
            Destroy(collectible.gameObject);

        } else {
            int currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
            if (gameData.HasSavedBefore() && currentSceneBuildIndex == (int)Level.MainMenu) {
                newButtonText = GameObject.FindGameObjectWithTag("NewButton")
                                          .GetComponentInChildren<Text>();
                newButtonText.text = "Resume";
            }
        }
    }

    /* Game Data is saved when the editor is closed
     * Does not concern end users
     */
    private void OnApplicationQuit() {
        GameFile.Serialise(saveFilePath, gameData);
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public static void ResetGameData() {
        File.Delete(saveFilePath);
        string[] levelFileNames = Directory.GetFiles(LevelFile.GetSaveDirectoryPath());
        foreach (string levelFileName in levelFileNames) {
            File.Delete(levelFileName);
        }
        gameData.SetHasSavedBefore(false);
        FirstGameSetUp();
    }

    public static void ResetNewButtonText() {
        newButtonText.text = "New";
    }

    public static GameData GetGameData() {
        return gameData;
    }

    public static int GetNumLevelsCompleted() {
        return gameData.GetNumLevelsCompleted();
    }

    public static void SetHasSavedBefore() {         gameData.SetHasSavedBefore(true);     }

    public static bool HasSavedBefore() {
        return gameData.HasSavedBefore();
    }      public static void SetLastSavedFileName(string fileName) {         gameData.SetLastSavedFileName(fileName);     }

    public static string GetLastSavedFileName() {
        return gameData.GetLastSavedFileName();
    }      public static void UpdateCollectibleLocations(int sceneBuildIndex) {         gameData.UpdateCollectibleLocations(sceneBuildIndex);     }      public static void UpdateNumLevelsCompleted() {         gameData.UpdateNumLevelsCompleted();     } 
}
