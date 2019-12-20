using Main._5ive.Commons;
using UnityEngine;
using UnityEngine.UI;

namespace Main._5ive.UI.Menus {

	/// <summary>
	/// This script extends the <code>Menu</code> script
	/// and represents a menu where the user can load previous game progress.
	/// </summary>
	/// <remarks>
	/// The functions of this script are to populate the menu with buttons
	/// and provide level file loading functionality.
	/// </remarks>
	public class LoadMenu : Menu {
		private Button loadButton;
		private Button deleteButton;
		private Button deleteAllButton;

		private void Start() {
			Init();
		}

		protected override void Init() {
			base.Init();

			loadButton = GameObject.FindWithTag(Tags.LoadButton).GetComponent<Button>();
			deleteButton = transform.Find(Tags.DeleteButton).GetComponent<Button>();
			deleteAllButton = transform.Find(Tags.DeleteAllButton).GetComponent<Button>();

			loadButton.interactable = false;
			deleteButton.interactable = false;
			deleteAllButton.interactable = !gameButtonCollection.IsEmpty();
		}

		public void LoadGameButton() {
			TargetButton.LoadLevel();
			loadButton.interactable = false;
		}

		public void RemoveGameButton() {
			TargetButton.transform.SetParent(null);
			gameButtonCollection.Remove(TargetButton);
			UpdateButtonInteractivity(false, loadButton, deleteButton);

			if (gameButtonCollection.IsEmpty()) {
				UpdateButtonInteractivity(false, deleteAllButton);
			}
		}


		public void RemoveAllGameButtons() {
			gameButtonCollection.RemoveAll();
			UpdateButtonInteractivity(false, loadButton, deleteButton, deleteAllButton);
		}

		private void UpdateButtonInteractivity(bool state, params Button[] buttons) {
			foreach (Button button in buttons) {
				button.interactable = state;
			}
		}
	}

}