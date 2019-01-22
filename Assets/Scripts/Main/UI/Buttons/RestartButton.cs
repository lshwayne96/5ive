using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour {

	public void RestartLevel() {
		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(scene.buildIndex);
	}

	public void RestartGame() {
		Game.instance.Reset();
	}
}
