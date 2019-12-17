using Main._5ive.UI.Menus;
using UnityEngine;

namespace Main._5ive.UI.Buttons {

	/// <summary>
	/// This script represents level file overwriting functionality via a button.
	/// </summary>
	/// <remarks>
	/// This script is meant to be attached to the overwrite button in the menu.
	/// </remarks>
	public class OverwriteButton : MonoBehaviour {

		private SaveMenu manager;

		private void Start() {
			manager = transform.parent.GetComponentInChildren<SaveMenu>();
		}

		public void OverwriteLevel() {
			manager.OverwriteTargetButton();
		}
	}

}