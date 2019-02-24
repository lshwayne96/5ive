/// <summary>
/// This script extends the <code>Menu</code> script
/// and represents a menu where the user can save the game progress.
/// </summary>
/// <remarks>
/// The functions of this script are to populate the menu with buttons
/// and provide level file overwriting functionality.
/// </remarks>
public class SaveMenu : Menu {

	public void AddButton() {
		GameButton gameButton = manager.GetNewlyAddedGameButton();
		AttachGameButton(gameButton);
	}

	/// <summary>
	/// Overwrites the target button.
	/// </summary>
	public void OverwriteTargetButton() {
		targetButton.OverwriteLinkedFile();
		manager.UpdateGameButtonsOrdering();
		ReattachGameButton(targetButton);
	}

	private void AttachGameButton(GameButton gameButton) {
		gameButton.transform.SetParent(content.transform);
		gameButton.transform.SetAsFirstSibling();
	}

	private void ReattachGameButton(GameButton gameButton) {
		gameButton.transform.SetSiblingIndex(0);
	}
}
