using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script pauses the game by bringing up the game menu
/// and makes all objects in the game freeze.
/// </summary>
/// <remarks>
/// /// To prevent further input from the user,
/// the player movement script, Platformer2DUserControl, is disabled.
/// </remarks>
public class PauseBehaviour {

	private bool isActive;

	private GameObject menu;

	private List<IPausable> pausables;

	public PauseBehaviour() {
		menu = GameObject.FindWithTag(Tags.Menu);
		menu.SetActive(false);

		pausables = new List<IPausable>();
		pausables.Add(GameObject.FindWithTag(Tags.Player).GetComponent<Player>());
		pausables.Add(GameObject.FindWithTag(Tags.Ball).GetComponent<Ball>());
	}

	public void Run() {
		if (isActive) {
			Unpause();
		} else {
			Pause();
		}
	}

	/// <summary>
	/// Pauses the game.
	/// </summary>
	/// <remarks>
	/// Time is not affected.
	/// </remarks>
	public void Pause() {
		menu.SetActive(true);
		pausables.ForEach(p => p.Pause());
		isActive = true;
	}

	/// <summary>
	/// Unpauses the game.
	/// </summary>
	/// <remarks>
	/// Time is not affected.
	/// </remarks>
	public void Unpause() {
		menu.SetActive(false);
		pausables.ForEach(p => p.Unpause());
		isActive = false;
	}
}
