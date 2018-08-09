/*
 * This script is for a Singleton object which will not be destroyed
 * when a new scene is loaded.
 * It restores the scene data from a saved game file after
 * the scene of the game file is loaded.
 * 
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class RestoreLevel : MonoBehaviour {

    // The Singleton RestoreScene instance
    public static RestoreLevel restoreLevel;
    private LevelData levelData;
    private bool hasSavedScene;

    // Ensures that there is only one RestoreGame instance
    private void Awake() {
        if (restoreLevel == null) {
            DontDestroyOnLoad(gameObject);
            restoreLevel = this;

        } else if (restoreLevel != this) {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Caches data from the LoadGame script
    public void Cache(LevelData levelData) {
        this.levelData = levelData;
        hasSavedScene = true;
    }

    // Restores the previous game data
    public void Restore() {
        levelData.Restore();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
        if (hasSavedScene) {
            Restore();
            hasSavedScene = false;
        }
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


}
