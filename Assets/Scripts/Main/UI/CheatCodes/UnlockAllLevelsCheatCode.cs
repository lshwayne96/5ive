using Main.Commons;
using UnityEngine;
using UnityEngine.UI;

namespace Main.UI.CheatCodes {

	public class UnlockAllLevelsCheatCode : CheatCode {

		public const string Code = "power overwhelming";

		public override void Run(InputField inputField) {
			Button[] buttons = GameObject.FindWithTag(Tags.LevelButtons)
				.GetComponentsInChildren<Button>();
			UnlockAllLevels(buttons);
			ClearAndHideInputField(inputField);
		}

		/// <summary>
		/// Unlocks all levels by allowing the user to click
		/// on the level buttons.
		/// </summary>
		/// <param name="buttons">The level buttons to be made clickable.</param>
		private void UnlockAllLevels(Button[] buttons) {
			for (int i = 0; i < buttons.Length; i++) {
				buttons[i].interactable = true;
			}
		}
	}

}