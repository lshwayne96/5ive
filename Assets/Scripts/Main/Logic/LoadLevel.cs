/*
 * This script is used to load a game.
 */

using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadLevel : MonoBehaviour {

	// Deserialise the game data and cache them in restoreGame
	public void Load(string fileName) {
		string saveFilePath = StorageUtil.FileNameToPath(fileName, true);
		LevelData levelData = StorageUtil.Deserialise<LevelData>(saveFilePath);

		// Load the scene of the saved game
		SceneManager.LoadScene(levelData.sceneBuildIndex);
		// Cache the levelData reference in restoreGame
		RestoreLevel.restoreLevel.Cache(levelData);
	}
}