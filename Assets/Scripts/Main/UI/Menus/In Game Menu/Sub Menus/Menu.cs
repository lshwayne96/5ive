using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script represents a menu. The main function of this script
/// is to populate the menu with buttons.
/// </summary>
/// <remarks>
/// By default, the file name associated with a level file is tagged
/// with a specific preamble. Before the file name can be displayed
/// on the button, the preamble has to be trimmed.
/// </remarks>
public abstract class Menu : MonoBehaviour {

	/// <summary>
	/// The button prefab.
	/// </summary>
	public GameObject prefab;

	/// <summary>
	/// Represents a mapping of all button names
	/// and their corresponding button scripts
	/// that are active in the menu.
	/// </summary>
	protected Dictionary<string, GameButton> nameButtonMapping;

	/// <summary>
	/// Represents a pair consisting of the button name
	/// and the corresponding button script
	/// that is targeted for deletion.
	/// </summary>
	protected KeyValuePair<string, GameButton> targetButton;

	/*
	/// <summary>
	/// An array of all the <code>GameButton</code> scripts that
	/// are active in the menu.
	/// </summary>
	/// <remarks>
	/// The scripts are cached here for better performance instead of
	/// using <code>GetComponent()</code> everytime the scripts
	/// need to be accessed.
	/// </remarks>
	protected Button[] activeButtons;
	*/

	/// <summary>
	/// Represents the names of all the buttons that are deleted in the load menu.
	/// </summary>
	/// <remarks></remarks>
	protected static HashSet<string> deletedButtonNames;

	/// <summary>
	/// Represents the names, and date and time of the buttons that are
	/// overwritten in the save menu.
	/// </summary>
	protected static Dictionary<string, DateTime> nameDateTimeMapping;

	private void Start() {
		Init();
	}

	public virtual void Init() {
		nameButtonMapping = new Dictionary<string, GameButton>();

		deletedButtonNames = new HashSet<string>();
		nameDateTimeMapping = new Dictionary<string, DateTime>();

		CreateButtons();
	}

	/// <summary>
	/// Populates the menu with buttons.
	/// </summary>
	/// <remarks>
	/// This method is inefficient in that even if there are existing buttons
	/// in the menu, it will not take them into account when adding new buttons.
	/// What it does is that it retrieves all the current level files,
	/// gets their information and then creates buttons corresponding to them.
	/// </remarks>
	protected void CreateButtons() {
		GameButtonInfo[] gameButtonInfos = Game.instance.storage.FetchGameButtons();

		for (int i = 0; i < gameButtonInfos.Length; i++) {
			print("Create button");
			CreateButton(gameButtonInfos[i]);
		}
	}

	private void CreateButton(GameButtonInfo gameButtonInfo) {
		GameButton button = Instantiate(prefab).GetComponent<GameButton>();
		AttachButtonToMenu(button);

		// Cache the buttons for easy deletion
		nameButtonMapping.Add(gameButtonInfo.FileName, button);

		button.SetUp(gameButtonInfo);
	}

	/// <summary>
	/// Adds <code>button</code> to the menu and brings it to the top.
	/// </summary>
	/// <param name="button">Button.</param>
	private void AttachButtonToMenu(GameButton button) {
		button.AttachTo(transform);
		button.MoveToTopOfMenu();
		print("Attached");
	}

	/// <summary>
	/// Add or remove buttons whenever their corresponding level files are added or removed.
	/// </summary>
	/// <remarks>
	/// This method is meant to be called whenever the menu is made active.
	/// The underlying reason for this method is that since buttons can be modified in both
	/// the save and load menus, there is a disconnect between them, and hence this
	/// method modifies the menu's buttons to ensure consistency.
	/// </remarks>
	public abstract void UpdateButtons();


	/// <summary>
	/// Sets the <code>button</code> as a target.
	/// </summary>
	/// <remarks>
	/// This method is called when the user single clicks
	/// on a button in the menu.
	/// </remarks>
	/// <param name="button">Button.</param>
	public virtual void SetTargetButton(GameButton button) {
		string buttonName = button.NameLabel.text;
		targetButton = new KeyValuePair<string, GameButton>(buttonName, button);
	}

	/// <summary>
	/// Delete all buttons and their corresponding level files.
	/// </summary>
	public virtual void DeleteAllButtons() {
		foreach (GameButton button in nameButtonMapping.Values) {
			button.DeleteCorrespondingFile();
			Destroy(button);
		}
		nameButtonMapping.Clear();
	}

	/// <summary>
	/// Checks if there are still level files.
	/// </summary>
	/// <returns><c>true</c>, if there are still level files, <c>false</c> otherwise.</returns>
	protected bool AreFilesPresent() {
		return nameButtonMapping.Count > 0;
	}

	/// <summary>
	/// Checks if a level file exists.
	/// </summary>
	/// <returns><c>true</c>, if the level file exists, <c>false</c> otherwise.</returns>
	/// <param name="fileName">File name.</param>
	public bool DoesFileWithSameNameExist(string fileName) {
		return nameButtonMapping.ContainsKey(fileName);
	}

	/*
	protected void OnDisable() {
		for (int i = 0; i < activeButtons.Length; i++) {
			activeButtons[i].interactable = false;
		}
	}
	*/
}
