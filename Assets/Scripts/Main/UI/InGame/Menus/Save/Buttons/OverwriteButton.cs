using Main.UI.InGame.Menus.Save;
using UnityEngine;

namespace Main.UI.InGame.Menus.Save.Buttons {

	/// <summary>
	/// This script represents level file overwriting functionality via a button.
	/// </summary>
	/// <remarks>
	/// This script is meant to be attached to the overwrite button in the menu.
	/// </remarks>
	public class OverwriteButton : MonoBehaviour {

		private SaveInGameMenu manager;

		void Start() {
			manager = transform.parent.GetComponentInChildren<SaveInGameMenu>();
		}

		public void Overwrite() {
			manager.OverwriteTargetButton();
		}
	}

}