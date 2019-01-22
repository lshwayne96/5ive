using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script represents a menu. The main function of this script
/// is to populate the menu with buttons.
/// </summary>
/// <remarks>
/// By default, the file name associated with a level file is tagged
/// with a specific preamble. Before the file name can be displayed
/// on the button, the preamble has to be trimmed.
/// </remarks>
public abstract class Menu : MonoBehaviour {

	/// <summary>
	/// The button prefab.
	/// </summary>
	protected GameObject prefab;

	/// <summary>
	/// Represents a mapping of all button names
	/// and their corresponding button scripts
	/// that are active in the menu.
	/// </summary>
	protected Dictionary<string, GameButton> nameButtonMapping;

	/// <summary>
	/// Represents a pair consisting of the button name
	/// and the corresponding button script
	/// that is targeted for deletion.
	/// </summary>
	protected KeyValuePair<string, GameButton> targetButton;

	/// <summary>
	/// An array of all the <code>GameButton</code> scripts that
	/// are active in the menu.
	/// </summary>
	/// <remarks>
	/// The scripts are cached here for better performance instead of
	/// using <code>GetComponent()</code> everytime the scripts
	/// need to be accessed.
	/// </remarks>
	protected Button[] activeButtons;

	/// <summary>
	/// Represents the names of all the buttons that are deleted in the load menu.
	/// </summary>
	/// <remarks></remarks>
	protected static HashSet<string> deletedButtonNames;

	/// <summary>
	/// Represents the names, and date and time of the buttons that are
	/// overwritten in the save menu.
	/// </summary>
	protected static Dictionary<string, DateTime> nameDateTimeMapping;

	private DirectoryInfo dirInfo;
	private string dirPath;

	public virtual void Init() {
		nameButtonMapping = new Dictionary<string, GameButton>();

		deletedButtonNames = new HashSet<string>();
		nameDateTimeMapping = new Dictionary<String, DateTime>();

		dirPath = StorageUtil.GetDirectoryPath(FileType.Level);
		dirInfo = Directory.CreateDirectory(dirPath);

		CreateButtons();
	}

	/// <summary>
	/// Populates the menu with buttons.
	/// </summary>
	/// <remarks>
	/// This method is inefficient in that even if there are existing buttons
	/// in the menu, it will not take them into account when adding new buttons.
	/// What it does is that it retrieves all the current level files,
	/// gets their information and then creates buttons corresponding to them.
	/// </remarks>
	protected void CreateButtons() {
		List<FileInfo> fileInfos = GetSavedFileInfos();
		List<LevelData> levelDatas = GetDatas(fileInfos);

		for (int i = 0; i < levelDatas.Count; i++) {
			CreateButton(levelDatas[i], fileInfos[i], fileInfos[i].FullName);
		}
	}

	/// <summary>
	/// Gets the datas.
	/// </summary>
	/// <returns>The datas.</returns>
	/// <param name="fileInfos">File infos.</param>
	private List<LevelData> GetDatas(List<FileInfo> fileInfos) {
		List<LevelData> levelDatas = new List<LevelData>();
		foreach (FileInfo fileInfo in fileInfos) {
			levelDatas.Add(GetData(fileInfo));
		}
		return levelDatas;
	}

	/// <summary>
	/// Gets the file infos from the directory containing the level files,
	/// removes all non-level files and sorts them based on their creation date (with
	/// those being created the latest in front).
	/// </summary>
	/// <returns>The processed file infos.</returns>
	private List<FileInfo> GetSavedFileInfos() {
		List<FileInfo> savedFiles = new List<FileInfo>(dirInfo.GetFiles());

		// Remove all non-level files
		savedFiles.RemoveAll(f => !StorageUtil.ContainsTag(f.FullName));

		// Sort the files, with the files created last at the front
		savedFiles.Sort((x, y) => DateTime.Compare(x.LastWriteTime, y.LastWriteTime));

		return savedFiles;
	}

