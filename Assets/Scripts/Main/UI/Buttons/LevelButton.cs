using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour {

	public void LoadLevel(int sceneBuildIndex) {
		SceneManager.LoadScene(sceneBuildIndex);
	}
}
