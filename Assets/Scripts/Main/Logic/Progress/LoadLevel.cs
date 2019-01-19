using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script is used to load a level.
/// </summary>
public class LoadLevel : MonoBehaviour {

	/// <summary>
	/// Deserialises the data stored in <code>fileName</code>
	/// and hands over the deserialised data to the
	/// <code>RestoreLevel</code> singleton.
	/// </summary>
	/// <param name="fileName">File name.</param>
	public void Load(string fileName) {
		string path = StorageUtil.FileNameToPath(fileName, true);
		LevelData levelData = StorageUtil.Deserialise<LevelData>(path);
		SceneManager.LoadScene(levelData.SceneBuildIndex);

		RestoreLevel.instance.Take(levelData);
	}
}