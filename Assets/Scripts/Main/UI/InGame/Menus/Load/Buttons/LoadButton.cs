using UnityEngine;

namespace Main.UI.InGame.Menus.Load.Buttons {

	/// <summary>
	/// This script represents level loading functionality via a button.
	/// </summary>
	/// <remarks>
	/// This script is meant to be attached to the load button in the menu.
	/// </remarks>
	public class LoadButton : MonoBehaviour {

		private LoadInGameMenu manager;

		void Start() {
			manager = transform.parent.GetComponentInChildren<LoadInGameMenu>();
		}

		public void Load() {
			manager.LoadTargetButton();
		}
	}

}