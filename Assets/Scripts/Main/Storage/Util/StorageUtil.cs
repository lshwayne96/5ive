using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// This is a storage utility class.
/// </summary>
public static class StorageUtil {

	public static readonly string LevelDirectoryPath = Application.persistentDataPath + "/Levels/";
	public static readonly string GameDirectoryPath = Application.persistentDataPath + "/Games/";

	private const string FileExtension = ".dat";

	/// <summary>
	/// A tag added to game files to distinguish them from non-game files.
	/// </summary>
	private const string tag = "Unity";

	/// <summary>
	/// Converts the file name to its corresponding path.
	/// </summary>
	/// <returns>The path.</returns>
	/// <param name="fileName">Name.</param>
	/// <param name="addition">If addition is TagAddition.Enable, a tag will be added.
	/// If addition is TagAddition.Disable, no tag will be added.</param>
	public static string FileNameToPath(string fileName, TagAddition addition) {
		if (addition == TagAddition.Enable) {
			return LevelDirectoryPath + AddTag(fileName) + FileExtension;
		}
		return LevelDirectoryPath + fileName + FileExtension;
	}

	/// <summary>
	/// Converts the path to its corresponding file name.
	/// </summary>
	/// <returns>The file name.</returns>
	/// <param name="path">Path.</param>
	public static string PathToFileName(string path) {
		string pathWithoutExtension = TrimExtension(path);
		string name = TrimDirectory(pathWithoutExtension);
		return name;
	}

	private static string TrimExtension(string path) {
		return path.Substring(0, path.Length - FileExtension.Length);
	}

	private static string TrimDirectory(string path) {
		return path.Substring(LevelDirectoryPath.Length, path.Length - LevelDirectoryPath.Length);
	}

	/// <summary>
	/// Adds a tag to the file name.
	/// </summary>
	/// <returns>The tagged file name.</returns>
	/// <param name="fileName">File name.</param>
	public static string AddTag(string fileName) {
		return string.Concat(tag, fileName);
	}

	/// <summary>
	/// Extracts the tag from the file name and returns it.
	/// </summary>
	/// <remarks>
	/// The file name is first checked if
	/// it contains the tag in the first place.
	/// </remarks>
	/// <returns>The tag.</returns>
	/// <param name="fileName">File name.</param>
	public static string ExtractTag(string fileName) {
		if (!ContainsTag(fileName)) {
			return string.Empty;
		}
		return tag;
	}

	/// <summary>
	/// Checks if the file name contains a tag.
	/// </summary>
	/// <returns>
	/// <c>true</c>, if a tag is present,
	/// <c>false</c> otherwise.</returns>
	/// <param name="fileName">File name.</param>
	public static bool ContainsTag(string fileName) {
		string potentialTag = fileName.Substring(0, tag.Length);
		return potentialTag.Equals(tag);
	}

	/// <summary>
	/// Gets the directory path for the specified file type.
	/// </summary>
	/// <returns>The directory path.</returns>
	/// <param name="type">Type.</param>
	public static string GetDirectoryPath(FileType type) {
		switch (type) {
		case FileType.Game:
			return GameDirectoryPath;
		case FileType.Level:
			return GameDirectoryPath;
		default:
			Debug.Assert(false, "Invalid FileType");
			return null;
		}
	}

	/// <summary>
	/// Serialises the data and stores it into the file at the specified path.
	/// </summary>
	/// <param name="type">Type.</param>
	/// <param name="path">Path.</param>
	/// <param name="data">Data.</param>
	public static void Serialise(FileType type, string path, object data) {
		string directoryPath = GetDirectoryPath(type);
		Directory.CreateDirectory(directoryPath);

		FileStream fileStream = File.Create(path);
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		binaryFormatter.Serialize(fileStream, data);
		fileStream.Close();
	}

	/// <summary>
	/// Deserialises the data stored in the file at the specified path.
	/// </summary>
	/// <returns>The deserialised data.</returns>
	/// <param name="path">Path.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public static T Deserialise<T>(string path) {
		FileStream fileStream = File.Open(path, FileMode.Open);
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		T data = (T) binaryFormatter.Deserialize(fileStream);
		fileStream.Close();
		return data;
	}
}

public enum TagAddition {
	Enable, Disable
}