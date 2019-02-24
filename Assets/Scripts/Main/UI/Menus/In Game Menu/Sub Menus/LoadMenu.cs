using UnityEngine;
using UnityEngine.UI;

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

	public override void Init() {
		base.Init();

		loadButton = GameObject.FindWithTag(Tags.LoadButton).GetComponent<Button>();
		deleteButton = GameObject.FindWithTag(Tags.DeleteButton).GetComponent<Button>();
		deleteAllButton = GameObject.FindWithTag(Tags.DeleteAllButton).GetComponent<Button>();

		loadButton.interactable = false;
		deleteButton.interactable = false;

		if (GameButtonManager.instance.IsEmpty()) {
			deleteAllButton.interactable = false;
		} else {
			deleteAllButton.interactable = true;
		}
	}

	public void DeleteButton() {
		DetachGameButton(targetButton);
		manager.RemoveGameButton(targetButton);
		UpdateInteractivity(false, loadButton, deleteButton);

		if (GameButtonManager.instance.IsEmpty()) {
			UpdateInteractivity(false, deleteAllButton);
		}
	}

	public void DeleteAllButtons() {
		manager.RemoveAllGameButtons();

		// Disallow the user to click on the load, delete and delete all buttons
		UpdateInteractivity(false, loadButton, deleteButton, deleteAllButton);
	}

	public void LoadTargetButton() {
		targetButton.LoadLevel();
		loadButton.interactable = false;
	}

	public override void SetTargetButton(GameButton button) {
		base.SetTargetButton(button);

		// Allow the player to click on the load and delete button
		UpdateInteractivity(true, loadButton, deleteButton);
	}

	private void UpdateInteractivity(bool isInteractable, params Button[] buttons) {
		for (int i = 0; i < buttons.Length; i++) {
			buttons[i].interactable = isInteractable;
		}
	}

	private void DetachGameButton(GameButton gameButton) {
		gameButton.transform.SetParent(null);
	}
}
