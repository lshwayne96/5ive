using UnityEngine;

/// <summary>
/// This script is used to add or remove buttons from the menu
/// whenever corresponding level files are added or removed.
/// </summary>
/// <remarks>
/// This script is meant to be attached to the menu.
/// </remarks>
public class MenuUpdater : MonoBehaviour {

	private bool isMenuInit;

	private Menu manager;

	private void OnEnable() {
		if (manager == null) {
			manager = GetComponentInChildren<Menu>();
		}

		if (!isMenuInit) {
			manager.Init();
			isMenuInit = true;
		} else {
			manager.UpdateButtons();
		}
	}
}
