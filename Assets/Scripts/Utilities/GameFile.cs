/*
 * This is a utility class.
 * It provides methods get the save file path,
 * and to serialise and deserialise data.
 */

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameFile {
    // The standard directory path where level files will be saved
    private static string saveDirectoryPath =
        Application.persistentDataPath + "/Saved Games/";
    private static string fileNameExtension = ".dat";

    // Hide the constructor
    private GameFile() {

    }

    public static string GetSaveFilePath() {
        return saveDirectoryPath + "CurrentGame" + fileNameExtension;
    }

    public static void Serialise(string filePath, object data) {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        Directory.CreateDirectory(saveDirectoryPath);
        FileStream fileStream = File.Create(filePath);
        binaryFormatter.Serialize(fileStream, data);
        fileStream.Close();
    }

    public static T Deserialise<T>(string filePath) {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = File.Open(filePath, FileMode.Open);
        T data = (T)binaryFormatter.Deserialize(fileStream);
        fileStream.Close();
        return data;
    }
}
