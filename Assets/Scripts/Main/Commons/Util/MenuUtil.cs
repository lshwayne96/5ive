using UnityEngine;

/// <summary>
/// This is a utility class that contains utility methods
/// pertaining to menu interaction.
/// </summary>
public static class MenuUtil {

	/// <summary>
	/// Sets the menu to either the <c>SaveMenu</c>
	/// or the <c>LoadMenu</c> depending on the scene.
	/// </summary>
	public static void Set(GameObject menu) {
		menu = GameObject.FindWithTag("SaveMenu");
		if (menu == null) {
			menu = GameObject.FindWithTag("LoadMenu");
		}
	}

	/// <summary>
	/// Checks if the menu is the <c>SaveMenu</c>.
	/// </summary>
	/// <returns>
	/// <c>true</c>, if menu is the <c>SaveMenu</c>,
	/// <c>false</c> otherwise.</returns>
	/// <param name="menu">Menu.</param>
	public static bool IsSaveMenu(GameObject menu) {
		return menu.CompareTag("SaveMenu");
	}

	/// <summary>
	/// Checks if the menu is the <c>LoadMenu</c>.
	/// </summary>
	/// <returns>
	/// <c>true</c>, if menu is the <c>LoadMenu</c>,
	/// <c>false</c> otherwise.</returns>
	/// <param name="menu">Menu.</param>
	public static bool IsLoadMenu(GameObject menu) {
		return menu.CompareTag("LoadMenu");
	}
}