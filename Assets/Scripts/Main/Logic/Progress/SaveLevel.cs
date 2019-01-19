using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This script is used to save the level.
/// </summary>
/// <remarks>
/// It is called when the user presses enter in the
/// input field in the menu.
/// This script is used in both the input field and button game objects.
/// </remarks>
public class SaveLevel : MonoBehaviour {

	private InputField inputField;
	private FileButtonManager buttonManager;

	private ComponentManager comManager;

	private RestorableMonoBehaviour[] restorables;

	private void Start() {
		int currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;

		InitButtonManager(currentSceneBuildIndex);

		GameObject comManagerGO = GameObject.FindGameObjectWithTag(Tags.ComponentManager);
		if (comManagerGO != null) {
			comManager = comManagerGO.GetComponent<ComponentManager>();
			restorables = comManager.GetScripts<RestorableMonoBehaviour>();
		}
	}

	private void InitButtonManager(int currentSceneBuildIndex) {
		bool isInMainMenu = currentSceneBuildIndex == (int) LevelNames.MainMenu;
		if (isInMainMenu) {
			buttonManager = transform.parent.GetComponent<FileButtonManager>();
		} else {
			buttonManager = GameObject.FindGameObjectWithTag(Tags.ButtonManager)
								  .GetComponentInChildren<FileButtonManager>();
		}
	}

	/// <summary>
	/// Saves the level.
	/// </summary>
	/// <remarks>
	/// This is a wrapper function over the overloaded core <code>SaveLevel#Save(...)</code>.
	/// If nothing is entered in the input field, the function does nothing.
	/// </remarks>
	public void Save() {
		string fileName = inputField.text;
		bool hasInput = !fileName.Equals(string.Empty);
		if (hasInput) {
			if (buttonManager.DoesFileExist(fileName)) {
				NotificationManager.Send(new FileAlreadyExists());
				inputField.DeactivateInputField();
				ClearInputField();
				return;
			}

			Save(fileName);
			buttonManager.UpdateButtons();
			inputField.DeactivateInputField();
			ClearInputField();
		}
	}

	/// <summary>
	/// This is the core implementation of <code>SaveLevel#Save()</code>.
	/// </summary>
	/// <param name="fileName">File name.</param>
	private void Save(string fileName) {
		Data[] datas = new Data[restorables.Length];
		for (int i = 0; i < restorables.Length; i++) {
			datas[i] = restorables[i].Save();
		}

		string path = StorageUtil.FileNameToPath(fileName, true);
		Scene scene = SceneManager.GetActiveScene();
		LevelData levelData = new LevelData(scene, datas);

		StorageUtil.Serialise(FileType.Level, path, levelData);
		GameDataManager.Save(fileName, scene.buildIndex);
	}

	private void ClearInputField() {
		inputField.text = string.Empty;
	}

	public void Overwrite(string fileName) {
		Save(fileName);
		buttonManager.UpdateButtons();
	}

	private void OnEnable() {
		inputField = GetComponent<InputField>();
		if (inputField != null) {
			inputField.ActivateInputField();
		}
	}
}