using System;
using System.Collections.Generic;
using Main._5ive.Model;
using UnityEngine;

namespace Main._5ive.UI.Buttons.GameButtons {

	public class GameButtonCollection : MonoBehaviour {
		/// <summary>
		/// The button prefab.
		/// </summary>
		[SerializeField]
		private GameObject prefab;

		private List<GameButton> gameButtons;

		// Sort the pairs, with the pairs that have the oldest date time placed at the front
		private readonly Comparison<GameButton> comparison = (b1, b2) =>
			DateTime.Compare(b1.GetDateTime(), b2.GetDateTime());

		public void Start() {
			gameButtons = new List<GameButton>();
		}

		private void RestoreWith(IReadOnlyList<State> states) {
			foreach (State state in states) {
				CreateButton(state);
			}
		}

		private void CreateButton(State state) {
			GameObject buttonGameObject = Instantiate(prefab);
			GameButton button = buttonGameObject.GetComponent<GameButton>();
			gameButtons.Add(button);
			button.RestoreWith(state);
		}

		public void Add() {
		}

		/// <summary>
		/// Removes all the buttons and their corresponding level files.
		/// </summary>
		public void RemoveAll() {
			foreach (GameButton gameButton in gameButtons) {
				Remove(gameButton);
			}
		}

		public void Remove(GameButton gameButton) {
			gameButton.DeleteLinkedFile();
			gameButtons.Remove(gameButton);
			Destroy(gameButton.gameObject);
		}

		public bool Contains(string name) {
			foreach (GameButton gameButton in gameButtons) {
				if (gameButton.GetName().Equals(name)) {
					return true;
				}
			}

			return false;
		}

		public bool IsEmpty() {
			return gameButtons.Count == 0;
		}

		public List<GameButton> GetAll() {
			return gameButtons;
		}

		public GameButton GetFirst() {
			return gameButtons[0];
		}

		public void Sort() {
			gameButtons.Sort(comparison);
		}
	}

}
