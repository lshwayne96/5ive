/*
 * This script is used to load a game.
 */

using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadLevel : MonoBehaviour {

    // Deserialise the game data and cache them in restoreGame
    public void Load(string fileName) {
        string saveFilePath = LevelFile.ConvertToPath(fileName, true);
        LevelData levelData = LevelFile.Deserialise<LevelData>(saveFilePath);

        // Load the scene of the saved game
        SceneManager.LoadScene(levelData.GetSceneBuildIndex());
        // Cache the levelData reference in restoreGame
        RestoreLevel.restoreLevel.Cache(levelData);
    }
}