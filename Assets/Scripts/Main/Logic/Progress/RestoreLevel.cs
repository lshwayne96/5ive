using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script represents a singleton that will restore the
/// level's progress if any.
/// </summary>
public class RestoreLevel : MonoBehaviour {

	public static RestoreLevel instance;

	public bool HasRestoredLevel { get; private set; }

	private LevelData levelData;

	private bool hasSavedLevel;

	private void Awake() {
		if (instance == null) {
			DontDestroyOnLoad(gameObject);
			instance = this;

		} else if (instance != this) {
			Destroy(gameObject);
		}

		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	/// <summary>
	/// Takes the <code>levelData</code> and caches it.
	/// </summary>
	/// <param name="levelData">Level data.</param>
	public void Take(LevelData levelData) {
		this.levelData = levelData;
		hasSavedLevel = true;
	}

	/// <summary>
	/// Restores the level data.
	/// </summary>
	public void Restore() {
		levelData.Restore();
		HasRestoredLevel = true;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
		if (hasSavedLevel) {
			Restore();
			hasSavedLevel = false;
		}
	}

	private void OnDisable() {
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}
}
