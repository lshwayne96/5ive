/*
 * This is a utility class.
 * It provides methods which convert between a file path and a file name
 * and adds an identifier to save level files.
 */

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LevelFile {

    // The standard directory path where level files will be saved
    private static string saveDirectoryPath =
        Application.persistentDataPath + "/Saved Levels/";
    private static string fileNameExtension = ".dat";
    // A tag added to saved level files to allow for distinguish them from others
    private static string tag = "UnityLevel";

    // Hide the constructor
    private LevelFile() {

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

    // Add the tag
    public static string AddTag(string fileName) {
        return String.Concat(tag, fileName);
    }

    // Remove the tag
    public static string RemoveTag(string fileName) {
        string potentialTag = fileName.Substring(0, tag.Length);
        if (potentialTag.Equals(tag)) {
            return fileName.Substring(tag.Length);
        } else {
            return System.String.Empty;
        }
    }

    public static string GetSaveDirectoryPath() {
        return saveDirectoryPath;
    }

    public static bool ContainsTag(string fileName) {
        // If the length of the tag is longer than or equal to the file name length
        if (tag.Length >= fileName.Length) {
            return false;
        } else {
            // Check if the file name has the identifier in it's first part
            string potentialTag = fileName.Substring(0, tag.Length);
            return potentialTag.Equals(tag);
        }
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