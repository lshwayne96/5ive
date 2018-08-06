using UnityEngine;

public class SaveGame : MonoBehaviour {

    private string saveFilePath;
    private GameData gameData;

    void Start() {
        saveFilePath = GameFile.GetSaveFilePath();



    }
}
