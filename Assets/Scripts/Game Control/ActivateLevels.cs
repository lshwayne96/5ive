/*
 * This script is attached to the empty Levels GameObject
 * in the OverviewMenu GameObject in Scene 0_Main Menu.
 */

using UnityEngine;
using UnityEngine.UI;

public class ActivateLevels : MonoBehaviour {

    private GameData gameData;
    private Button[] levelButtons;
    private bool hasInitialised;

    private void Initialise() {
        gameData = GameDataManager.GetGameData();
        levelButtons = GetComponentsInChildren<Button>();
        GameObject.FindGameObjectWithTag("OverviewMenu").SetActive(false);
    }

    /*
     * Unlock all the levels that the player has completed
     * with the exception of the always-unlocked first level
     */
    private void OnEnable() {
        if (!hasInitialised) {
            Initialise();
            hasInitialised = true;
        }

        for (int i = 0; i < gameData.GetNumLevelsCompleted(); i++) {
            levelButtons[i].interactable = true;
        }
    }
}
