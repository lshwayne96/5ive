/*
 * This script calls UpdateButtons() from the FileButtonManager instance
 * attached to the menu's scroll view content to either add or remove buttons.
 */

using UnityEngine;

public class UpdateFileButtons : MonoBehaviour {

    private bool hasInitialised;
    private FileButtonManager fileButtonManager;
    private GameObject parentMenu;

    private void OnEnable() {
        if (parentMenu == null) {
            parentMenu = GameMenu.SetParentMenu(parentMenu);
        }

        if (fileButtonManager == null) {
            fileButtonManager = GetComponentInChildren<FileButtonManager>();
        }

        if (!hasInitialised) {
            fileButtonManager.Initialise();
            hasInitialised = true;
        } else {
            fileButtonManager.UpdateButtons();
        }
    }
}
