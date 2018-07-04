using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadGame : MonoBehaviour {
    private String saveFile;
    private GameObject player;
    private GameObject ball;

    private void Start() {
        saveFile = Application.persistentDataPath;
        player = GameObject.FindWithTag("Player");
        ball = GameObject.FindWithTag("TeleportationBall");
    }

    public void Load() {
        //Debug.Log("Called Load()");
        try {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Open(saveFile + "/player_info.dat", FileMode.Open);
            LevelData levelData = (LevelData)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();

            if (RestoreGame.restoreGame != null) {
                RestoreGame.restoreGame.Take(levelData, player, ball);
            }

            SceneManager.LoadScene(levelData.sceneBuildIndex);

            //Debug.Log("Scene loaded");

        } catch (FileNotFoundException) {
            Console.WriteLine("Cannot load game. Game file has been deleted or moved.");
        }
    }
}