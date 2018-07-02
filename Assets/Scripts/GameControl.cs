using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameControl : MonoBehaviour {
    public static GameControl gameControl;
    private String saveFile;
    private static GameObject player;
    private static GameObject ball;

    private void Awake() {
        if (gameControl == null) {
            gameControl = this;
            DontDestroyOnLoad(gameControl);

        } else if (gameControl != this) {
            Destroy(this);
        }
    }

    private void Start() {
        saveFile = Application.persistentDataPath;
        Debug.Log(saveFile);
        player = GameObject.FindWithTag("Player");
        ball = GameObject.FindWithTag("TeleportationBall");
    }

    public static GameObject[] package() {
        return new GameObject[] { player, ball };
    }

    public void Save() {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = File.Create(saveFile + "/player_info.data");
        GameData gameData = new GameData();
        gameData.add();
        binaryFormatter.Serialize(fileStream, gameData);
        fileStream.Close();
    }

    public void Load() {
        try {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Open(saveFile + "/player_info.data", FileMode.Open);
            GameData gameData = (GameData)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
        } catch (FileNotFoundException) {
            Console.WriteLine("Cannot load game. Game file has been deleted or moved.");
        }
    }
}

[Serializable]
public class GameData {
    HashSet<LevelData> gameData = new HashSet<LevelData>();

    public void add() {
        Scene scene = SceneManager.GetActiveScene();
        GameObject[] gameObjects = GameControl.package();
        GameObject player = gameObjects[0];
        GameObject ball = gameObjects[1];
        LevelData levelData = new LevelData(scene, player, ball);
        gameData.Add(levelData);
    }
}

public class LevelData {
    private Scene scene;
    private GameObject player;
    private GameObject ball;

    public LevelData(Scene scene, GameObject player, GameObject ball) {
        this.scene = scene;
        this.player = player;
        this.ball = ball;
    }
}
