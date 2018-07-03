using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SaveGame : MonoBehaviour {
    private String saveFile;
    private GameObject player;
    private GameObject ball;

    private void Start() {
        saveFile = Application.persistentDataPath;
        player = GameObject.FindWithTag("Player");
        ball = GameObject.FindWithTag("TeleportationBall");
    }

    public void Save() {
        //Debug.Log("Called Saved()");

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = File.Create(saveFile + "/player_info.data");

        Scene scene = SceneManager.GetActiveScene();

        Debug.Log("Velocity");
        Vector2 vector = player.GetComponent<Rigidbody2D>().velocity;
        Debug.Log(vector.x);
        Debug.Log(vector.y);

        Debug.Log("Position");
        Vector3 vector1 = player.transform.localPosition;
        Debug.Log(vector1.x);
        Debug.Log(vector1.y);
        Debug.Log(vector1.z);

        LevelData levelData = new LevelData(scene, player, ball);

        binaryFormatter.Serialize(fileStream, levelData);
        fileStream.Close();
    }
}
