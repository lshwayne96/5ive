using Main.UI.InGame.Menus;
using UnityEngine;

namespace Main.Commons.Util {

	/// <summary>
	/// This is a utility class that contains utility methods
	/// pertaining to menu interaction.
	/// </summary>
	public static class MenuUtil {

		/// <summary>
		/// Gets a suitable menu dependng on which
		/// menu is active in the scene.
		/// </summary>
		/// <returns>A suitable menu.</returns>
		public static InGameMenu GetSuitableMenu() {
			GameObject menuGO = GameObject.FindWithTag(Tags.SaveMenu);
			if (menuGO == null) {
				menuGO = GameObject.FindWithTag(Tags.LoadMenu);
			}

			return menuGO.GetComponent<InGameMenu>();
		}

		/// <summary>
		/// Checks if the <code>menu</code> is of <code>SaveMenu</code> type.
		/// </summary>
		/// <returns><c>true</c>, if the menu is of <code>SaveMenu</code> type,
		/// <c>false</c> otherwise.</returns>
		/// <param name="menu">Menu.</param>
		public static bool IsSaveMenu(InGameMenu menu) {
			return menu.CompareTag(Tags.SaveMenu);
		}

		/// <summary>
		/// Checks if the <code>menu</code> is of <code>LoadMenu</code> type.
		/// </summary>
		/// <returns><c>true</c>, if the menu is of <code>LoadMenu</code> type,
		/// <c>false</c> otherwise.</returns>
		/// <param name="menu">Menu.</param>
		public static bool IsLoadMenu(InGameMenu menu) {
			return menu.CompareTag(Tags.LoadMenu);
		}
	}

}