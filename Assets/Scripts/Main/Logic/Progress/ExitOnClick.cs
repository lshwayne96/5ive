/*
 * This script quits the game upon
 * interacting with the attached gameObject.
 */

using UnityEngine;

/// <summary>
/// This script exits the game.
/// </summary>
public class QuitOnClick : MonoBehaviour {

	public void Quit() {
		SaveBeforeQuit();

#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

	}

	private void SaveBeforeQuit() {
		string saveFilePath = StorageUtil.GetDirectoryPath(FileType.Game);
		GameDataManager.SaveGame(saveFilePath);
	}
}
