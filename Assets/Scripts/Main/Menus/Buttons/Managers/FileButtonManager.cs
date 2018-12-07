using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * This script populates the Content gameObject in a ScrollView gameObject
 * with buttons that link to saved games.
 * It is called whenever the player selects the Save or Load buttons
 * in the Pause Menu.
 */
public class FileButtonManager : MonoBehaviour {

	// A gameObject pool where buttons are recycled
	protected GameObjectPool gameObjectPool;

	//protected HashSet<GameObject> buttons;
	protected Button[] actionButtons;
	protected GameObject fileButtonGOToActOn;
	protected Dictionary<String, GameObject> fileButtons;
	protected string taggedFileNameToActOn;

	protected static HashSet<String> deletedTaggedFileNames;
	protected static Dictionary<String, DateTime> modifiedTaggedFileNamesAndDateTime;

	private DirectoryInfo saveDirectoryInfo;
	private string saveDirectoryPath;


	// Initialise the buttons associated with any save game files
	public virtual void Initialise() {
		gameObjectPool = GameObject.FindWithTag("GameObjectPool").GetComponent<GameObjectPool>();
		fileButtons = new Dictionary<string, GameObject>();

		deletedTaggedFileNames = new HashSet<String>();
		modifiedTaggedFileNamesAndDateTime = new Dictionary<String, DateTime>();

		saveDirectoryPath = StorageUtil.GetDirectoryPath(FileType.Level);
		saveDirectoryInfo = Directory.CreateDirectory(saveDirectoryPath);

		CreateFileButtons();
	}

	// Add new or remove old buttons immediately whenever they are added or removed
	public virtual void UpdateButtons() {

	}

	// Populate the Content gameObject with buttons associated with saved game files
	protected void CreateFileButtons() {
		// Get the files in the save directory
		List<FileInfo> fileInfos = new List<FileInfo>(saveDirectoryInfo.GetFiles());
		// Sort files from earliest to lastest created 
		fileInfos.Sort((x, y) => DateTime.Compare(x.LastWriteTime, y.LastWriteTime));

		foreach (FileInfo fileInfo in fileInfos) {
			CreateFileButton(fileInfo);
		}
	}

	// Creates a button associated with the fileInfo
	private void CreateFileButton(FileInfo fileInfo) {
		string filePath = fileInfo.FullName;
		string taggedFileName = StorageUtil.PathToFileName(filePath);

		if (!fileButtons.ContainsKey(taggedFileName) && StorageUtil.ContainsTag(taggedFileName)) {
			LevelData sceneData = StorageUtil.Deserialise<LevelData>(filePath);
			SetUpFileButtonInfo(sceneData, fileInfo, taggedFileName);
		}
	}

	/*
     * Retrieves a button from the game object pool and initialises it
     * with information from the levelData and fileInfo
     */
	private void SetUpFileButtonInfo(LevelData levelData, FileInfo fileInfo, String taggedFileName) {
		// Get information to be displayed on the SaveLoadMenuButton button
		int sceneBuildIndex = levelData.GetSceneBuildIndex();
		DateTime dateTime = fileInfo.LastWriteTimeUtc;

		GameObject button = gameObjectPool.GetObject();
		// Add the button to the scroll view content
		button.transform.SetParent(transform);
		// Add the button to the top
		button.transform.SetAsFirstSibling();
		// Cache the buttons for easy deletion
		fileButtons.Add(taggedFileName, button);

		FileButton fileButton = button.GetComponent<FileButton>();
		string fileName = StorageUtil.ExtractTag(StorageUtil.PathToFileName(fileInfo.FullName));
		fileButton.SetUp(fileName, sceneBuildIndex, dateTime);
	}


	// Recycle one of the buttons on the Content gameObject and delete its associated file
	public virtual void DeleteOne() {

	}

	// Recycle all the buttons on the Content gameObject and delete their associated files
	public virtual void DeleteAll() {
		foreach (GameObject button in fileButtons.Values) {
			FileButton fileButton = button.GetComponent<FileButton>();
			fileButton.DeleteFile();
			// Recycle the button to the SimpleObjectPool instance
			gameObjectPool.ReturnObject(button);
		}
	}

	/*
     * Cache the name of the file and the gameObject
     * attached to the FileButton instance to delete
     */
	public virtual void SetFileButtonToActOn(FileButton fileButton) {

	}

	public virtual void Overwrite() {

	}

	public virtual void Load() {

	}

	// Checks to see if there are still saved game files on the local machine
	protected bool HaveFiles() {
		FileInfo[] fileInfos = saveDirectoryInfo.GetFiles();
		foreach (FileInfo fileInfo in fileInfos) {
			string fileName = fileInfo.Name;
			if (StorageUtil.ContainsTag(fileName)) {
				return true;
			}
		}

		return false;
	}

	/*
     * Checks to see if there are still saved game files
     * by checking the local cache taggedFileNames
     */
	protected bool StillHaveFiles() {
		return fileButtons.Count > 0;
	}

	public bool DoesFileExist(string fileName) {
		return fileButtons.ContainsKey(StorageUtil.AddTag(fileName));
	}

	protected void EnableDisableActionButtons(bool value, params Button[] actionButtons) {
		for (int i = 0; i < actionButtons.Length; i++) {
			actionButtons[i].interactable = value;
		}
	}

	protected void OnDisable() {
		EnableDisableActionButtons(false, actionButtons);
	}
}
