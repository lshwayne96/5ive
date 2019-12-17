using Main._5ive.Commons;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main._5ive.UI.Buttons {

	public class MainMenuButton : MonoBehaviour {

		public void LoadMainMenu() {
			SceneManager.LoadScene((int) LevelName.MainMenu);
		}
	}

}