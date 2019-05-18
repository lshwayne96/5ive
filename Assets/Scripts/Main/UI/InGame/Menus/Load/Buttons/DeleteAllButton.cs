using Main.UI.InGame.Menus.Load;
using UnityEngine;

namespace Main.UI.InGame.Menus.Load.Buttons {

	/// <summary>
	/// This script represents level file deletion functionality via a button.
	/// </summary>
	/// <remarks>
	/// This script is meant to be attached to the delete button in the load menu.
	/// </remarks>
	public class DeleteAllButton : MonoBehaviour {

		private LoadInGameMenu manager;

		private void Start() {
			manager = transform.parent.GetComponentInChildren<LoadInGameMenu>();
		}

		public void DeleteAll() {
			manager.DeleteAllButtons();
		}
	}

}