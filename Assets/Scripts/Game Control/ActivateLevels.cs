using UnityEngine;
using UnityEngine.UI;

public class ActivateLevels : MonoBehaviour {

    private GameData gameData;

    private void Start() {
        gameData = GameDataManager.GetGameData();
    }

    /*
     * Unlock all the levels that the player has completed
     * with the exception of the always-unlocked first level
     */
    private void OnEnable() {
        Button[] levelButtons = GetComponentsInChildren<Button>();
        for (int i = 1; i <= gameData.GetNumLevelsCompleted(); i++) {
            levelButtons[i].interactable = true;
        }
    }
}
