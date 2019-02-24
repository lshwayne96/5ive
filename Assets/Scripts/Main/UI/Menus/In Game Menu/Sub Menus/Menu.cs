using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

	protected GameButton targetButton;

	protected GameObject content;

	protected GameButtonManager manager;

	private void Start() {
		Init();
	}

	public virtual void Init() {
		content = GetContentGameObject();
		manager = GameButtonManager.instance;

		CreateButtons();
	}

	private GameObject GetContentGameObject() {
		return GetComponentInChildren<ScrollRect>().transform.GetChild(0).GetChild(0).gameObject;
	}

	private void CreateButtons() {
		List<GameButton> gameButtons = manager.GetGameButtons();
		foreach (GameButton gameButton in gameButtons) {
			gameButton.transform.SetParent(content.transform);
		}
	}

	/// <summary>
	/// Sets the <code>button</code> as a target.
	/// </summary>
	/// <remarks>
	/// This method is called when the user single clicks
	/// on a button in the menu.
	/// </remarks>
	/// <param name="button">Button.</param>
	public virtual void SetTargetButton(GameButton button) {
		targetButton = button;
	}

	/// <summary>
	/// Checks if a level file exists.
	/// </summary>
	/// <returns><c>true</c>, if the level file exists, <c>false</c> otherwise.</returns>
	/// <param name="fileName">File name.</param>
	public bool DoesFileWithSameNameExist(string fileName) {
		return GameButtonManager.instance.Contains(fileName);
	}
}
