/*
 * This is a utility class.
 * It provides methods which convert between a file path and a file name
 * and adds an identifier to save game files.
 */
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameFile {
    // The standard directory path where game files will be saved
    private static string saveDirectoryPath =
        Application.persistentDataPath + "/Saved Games/";
    private static string fileNameExtension = ".dat";
    // An identifier added to saved game files to allow for distinguish them from others
    private static string fileNameIdentifier = "UNITY";

    // Hide the constructor
    private GameFile() {
    }

    // Get the full path of the file
    public static string ConvertToPath(string fileName) {
        return saveDirectoryPath + fileName + fileNameExtension;
    }

    // Get the name of the file from its path
    public static string ConvertToName(string filePath) {
        // Trim the file path of it's extension
        filePath = filePath.Substring(0, filePath.Length - 4);
        // Trim the file path of it's directory path
        return filePath.Substring(saveDirectoryPath.Length,
                                  filePath.Length - saveDirectoryPath.Length);
    }

    // Add the identifier
    public static string AddIdentifier(string fileName) {
        return String.Concat(fileNameIdentifier, fileName);
    }

    // Remove the identifier
    public static string RemoveIdentifier(string fileName) {
        return fileName.Substring(fileNameIdentifier.Length);
    }

    public static string GetSaveDirectoryPath() {
        return saveDirectoryPath;
    }

    public static bool IdentifierExists(string fileName) {
        // If the length of the identifier is longer than or equal to the file name length
        if (fileNameIdentifier.Length >= fileName.Length) {
            return false;
        } else {
            // Check if the file name has the identifier in it's first part
            string potentialIdentifier = fileName.Substring(0, fileNameIdentifier.Length);
            return potentialIdentifier.Equals(fileNameIdentifier);
        }
    }

    public static LevelData Deserialise(string filePath) {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = File.Open(filePath, FileMode.Open);
        LevelData levelData = (LevelData)binaryFormatter.Deserialize(fileStream);
        fileStream.Close();
        return levelData;
    }
}
