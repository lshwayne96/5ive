using UnityEngine;

/// <summary>
/// This script exits the game.
/// </summary>
public class ExitOnClick : MonoBehaviour {

	public void Quit() {
		SaveBeforeQuit();

#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

	}

	private void SaveBeforeQuit() {
		string path = StorageUtil.GetDirectoryPath(FileType.Game);
		GameDataManager.SaveGame(path);
	}
}
