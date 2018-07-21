/*
 * This script is for a Singleton object which will not be destroyed
 * when a new scene is loaded.
 * It restores the game data from a saved game file after
 * the scene of the game file is loaded.
 */

using UnityEngine;

public class RestoreGame : MonoBehaviour {

    // The Singleton RestoreGame instance
    public static RestoreGame restoreGame;
    private LevelData levelData;
    private bool hasSavedGame;

    // Ensures that there is only one RestoreGame instance
    private void Awake() {
        if (restoreGame == null) {
            DontDestroyOnLoad(gameObject);
            restoreGame = this;
            //Debug.Log("Make this the restoreGame");

        } else if (restoreGame != this) {
            Destroy(gameObject);
            //Debug.Log("This is not the restoreGame");
        }
    }

    // Caches data from the LoadGame script
    public void Cache(LevelData levelData) {
        this.levelData = levelData;
        this.hasSavedGame = true;
    }

    // Restores the previous game data
    public void Restore() {
        levelData.Restore();
    }

    private void OnLevelWasLoaded(int level) {
        if (hasSavedGame) {
            Restore();
        }
    }
}

