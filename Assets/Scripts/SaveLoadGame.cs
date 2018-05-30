using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoadGame : MonoBehaviour {
    private String saveFile = Application.persistentDataPath;

    public void Save() {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = File.Create(saveFile);
        GameData gameData = new GameData();
        gameData.populate();
        binaryFormatter.Serialize(fileStream, gameData);
        fileStream.Close();
    }

    public void Load() {
        try {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Open(saveFile, FileMode.Open);
            GameData gameData = (GameData)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
        } catch (FileNotFoundException) {
            Console.WriteLine("Cannot load game. Game file has been deleted or moved.");
        }
    }
}
