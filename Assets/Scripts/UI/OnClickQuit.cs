/*
 * This script quits the game upon
 * interacting with the attached gameObject.
 */

using UnityEngine;

public class OnClickQuit : MonoBehaviour {

    public void Quit() {
        SaveBeforeQuit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }

    private void SaveBeforeQuit() {
        string saveFilePath = GameFile.GetSaveFilePath();
        GameFile.Serialise(saveFilePath, GameDataManager.GetGameData());
    }
}
