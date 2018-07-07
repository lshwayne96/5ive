using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadGame : MonoBehaviour {
    private String saveFilePath;
    private String saveDirectoryPath;
    private GameObject player;
    private GameObject ball;

    private void Start() {
        saveFilePath = Application.persistentDataPath;
        saveDirectoryPath = saveFilePath + "/Saved Games";
        player = GameObject.FindWithTag("Player");
        ball = GameObject.FindWithTag("TeleportationBall");
    }

    public void Load() {
        //Debug.Log("Called Load()");
        try {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Open(saveDirectoryPath + "/game_info.dat", FileMode.Open);
            LevelData levelData = (LevelData)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();

            if (RestoreGame.restoreGame != null) {
                RestoreGame.restoreGame.Take(levelData, player, ball);
            }

            SceneManager.LoadScene(levelData.sceneBuildIndex);

            //Debug.Log("Scene loaded");

        } catch (DirectoryNotFoundException) {
            Debug.Log("Saved Games folder has been deleted or moved.");
            Console.WriteLine("Saved Games folder has been deleted or moved.");
        } catch (FileNotFoundException) {
            Debug.Log("Game file has been deleted or moved.");
            Console.WriteLine("Game file has been deleted or moved.");
        }
    }
}