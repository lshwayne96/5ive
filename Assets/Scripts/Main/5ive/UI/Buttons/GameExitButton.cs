using Main._5ive.Messaging;
using Main._5ive.Messaging.Events;
using UnityEngine;

namespace Main._5ive.UI.Buttons {

    /// <summary>
    /// This script represents a button function where the user
    /// exits the game if the button is clicked.
    /// </summary>
    /// <remarks>
    /// This script is attached to the exit buttons in the main menu
    /// and in the in-game menu.
    /// </remarks>
    public class GameExitButton : MonoBehaviour {
        private EventsCentre eventsCentre;

        public void Start() {
            eventsCentre = EventsCentre.GetInstance();
        }

        public void ExitGame() {
            eventsCentre.Publish(new GameExitEvent());

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
	        Application.Quit();
#endif
        }
    }

}