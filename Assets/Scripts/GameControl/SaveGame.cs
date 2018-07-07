using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class SaveGame : MonoBehaviour {
    public InputField inputField;
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

    public void Save() {
        //Debug.Log("Called Saved()");

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        DirectoryInfo directoryInfo = Directory.CreateDirectory(saveDirectoryPath);
        FileStream fileStream = File.Create(saveDirectoryPath + "/" + inputField.text + ".dat");

        //Debug.Log(directoryInfo.FullName);
        //Debug.Log(saveFile);

        Scene scene = SceneManager.GetActiveScene();
        LevelData levelData = new LevelData(scene, player, ball);

        binaryFormatter.Serialize(fileStream, levelData);
        fileStream.Close();

        /*
        Debug.Log("Velocity");
        Vector2 vector = player.GetComponent<Rigidbody2D>().velocity;
        Debug.Log(vector.x);
        Debug.Log(vector.y);

        Debug.Log("Position");
        Vector3 vector1 = player.transform.localPosition;
        Debug.Log(vector1.x);
        Debug.Log(vector1.y);
        Debug.Log(vector1.z);
        */
    }
}
