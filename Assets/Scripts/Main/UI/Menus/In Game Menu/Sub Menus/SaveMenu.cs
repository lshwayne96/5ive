using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script extends the <code>Menu</code> script
/// and represents a menu where the user can save the game progress.
/// </summary>
/// <remarks>
/// The functions of this script are to populate the menu with buttons
/// and provide level file overwriting functionality.
/// </remarks>
public class SaveMenu : Menu {

	//private Button overwriteButton;

	private static Logger logger = Logger.GetLogger();

	public override void Init() {
		base.Init();

		//activeButtons = new Button[] { overwriteButton };
	}

	/// <summary>
	/// Updates the buttons in the menu.
	/// </summary>
	/// <remarks>
	/// There are two scenarios in which the buttons will be updated.
	/// <list type="number">
	/// <item><description>
	/// There are buttons that have been deleted in the other menu but
	/// not in this. In that case, those deleted buttons will be deleted
	/// in this menu too.
	/// </description></item>
	/// <item><description>
	/// There are no more saved files. In that case, every button in
	/// this menu will be deleted since there are no more saved files to
	/// represent.
	/// </description></item>
	/// </list>
	/// </remarks>
	public override void UpdateButtons() {
		if (Game.instance.storage.HasLevels()) {
			logger.LogHigh("Saved files are present");

			print("Removing");
			RemoveDeletedButtons();
			logger.LogHigh("Removed deleted buttons");

			CreateButtons();
			logger.LogHigh("Buttons created");
		} else {
			logger.LogHigh("No saved files are present");
			DeleteAllButtons();
		}
	}

	/// <summary>
	/// Removes buttons that have been deleted in the other menu
	/// but not in this menu.
	/// </summary>
	private void RemoveDeletedButtons() {
		foreach (string fileName in deletedButtonNames) {
			if (nameButtonMapping.ContainsKey(fileName)) {
				Destroy(nameButtonMapping[fileName]);
				nameButtonMapping.Remove(fileName);
			}
		}
		deletedButtonNames.Clear();
		print("Done removing");
	}

	public override void SetTargetButton(GameButton button) {
		base.SetTargetButton(button);

		// Allow the selected target button to be overwritten
		//overwriteButton.interactable = true;
	}

	/// <summary>
	/// Overwrites the target button.
	/// </summary>
	public void OverwriteTargetButton() {
		GameButton button = targetButton.Value.GetComponent<GameButton>();
		button.OverwriteCorrespondingFile();

		string buttonName = button.NameLabel.text;
		string dateTime = button.DateTimeLabel.text;
		try {
			nameDateTimeMapping.Add(buttonName, DateTime.Parse(dateTime));
		} catch (ArgumentException) {
			nameDateTimeMapping.Remove(buttonName);
			nameDateTimeMapping.Add(buttonName, DateTime.Parse(dateTime));
		}

		//overwriteButton.interactable = false;
	}
}
