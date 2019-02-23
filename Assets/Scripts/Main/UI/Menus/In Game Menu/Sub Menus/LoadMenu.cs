using System;
using System.Collections.Generic;
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

		//activeButtons = new Button[] { loadButton, deleteButton, deleteAllButton };

		loadButton.interactable = false;
		deleteButton.interactable = false;
		if (Game.instance.storage.HasLevels()) {
			deleteAllButton.interactable = true;
		} else {
			deleteAllButton.interactable = false;
		}
	}

	public override void UpdateButtons() {
		if (Game.instance.storage.HasLevels()) {
			deleteAllButton.interactable = true;
			UpdateOverwrittenButtons();
			CreateButtons();
		} else {
			deleteAllButton.interactable = false;
		}
	}

	private void UpdateOverwrittenButtons() {
		List<KeyValuePair<string, DateTime>> pairs = new List<KeyValuePair<string, DateTime>>(nameDateTimeMapping);
		// Sort the pairs, with the pairs that have the oldest date time placed at the front
		pairs.Sort((p1, p2) => DateTime.Compare(p1.Value, p2.Value));

		foreach (KeyValuePair<string, DateTime> pair in pairs) {
			UpdateOverwrittenButton(pair);
		}
		nameDateTimeMapping.Clear();
	}

	private void UpdateOverwrittenButton(KeyValuePair<string, DateTime> pair) {
		if (nameButtonMapping.ContainsKey(pair.Key)) {
			string buttonName = pair.Key;
			GameButton button = nameButtonMapping[buttonName];
			DateTime dateTime = pair.Value;
			button.SetDateTime(dateTime);
			button.MoveToTopOfMenu();
		}
	}

	public void LoadTargetButton() {
		GameButton button = targetButton.Value;
		button.LoadLevel();
		loadButton.interactable = false;
	}

	/// <summary>
	/// Delete the button and it's corresponding level file.
	/// </summary>
	public void DeleteTargetButton() {
		string path = targetButton.Key;
		Game.instance.storage.DeleteLevel(path);
		Destroy(targetButton.Value.gameObject);

		string buttonName = targetButton.Key;
		nameButtonMapping.Remove(buttonName);
		deletedButtonNames.Add(buttonName);
		ToggleButtonInteractivity(false, loadButton, deleteButton);

		if (!AreFilesPresent()) {
			deleteAllButton.interactable = false;
		}
	}

	public override void DeleteAllButtons() {
		base.DeleteAllButtons();

		// Allow the user to click on the load, delete and delete all buttons
		ToggleButtonInteractivity(false, loadButton, deleteButton, deleteAllButton);
		nameButtonMapping.Clear();
	}

	public override void SetTargetButton(GameButton button) {
		base.SetTargetButton(button);

		// Allow the player to click on the load and delete button
		ToggleButtonInteractivity(true, loadButton, deleteButton);
	}

	private void ToggleButtonInteractivity(bool isInteractable, params Button[] buttons) {
		for (int i = 0; i < buttons.Length; i++) {
			buttons[i].interactable = isInteractable;
		}
	}
}
