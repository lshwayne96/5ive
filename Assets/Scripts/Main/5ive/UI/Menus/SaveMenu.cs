using Main._5ive.UI.Buttons.GameButtons;

namespace Main._5ive.UI.Menus {

	/// <summary>
	/// This script extends the <code>Menu</code> script
	/// and represents a menu where the user can save the game progress.
	/// </summary>
	/// <remarks>
	/// The functions of this script are to populate the menu with buttons
	/// and provide level file overwriting functionality.
	/// </remarks>
	public class SaveMenu : Menu {
		private void Start() {
			Init();
		}

		public void AddButton() {
			GameButton gameButton = gameButtonCollection.GetFirst();
			AttachGameButton(gameButton);
		}

		/// <summary>
		/// Checks if a level file exists.
		/// </summary>
		/// <returns><c>true</c>, if the level file exists, <c>false</c> otherwise.</returns>
		/// <param name="fileName">File name.</param>
		public bool DoesFileWithSameNameExist(string fileName) {
			return gameButtonCollection.Contains(fileName);
		}

		/// <summary>
		/// Overwrites the target button.
		/// </summary>
		public void OverwriteTargetButton() {
			TargetButton.OverwriteLinkedFile();
			gameButtonCollection.Sort();
			ReattachGameButton(TargetButton);
		}

		private void AttachGameButton(GameButton gameButton) {
			gameButton.transform.SetParent(content.transform);
			gameButton.transform.SetAsFirstSibling();
		}

		private void ReattachGameButton(GameButton gameButton) {
			gameButton.transform.SetSiblingIndex(0);
		}
	}

}
