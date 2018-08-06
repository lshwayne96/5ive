/*
 * This script is attached to an empty prefab called StartGame.
 */

using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDataManager : MonoBehaviour {
    // The Singleton StartGame instance
    public static GameDataManager gameDataManager;
    private string saveFilePath;
    private GameData gameData;

    // Ensures that there is only one StartGame instance
    private void Awake() {
        if (gameDataManager == null) {
            DontDestroyOnLoad(gameObject);
            gameDataManager = this;

        } else if (gameDataManager != this) {
            Destroy(gameObject);
        }
    }

    void Start() {
        saveFilePath = GameFile.GetSaveFilePath();
        if (!File.Exists(saveFilePath)) { // First time starting the game
            File.Create(saveFilePath);
            gameData = new GameData(SceneManager.sceneCountInBuildSettings);

        } else { // Subsequent times starting the game
            gameData = GameFile.Deserialise<GameData>(saveFilePath);
        }
    }

    public GameData GetGameData() {
        return gameData;
    }
}
