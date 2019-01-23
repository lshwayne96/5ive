using UnityEngine;

/// <summary>
/// This script represents a button function where the user
/// exits the game if the button is clicked.
/// </summary>
/// <remarks>
/// This script is attached to the exit buttons in the main menu
/// and in the in-game menu.
/// </remarks>
public class ExitButton : MonoBehaviour {

	public void Exit() {
		SaveBeforeExit();

#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
	        Application.Quit();
#endif

	}

	private void SaveBeforeExit() {
		Game.instance.SaveBeforeExit();
	}
}
