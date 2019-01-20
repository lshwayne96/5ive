using UnityEngine; using UnityEngine.SceneManagement;

/// <summary> /// This scripts loads the specified scene. /// </summary> public class LoadLevelOnClick : MonoBehaviour {

	public void LoadByIndex(int sceneBuildIndex) {
		if (Game.HasAdvancedInGame && gameObject.CompareTag(Tags.NewButton)) {
			if (Game.HasUnlockedAndSavedLevel()) {
				GoToNewLevel();
			} else { 				GoToCurrentLevel();
			}
			return;
		} 		SceneManager.LoadScene(sceneBuildIndex);
	}  	private void GoToNewLevel() {
		SceneManager.LoadScene(Game.LastUnlockedLevel);
	}  	private void GoToCurrentLevel() { 		string path = StorageUtil.FileNameToPath(Game.LastSavedFileName, TagAddition.Enable);
		LevelData levelData = StorageUtil.Deserialise<LevelData>(path);
		SceneManager.LoadScene(levelData.SceneBuildIndex);
		RestoreLevel.instance.Take(levelData);
	} }  
