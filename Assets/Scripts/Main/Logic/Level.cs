using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script represents a level.
/// </summary>
/// <remarks>
/// Internally, a level corresponds to a scene.
/// </remarks>
public class Level : MonoBehaviour {

	/// <summary>
	/// Has the collectible in this level been collected before.
	/// </summary>
	/// <remarks>
	/// If it has been collected before, it should not be present
	/// when a saved level is loaded.
	/// </remarks>
	/// <value><c>true</c> if the collectible has been collected; otherwise, <c>false</c>.</value>
	public bool IsCollectibleCollected { get; private set; }

	/// <summary>
	/// Has the level been saved before.
	/// </summary>
	/// <remarks>
	/// If the level has been saved before, the player will start at the last
	/// saved point when the level is loaded. Otherwise, the player will start at
	/// the beginning.
	/// </remarks>
	/// <value><c>true</c> if has been saved; otherwise, <c>false</c>.</value>
	public bool HasBeenSaved { get; private set; }

	/// <summary>
	/// The build index of the scene corresponding to the level.
	/// </summary>
	public int SceneBuildIndex { get; private set; }

	public string FileName { get; private set; }

	private Collectible collectible;

	/// <summary>
	/// Represents all the objects in the level that extend <see cref="RestorableMonoBehaviour"/>.
	/// </summary>
	/// <remarks>
	/// These objects are serialised when the user saves the level
	/// and deserialised when the level is loaded.
	/// </remarks>
	private RestorableMonoBehaviour[] restorables;

	/// <summary>
	/// Represents the data corresponding to the <see cref="RestorableMonoBehaviour#restorables"/>.
	/// </summary>
	/// <remarks>
	/// Each restorable will produce one <see cref="Data"/> object,
	/// which itself can be 
	/// </remarks>
	private Data[] restorableData;

	private void Start() {
		SceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
		if (!LevelUtil.IsMainMenu(SceneBuildIndex)) {
			collectible = GetComponent<Collectible>();
			restorables = Game.instance.GetComponentsInChildren<RestorableMonoBehaviour>();
		}
	}

	public void CollectCollectible() {
		IsCollectibleCollected = true;
		Destroy(collectible.gameObject);
	}

	public Data Save() {
		return new LevelData(this);
	}

	public void RestoreWith(Data data) {
		LevelData levelData = (LevelData) data;

		HasBeenSaved = levelData.hasBeenSaved;
		IsCollectibleCollected = levelData.isCollectibleCollected;
		if (IsCollectibleCollected) {
			Destroy(collectible);
		}
		SceneBuildIndex = levelData.sceneBuildIndex;
		FileName = levelData.fileName;

		restorables = levelData.restorables;
		restorableData = levelData.restorablesData;
		for (int i = 0; i < restorables.Length; i++) {
			restorables[i].RestoreWith(restorableData[i]);
		}
	}

	[Serializable]
	public class LevelData : Data {

		public bool hasBeenSaved;

		public bool isCollectibleCollected;

		public int sceneBuildIndex;

		public string fileName;

		[NonSerialized]
		public RestorableMonoBehaviour[] restorables;

		public Data[] restorablesData;

		public LevelData(Level level) {
			hasBeenSaved = level.HasBeenSaved;
			isCollectibleCollected = level.IsCollectibleCollected;
			sceneBuildIndex = level.SceneBuildIndex;
			fileName = level.FileName;

			restorables = level.restorables;
			restorablesData = new Data[restorables.Length];

			for (int i = 0; i < restorablesData.Length; i++) {
				restorablesData[i] = restorables[i].Save();
			}
		}
	}
}
