using Main._5ive.UI.Menus;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main._5ive.UI.Buttons {

	/// <summary>
	/// This script represents level loading functionality via a button.
	/// </summary>
	/// <remarks>
	/// This script is meant to be attached to the load button in the menu.
	/// </remarks>
	public class LevelLoadButton : MonoBehaviour {

		private LoadMenu manager;

		private void Start() {
			manager = transform.parent.GetComponentInChildren<LoadMenu>();
		}

		public void LoadLevel() {
			manager.LoadGameButton();
		}

		public void LoadLevel(int sceneBuildIndex) {
			SceneManager.LoadScene(sceneBuildIndex);
		}
	}

}