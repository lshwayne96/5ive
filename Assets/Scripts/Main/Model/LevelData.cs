using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class represents the data of a level.
/// </summary>
/// <remarks>
/// It is used to restore the level to its previous state.
/// </remarks>
[Serializable]
public class LevelData {

	public int SceneBuildIndex { get; }

	private readonly Data[] datas;

	/// <summary>
	/// Initialises a new instance of the <see cref="T:LevelData"/> class.
	/// </summary>
	/// <param name="scene">Scene.</param>
	/// <param name="datas">Datas.</param>
	public LevelData(Scene scene, params Data[] datas) {
		SceneBuildIndex = scene.buildIndex;
		this.datas = datas;
	}

	/// <summary>
	/// Restores all game objects.
	/// </summary>
	/// <remarks>
	/// The game objects restored are those which types extend
	/// from <code>RestorableMonobehaviour</code>.
	/// </remarks>
	public void Restore() {
		GameObject comManagerGO = GameObject.FindGameObjectWithTag(Tags.ComponentManager);
		if (comManagerGO != null) {
			ComponentManager comManager = comManagerGO.GetComponent<ComponentManager>();

			RestorableMonoBehaviour[] restorables = comManager.GetScripts<RestorableMonoBehaviour>();
			for (int i = 0; i < restorables.Length; i++) {
				datas[i].Restore(restorables[i]);
			}
		}
	}
}