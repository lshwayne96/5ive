/*
 * This script is attached to an empty prefab called StartGame.
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
    private string saveFilePath;
    private bool hasInitialised;
    private bool hasSavedBefore;

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

    private void FirstGameSetUp() {
        Debug.Log("First");
        // Create the file, and then close the resulting file stream
        File.Create(saveFilePath).Close();
        gameData = new GameData(SceneManager.sceneCountInBuildSettings);
        newButtonText.text = "New";
    }

    private void SubsequentGamesSetUp() {
        Debug.Log("Subsequent");

        gameData = GameFile.Deserialise<GameData>(saveFilePath);
        if (gameData.HasSavedBefore()) {
            newButtonText.text = "Resume";
        }
    }

    public static GameData GetGameData() {
        return gameData;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
        if (scene.buildIndex != 0 && !gameData.DoesSceneContainCollectible(scene.buildIndex)) {
            collectible = GameObject.FindGameObjectWithTag("ComponentManager")
                                    .GetComponent<ComponentManager>()
                                    .GetScript<Collectible>();
            Destroy(collectible.gameObject);
        } else {
            int currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
            if (hasSavedBefore && currentSceneBuildIndex == 0) {
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

    public static void UpdateCollectibleLocations(int sceneBuildIndex) {
        gameData.UpdateCollectibleLocations(sceneBuildIndex);
    }

    public static void UpdateNumLevelsCompleted() {
        gameData.UpdateNumLevelsCompleted();
    }

    public void ResetGameData() {
        File.Delete(saveFilePath);
        string[] levelFileNames = Directory.GetFiles(LevelFile.GetSaveDirectoryPath());
        foreach (string levelFileName in levelFileNames) {
            File.Delete(levelFileName);
        }
        hasSavedBefore = false;
        gameData.SetHasSavedBefore(false);
        FirstGameSetUp();
    }

    public void SetHasSavedBefore() {
        hasSavedBefore = true;
        gameData.SetHasSavedBefore(true);
    }

    public void SetLastSavedFileName(string fileName) {
        gameData.SetLastSavedFileName(fileName);
    }
}
