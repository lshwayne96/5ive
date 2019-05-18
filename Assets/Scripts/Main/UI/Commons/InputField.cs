using Main.UI.CheatCodes;
using UnityEngine;
using UnityEngine.UI;

namespace Main.UI.Commons {

	public class InputField : MonoBehaviour {

		/// <summary>
		/// The input field in which the cheat codes can be entered.
		/// </summary>
		private UnityEngine.UI.InputField inputField;

		private void Start() {
			inputField = GetComponent<UnityEngine.UI.InputField>();
		}

		public void RunCheatCodeEntered() {
			string input = inputField.text.ToLower();

			switch (input) {
				case UnlockAllLevelsCheatCode.Code:
					new UnlockAllLevelsCheatCode().Run(inputField);
					break;
				default:

					// Ignore input since not a valid cheat code
					return;
			}
		}
	}

}