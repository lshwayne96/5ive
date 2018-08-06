/*
 * This script is used to load a game.
 */

using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadLevel : MonoBehaviour {

    // Deserialise the game data and cache them in restoreGame
    public void Load(string fileName) {
        try {
            string saveFilePath = LevelFile.ConvertToPath(LevelFile.AddTag(fileName));
            LevelData levelData = LevelFile.Deserialise<LevelData>(saveFilePath);

            // Load the scene of the saved game
            SceneManager.LoadScene(levelData.GetSceneBuildIndex());

            // Cache the levelData reference in restoreGame
            RestoreLevel.restoreLevel.Cache(levelData);

        } catch (FileNotFoundException) {
            Debug.Log("Level file has been deleted or moved.");
        }
    }
}