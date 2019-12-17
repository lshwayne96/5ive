using Main._5ive.UI.Buttons.GameButtons;
using UnityEngine;
using UnityEngine.UI;

namespace Main._5ive.UI.Menus {

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
		protected GameObject content;
		protected GameButtonCollection gameButtonCollection;

		/// <summary>
		/// Sets the <code>button</code> as a target.
		/// </summary>
		/// <remarks>
		/// This method is called when the user single clicks
		/// on a button in the menu.
		/// </remarks>
		public GameButton TargetButton { get; set; }

		protected virtual void Init() {
			content = GetContentGameObject();
			gameButtonCollection = transform.parent.GetComponentInChildren<GameButtonCollection>();
			CreateButtons();
		}

		private GameObject GetContentGameObject() {
			return GetComponentInChildren<ScrollRect>().transform.GetChild(0).GetChild(0).gameObject;
		}

		private void CreateButtons() {
			var gameButtons = gameButtonCollection.GetAll();
			foreach (GameButton gameButton in gameButtons) {
				gameButton.transform.SetParent(content.transform);
			}
		}
	}

}