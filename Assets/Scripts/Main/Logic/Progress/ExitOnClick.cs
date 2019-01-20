using UnityEngine;

/// <summary>
/// This script exits the game.
/// </summary>
public class ExitOnClick : MonoBehaviour {

	public void Quit() {
		SaveBeforeExit();

#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

	}

	private void SaveBeforeExit() {
		Game.SaveBeforeExit();
	}
}
