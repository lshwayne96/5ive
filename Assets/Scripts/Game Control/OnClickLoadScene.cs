/*
 * This script looks a specified scene upon
 * interacting with the attached gameObject.
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClickLoadScene : MonoBehaviour {

    public void LoadByIndex(int sceneBuildIndex) {
        int currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        if (GameDataManager.HasSavedBefore() && currentSceneBuildIndex == 0) {
            string saveFilePath = LevelFile.ConvertToPath(GameDataManager.GetLastSavedFileName(), true);
            LevelData levelData = LevelFile.Deserialise<LevelData>(saveFilePath);

            // Load the scene of the saved game
            SceneManager.LoadScene(levelData.GetSceneBuildIndex());

            // Cache the levelData reference in restoreGame
            RestoreLevel.restoreLevel.Cache(levelData);
        } else {
            SceneManager.LoadScene(sceneBuildIndex);
        }
    }
}
