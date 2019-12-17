using System.Collections.Generic;
using Main._5ive.Commons;
using Main._5ive.Model;
using Main._5ive.Model.Ball;
using Main._5ive.Model.Player;
using UnityEngine;

namespace Main._5ive.Logic.Behaviours {
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
			isActive = false;

			menu = GameObject.FindWithTag(Tags.Menu);
			menu.SetActive(false);

			pausables = new List<IPausable> {
				GameObject.FindWithTag(Tags.Player).GetComponent<Player>(),
				GameObject.FindWithTag(Tags.Ball).GetComponent<Ball>()
			};
		}

		public void Toggle() {
			if (isActive) {
				Resume();
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
		public void Resume() {
			menu.SetActive(false);
			pausables.ForEach(p => p.Resume());
			isActive = false;
		}
	}
}