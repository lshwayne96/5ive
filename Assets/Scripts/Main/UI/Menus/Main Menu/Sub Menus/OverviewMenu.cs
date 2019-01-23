using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script represents the overview menu in the main menu level.
/// </summary>
public class OverviewMenu : MonoBehaviour {

	private Button[] levelButtons;
	private bool hasInit;

	/*
     * Unlock all the levels that the player has completed
     * with the exception of the always-unlocked first level
     */
	private void Start() {
		if (!hasInit) {
			levelButtons = GetComponentsInChildren<Button>();
			GameObject.FindGameObjectWithTag(Tags.OverviewMenu).SetActive(false);
			hasInit = true;
		}

		UnlockLevelButtons();
	}

	/// <summary>
	/// Unlocks all the levels that the play has completed,
	/// with the exception of the always unlocked main menu level.
	/// </summary>
	private void UnlockLevelButtons() {
		for (int i = 0; i < Game.instance.NumLevelsCompleted; i++) {
			levelButtons[i].interactable = true;
		}
	}
}
