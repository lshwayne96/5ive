/*
 * This class represents the data of a level and is used to restore
 * a level to its saved state.
 */

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

	public int sceneBuildIndex { get; }

	private readonly Data[] datas;

	/// <summary>
	/// Initializes a new instance of the <see cref="T:LevelData"/> class.
	/// </summary>
	/// <param name="scene">Scene.</param>
	/// <param name="datas">Datas.</param>
	public LevelData(Scene scene, params Data[] datas) {
		sceneBuildIndex = scene.buildIndex;
		this.datas = datas;
	}

	public void Restore() {
		GameObject componentManagerGO = GameObject.FindGameObjectWithTag("ComponentManager");
		if (componentManagerGO != null) {
			ComponentManager componentManager = componentManagerGO.GetComponent<ComponentManager>();

			IRestorable[] restorables = componentManager.GetScripts<IRestorable>();
			for (int i = 0; i < restorables.Length; i++) {
				datas[i].Restore(restorables[i]);
			}
		}
	}
}