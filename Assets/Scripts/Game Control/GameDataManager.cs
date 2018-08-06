/*
 * This script is attached to an empty prefab called StartGame.
 */

using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDataManager : MonoBehaviour {
    // The Singleton StartGame instance
    public static GameDataManager gameDataManager;
    private static GameData gameData;
    private static Collectible collectible;

    // Ensures that there is only one StartGame instance
    private void Awake() {
        if (gameDataManager == null) {
            DontDestroyOnLoad(gameObject);
            gameDataManager = this;

        } else if (gameDataManager != this) {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start() {
        string saveFilePath = GameFile.GetSaveFilePath();
        if (!File.Exists(saveFilePath)) { // First time starting the game
            File.Create(saveFilePath);
            gameData = new GameData(SceneManager.sceneCountInBuildSettings);

        } else { // Subsequent times starting the game
            gameData = GameFile.Deserialise<GameData>(saveFilePath);
        }
    }

    public static GameData GetGameData() {
        return gameData;
    }

    public static void UpdateCollectibleLocations(int sceneBuildIndex) {
        gameData.UpdateCollectibleLocations(sceneBuildIndex);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
        if (scene.buildIndex != 0 && !gameData.DoesSceneContainCollectible(scene.buildIndex)) {
            collectible = GameObject.FindGameObjectWithTag("ComponentManager")
                        .GetComponent<ComponentManager>()
                        .GetScript<Collectible>();
            Destroy(collectible.gameObject);
        }
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
