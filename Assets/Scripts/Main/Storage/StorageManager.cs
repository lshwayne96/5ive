using Main.Model;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System;
using Main.Commons.Util;
using Main.Logic;

namespace Main.Storage {

	public class StorageManager : IStorage {

		private static readonly string LevelDirectoryPath = Application.persistentDataPath + "/Levels";

		private static readonly string GameDirectoryPath = Application.persistentDataPath + "/Games";

		private const string FileExtension = ".5ive";

		private const string GameFileName = "/" + "5ive" + FileExtension;

		private static readonly string GameFilePath = GameDirectoryPath + GameFileName;

		/// <summary>
		/// A tag added to game files to distinguish them from non-game files.
		/// </summary>
		private const string Tag = "unity_";

		public void StoreGame(Data data) {
			Serialise(FileType.Game, data, GameFilePath);
		}

		public Data FetchGame() {
			return Deserialise<Data>(GameFilePath);
		}

		public void StoreLevel(Data data, string name) {
			string path = NameToPath(name);
			Serialise(FileType.Level, data, path);
		}

		public Data FetchLevel(string name) {
			string path = NameToPath(name);
			return Deserialise<Data>(path);
		}

		public GameButtonInfo FetchGameButton(string name) {
			Data data = FetchLevel(name);
			Level.LevelData levelData = (Level.LevelData) data;

			string levelName = LevelUtil.GetLevelName(levelData.sceneBuildIndex);
			string path = NameToPath(name);
			DateTime lastWriteTime = File.GetLastWriteTimeUtc(path);
			return new GameButtonInfo(name, levelName, lastWriteTime);
		}

		public GameButtonInfo[] FetchGameButtons() {
			DirectoryInfo directoryInfo = Directory.CreateDirectory(LevelDirectoryPath);
			FileInfo[] fileInfos = GetLevelFileInfos(directoryInfo);
			Data[] levelDatas = GetDatasFrom(fileInfos);
			GameButtonInfo[] gameButtonInfos = BuildGameButtonInfos(fileInfos, levelDatas);
			return gameButtonInfos;
		}

		/// <summary>
		/// Gets the file infos from the directory containing the level files,
		/// removes all non-level files and sorts them based on their creation date (with
		/// those being created the latest in front).
		/// </summary>
		/// <returns>The processed file infos.</returns>
		private FileInfo[] GetLevelFileInfos(DirectoryInfo directoryInfo) {
			List<FileInfo> savedFiles = new List<FileInfo>(directoryInfo.GetFiles());

			// Remove all files without the tag
			Predicate<FileInfo> levelFileCheck = f => !ContainsTag(f.FullName);
			savedFiles.RemoveAll(levelFileCheck);

			// Sort the files, with the files created last at the front
			Comparison<FileInfo> dateTimeComparison = (x, y) => DateTime.Compare(x.LastWriteTime, y.LastWriteTime);
			savedFiles.Sort(dateTimeComparison);

			return savedFiles.ToArray();
		}

		/// <summary>
		/// Gets the datas.
		/// </summary>
		/// <returns>The datas.</returns>
		/// <param name="fileInfos">File infos.</param>
		private Data[] GetDatasFrom(FileInfo[] fileInfos) {
			Data[] levelDatas = new Data[fileInfos.Length];
			for (int i = 0; i < fileInfos.Length; i++) {
				levelDatas[i] = GetDataFrom(fileInfos[i]);
			}

			return levelDatas;
		}

		/// <summary>
		/// Get the level data from the corresponding level file.
		/// </summary>
		/// <param name="fileInfo">File info.</param>
		private Data GetDataFrom(FileInfo fileInfo) {
			string fileName = fileInfo.Name;
			return FetchLevel(fileName);
		}

		private GameButtonInfo[] BuildGameButtonInfos(FileInfo[] fileInfos, Data[] datas) {
			GameButtonInfo[] gameButtonInfos = new GameButtonInfo[fileInfos.Length];
			for (int i = 0; i < fileInfos.Length; i++) {
				string fileName = fileInfos[i].Name;
				Level.LevelData levelData = (Level.LevelData) datas[i];
				string levelName = LevelUtil.GetLevelName(levelData.sceneBuildIndex);
				DateTime lastWriteTime = File.GetLastWriteTimeUtc(fileInfos[i].FullName);

				gameButtonInfos[i] = new GameButtonInfo(fileName, levelName, lastWriteTime);
			}

			return gameButtonInfos;
		}

		public bool HasGame() {
			return File.Exists(GameFileName);
		}

		public bool HasLevels() {
			FileInfo[] fileInfos = Directory.CreateDirectory(LevelDirectoryPath).GetFiles();
			foreach (FileInfo fileInfo in fileInfos) {
				string fileName = fileInfo.Name;
				if (ContainsTag(fileName)) {
					return true;
				}
			}

			return false;
		}

		public void DeleteLevel(string name) {
			string path = NameToPath(name);
			File.Delete(path);
		}

		public void DeleteAllFiles() {
			DeleteGameFile();
			DeleteLevelFiles();
		}

		private void DeleteGameFile() {
			File.Delete(GameFilePath);
		}

		private void DeleteLevelFiles() {
			string[] filePaths = Directory.GetFiles(LevelDirectoryPath);
			foreach (string path in filePaths) {
				File.Delete(path);
			}
		}

		/// <summary>
		/// Converts the file name to its corresponding path.
		/// </summary>
		/// <returns>The path.</returns>
		/// <param name="name">Name.</param>
		private string NameToPath(string name) {
			if (name.Contains(Tag)) {
				return LevelDirectoryPath + "/" + name;
			}

			return LevelDirectoryPath + "/" + string.Concat(Tag, name) + FileExtension;
		}

		/// <summary>
		/// Converts the path to its corresponding file name.
		/// </summary>
		/// <returns>The file name.</returns>
		/// <param name="path">Path.</param>
		private string PathToName(string path) {
			string pathWithoutExtension = TrimExtension(path);
			return TrimDirectory(pathWithoutExtension);
		}

		private string TrimExtension(string path) {
			return path.Substring(0, path.Length - FileExtension.Length);
		}

		private string TrimDirectory(string path) {
			return path.Substring(LevelDirectoryPath.Length, path.Length - LevelDirectoryPath.Length);
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
		public string ExtractTag(string fileName) {
			if (!ContainsTag(fileName)) {
				return string.Empty;
			}

			return Tag;
		}

		/// <summary>
		/// Checks if the file name contains a tag.
		/// </summary>
		/// <returns>
		/// <c>true</c>, if a tag is present,
		/// <c>false</c> otherwise.</returns>
		/// <param name="fileName">File name.</param>
		public bool ContainsTag(string fileName) {
			return fileName.Contains(Tag);
		}

		/// <summary>
		/// Gets the directory path for the specified file type.
		/// </summary>
		/// <returns>The directory path.</returns>
		/// <param name="type">Type.</param>
		public string GetDirectoryPath(FileType type) {
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
		/// <param name="data">Data.</param>
		/// <param name="path">Path.</param>
		public void Serialise(FileType type, object data, string path) {
			string directoryPath = GetDirectoryPath(type);
			Directory.CreateDirectory(directoryPath);

			FileStream fileStream = File.Create(path);
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(fileStream, data);
			fileStream.Close();
		}

		public T Deserialise<T>(string path) {
			FileUtil.CreateFile(path);

			FileStream fileStream = File.Open(path, FileMode.Open);
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			T data = (T) binaryFormatter.Deserialize(fileStream);
			fileStream.Close();
			return data;
		}
	}

	public enum TagAddition {
		Enable,
		Disable
	}

}