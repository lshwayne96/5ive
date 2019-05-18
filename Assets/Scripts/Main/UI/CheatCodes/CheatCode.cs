using UnityEngine.UI;

namespace Main.UI.CheatCodes {

    public abstract class CheatCode {

        /// <summary>
        /// Runs the cheat code.
        /// </summary>
        /// <param name="inputField">The input field in which
        /// the cheat code is entered.</param>
        public abstract void Run(InputField inputField);

        /// <summary>
        /// Clears and hides the input field.
        /// </summary>
        protected void ClearAndHideInputField(InputField inputField) {
            inputField.text = string.Empty;
            inputField.DeactivateInputField();
            inputField.gameObject.SetActive(false);
        }
    }

}