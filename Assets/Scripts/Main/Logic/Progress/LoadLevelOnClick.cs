using UnityEngine; using UnityEngine.SceneManagement;

/// <summary> /// This scripts loads the specified scene. /// </summary> public class LoadLevelOnClick : MonoBehaviour {

	public void LoadByIndex(int sceneBuildIndex) {
		if (GameDataManager.hasAdvancedInGame && gameObject.CompareTag(Tags.NewGameButton)) {
			if (GameDataManager.HasUnlockedNewLevelWithoutSaving()) {
				GoToNewLevel();
			} else { 				GoToCurrentLevel();
			}
			return;
		} 		SceneManager.LoadScene(sceneBuildIndex);
	}  	private void GoToNewLevel() {
		SceneManager.LoadScene(GameDataManager.lastUnlockedLevel);
	}  	private void GoToCurrentLevel() { 		string path = StorageUtil.FileNameToPath(GameDataManager.lastSavedFileName, TagAddition.Enable);
		LevelData levelData = StorageUtil.Deserialise<LevelData>(path);
		SceneManager.LoadScene(levelData.SceneBuildIndex);
		RestoreLevel.instance.Take(levelData);
	} }  
