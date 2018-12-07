/*
 * This script is attached to the in-game menus to give them
 * button update functionality.
 * It calls UpateButtons() from the FileButtonManager instance
 * attached to the menu's scroll view content to either add or remove buttons.
 */

using UnityEngine;

public class UpdateFileButtons : MonoBehaviour {

    private bool hasInitialised;
    private FileButtonManager fileButtonManager;

    private void OnEnable() {
        // Set the fileButtonManager only once
        if (!fileButtonManager) {
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
