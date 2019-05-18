using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main.UI.StartGame.Menus.Overview.Buttons {

	public class LevelButton : MonoBehaviour {

		public void LoadLevel(int sceneBuildIndex) {
			SceneManager.LoadScene(sceneBuildIndex);
		}
	}

}