	/// <summary>
	/// Get the level data from the corresponding level file.
	/// </summary>
	/// <param name="fileInfo">File info.</param>
	private LevelData GetData(FileInfo fileInfo) {
		string path = fileInfo.FullName;
		string fileName = StorageUtil.PathToFileName(path);

		return StorageUtil.Deserialise<LevelData>(path);
	}

	/// <summary>
	/// Initialises a <code>GameButton</code> instance.
	/// </summary>
	/// <param name="levelData">Level data.</param>
	/// <param name="fileInfo">File information.</param>
	/// <param name="taggedFileName">Tagged file name.</param>
	private void CreateButton(LevelData levelData, FileInfo fileInfo, string taggedFileName) {
		// Get information to be displayed on the SaveLoadMenuButton button
		int sceneBuildIndex = levelData.SceneBuildIndex;
		DateTime dateTime = fileInfo.LastWriteTimeUtc;

		GameButton button = Instantiate(prefab).GetComponent<GameButton>();
		AttachButtonToMenu(button);

		// Cache the buttons for easy deletion
		nameButtonMapping.Add(taggedFileName, button);

		GameButton fileButton = button.GetComponent<GameButton>();
		string fileName = StorageUtil.ExtractTag(StorageUtil.PathToFileName(fileInfo.FullName));
		fileButton.SetUp(fileName, sceneBuildIndex, dateTime);
	}

	/// <summary>
	/// Adds <code>button</code> to the menu and brings it to the top.
	/// </summary>
	/// <param name="button">Button.</param>
	private void AttachButtonToMenu(GameButton button) {
		button.AttachToMenu(transform);
		button.MoveToTopOfMenu();
	}

	/// <summary>
	/// Add or remove buttons whenever their corresponding level files are added or removed.
	/// </summary>
	/// <remarks>
	/// This method is meant to be called whenever the menu is made active.
	/// The underlying reason for this method is that since buttons can be modified in both
	/// the save and load menus, there is a disconnect between them, and hence this
	/// method modifies the menu's buttons to ensure consistency.
	/// </remarks>
	public abstract void UpdateButtons();


	/// <summary>
	/// Sets the <code>button</code> as a target.
	/// </summary>
	/// <remarks>
	/// This method is called when the user single clicks
	/// on a button in the menu.
	/// </remarks>
	/// <param name="button">Button.</param>
	public virtual void SetTargetButton(GameButton button) {
		string buttonName = button.NameLabel.text;
		targetButton = new KeyValuePair<string, GameButton>(buttonName, button);
	}

	/// <summary>
	/// Delete all buttons and their corresponding level files.
	/// </summary>
	public virtual void DeleteAllButtons() {
		foreach (GameButton button in nameButtonMapping.Values) {
			button.DeleteCorrespondingFile();
			Destroy(button);
		}
		nameButtonMapping.Clear();
	}

	// Checks to see if there are still saved game files on the local machine
	protected bool areSavedFilesPresent() {
		FileInfo[] fileInfos = dirInfo.GetFiles();
		foreach (FileInfo fileInfo in fileInfos) {
			string fileName = fileInfo.Name;
			if (StorageUtil.ContainsTag(fileName)) {
				return true;
			}
		}

		return false;
	}

	/// <summary>
	/// Checks if there are still level files.
	/// </summary>
	/// <returns><c>true</c>, if there are still level files, <c>false</c> otherwise.</returns>
	protected bool AreFilesPresent() {
		return nameButtonMapping.Count > 0;
	}

	/// <summary>
	/// Checks if a level file exists.
	/// </summary>
	/// <returns><c>true</c>, if the level file exists, <c>false</c> otherwise.</returns>
	/// <param name="fileName">File name.</param>
	public bool DoesFileExist(string fileName) {
		return nameButtonMapping.ContainsKey(StorageUtil.AddTag(fileName));
	}

	protected void OnDisable() {
		for (int i = 0; i < activeButtons.Length; i++) {
			activeButtons[i].interactable = false;
		}
	}
}
