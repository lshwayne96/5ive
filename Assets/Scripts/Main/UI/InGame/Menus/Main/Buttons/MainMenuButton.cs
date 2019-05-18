using Main.Commons;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main.UI.InGame.Buttons {

	public class MainMenuButton : MonoBehaviour {

		public void LoadMainMenu() {
			SceneManager.LoadScene((int) LevelName.MainMenu);
		}
	}

}