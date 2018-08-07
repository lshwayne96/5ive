/*
 * This script is attached to the empty Levels GameObject
 * in the OverviewMenu GameObject in Main Menu scene.
 */

using UnityEngine;
using UnityEngine.UI;

public class ActivateLevels : MonoBehaviour {

    private Button[] levelButtons;
    private bool hasInitialised;

    private void Initialise() {
        levelButtons = GetComponentsInChildren<Button>();
        GameObject.FindGameObjectWithTag("OverviewMenu").SetActive(false);
    }

    /*
     * Unlock all the levels that the player has completed
     * with the exception of the always-unlocked first level
     */
    private void Start() {
        if (!hasInitialised) {
            Initialise();
            hasInitialised = true;
        }

        UnlockLevelButtons();
    }

    private void UnlockLevelButtons() {
        for (int i = 0; i < GameDataManager.GetNumLevelsCompleted(); i++) {
            levelButtons[i].interactable = true;
        }
    }
}
