using UnityEngine;
using System;

/// <summary>
/// This script represents a level.
/// </summary>
/// <remarks>
/// Internally, a level corresponds to a scene.
/// </remarks>
public class Level : RestorableMonoBehaviour {

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
	/// Each restorable will produce one <see cref="global::Data"/> object,
	/// which itself can be 
	/// </remarks>
	private Data[] restorableData;

	private void Start() {
		collectible = GetComponent<Collectible>();
		restorables = GameObject.FindWithTag(Tags.ComponentManager)
								.GetComponent<ComponentManager>()
								.GetScripts<RestorableMonoBehaviour>();
	}

	public void CollectCollectible() {
		IsCollectibleCollected = true;
		Destroy(collectible.gameObject);
	}

	public override Data Save() {
		return new LevelData(this);
	}

	public override void RestoreWith(global::Data data) {
		LevelData levelData = (LevelData) data;

		HasBeenSaved = levelData.HasBeenSaved;
		IsCollectibleCollected = levelData.IsCollectibleCollected;
		if (IsCollectibleCollected) {
			Destroy(collectible);
		}
		SceneBuildIndex = levelData.SceneBuildIndex;

		restorables = levelData.Restorables;
		restorableData = levelData.RestorableData;
		for (int i = 0; i < restorables.Length; i++) {
			restorables[i].RestoreWith(restorableData[i]);
		}
	}

	[Serializable]
	public class LevelData : Data {

		public bool HasBeenSaved { get; private set; }

		public bool IsCollectibleCollected { get; private set; }

		public int SceneBuildIndex { get; private set; }

		public RestorableMonoBehaviour[] Restorables { get; private set; }

		public Data[] RestorableData { get; private set; }

		public LevelData(Level level) {
			HasBeenSaved = level.HasBeenSaved;
			IsCollectibleCollected = level.IsCollectibleCollected;
			SceneBuildIndex = level.SceneBuildIndex;
			Restorables = level.restorables;
			for (int i = 0; i < Restorables.Length; i++) {
				RestorableData[i] = Restorables[i].Save();
			}
		}

		public override void Restore(RestorableMonoBehaviour restorable) {
			restorable.RestoreWith(this);
		}
	}
}
