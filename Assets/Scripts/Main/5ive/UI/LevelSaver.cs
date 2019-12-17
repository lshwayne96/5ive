using Main._5ive.Messaging;
using Main._5ive.Messaging.Events;
using Main._5ive.Model.Story.Messages;
using Main._5ive.UI.Menus;
using Main._5ive.UI.Notifications;
using UnityEngine;
using UnityEngine.UI;

namespace Main._5ive.UI {

	/// <summary>
	/// This script represents an <see cref="InputField"/> in the
	/// in game save menu that takes in file name and saves the
	/// current level progress into a file named as such.
	/// </summary>
	/// <remarks>
	/// The method that implements this function, <see cref="SaveLevel()"/>,
	/// is caled when the user presses the return key in the input field.
	/// </remarks>
	public class LevelSaver : MonoBehaviour {
		private InputField inputField;
		private SaveMenu saveMenu;

		private EventsCentre eventsCentre;

		private void Start() {
			inputField = GetComponent<InputField>();
			saveMenu = GetComponentInParent<SaveMenu>();

			eventsCentre = EventsCentre.GetInstance();
		}

		/// <summary>
		/// Saves the level.
		/// </summary>
		/// <remarks>
		/// This is a wrapper function over the overloaded core <code>SaveLevel#Save(...)</code>.
		/// If nothing is entered in the input field, the function does nothing.
		/// </remarks>
		public void SaveLevel() {
			string fileName = inputField.text;

			bool isInputEmpty = fileName.Equals(string.Empty);
			if (isInputEmpty) {
				return;
			}

			bool isNameTaken = saveMenu.DoesFileWithSameNameExist(fileName);
			if (isNameTaken) {
				NotificationManager.Send(new FileAlreadyExistsMessage());
				return;
			}

			eventsCentre.Publish(new LevelSaveEvent());

			saveMenu.AddButton();
			ClearInputField();
		}

		private void ClearInputField() {
			inputField.text = string.Empty;
		}

		private void OnEnable() {
			inputField = GetComponent<InputField>();
			if (inputField != null) {
				inputField.ActivateInputField();
			}
		}
	}

}