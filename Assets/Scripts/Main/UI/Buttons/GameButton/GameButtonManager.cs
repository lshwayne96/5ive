using System.Collections.Generic;
using UnityEngine;
using System;

public class GameButtonManager : MonoBehaviour {

	public static GameButtonManager instance;

#pragma warning disable CS0649
	/// <summary>
	/// The button prefab.
	/// </summary>
	[SerializeField]
	private GameObject prefab;
#pragma warning restore CS0649

	private List<GameButton> gameButtons;

	private void Awake() {
		instance = this;
	}

	// Sort the pairs, with the pairs that have the oldest date time placed at the front
	private readonly Comparison<GameButton> comparison = (b1, b2) => DateTime.Compare(b1.GetDateTime(), b2.GetDateTime());

	private void CreateButtons() {
		gameButtons = new List<GameButton>();
		GameButtonInfo[] gameButtonInfos = Game.instance.storage.FetchGameButtons();
		for (int i = 0; i < gameButtonInfos.Length; i++) {
			CreateButton(gameButtonInfos[i]);
		}
	}

	private void CreateButton(GameButtonInfo gameButtonInfo) {
		GameObject buttonGameObject = Instantiate(prefab);
		GameButton button = buttonGameObject.GetComponent<GameButton>();
		gameButtons.Add(button);
		button.SetUp(gameButtonInfo);
	}

	public void RemoveGameButton(GameButton gameButton) {
		gameButton.DeleteLinkedFile();
		gameButtons.Remove(gameButton);
		Destroy(gameButton.gameObject);
	}

	/// <summary>
	/// Removes all the buttons and their corresponding level files.
	/// </summary>
	public void RemoveAllGameButtons() {
		foreach (GameButton gameButton in gameButtons) {
			RemoveGameButton(gameButton);
		}
		gameButtons.Clear();
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

	public List<GameButton> GetGameButtons() {
		CreateButtons();
		gameButtons.Sort(comparison);
		return gameButtons;
	}

	public GameButton GetNewlyAddedGameButton() {
		return gameButtons[0];
	}

	public void UpdateGameButtonsOrdering() {
		gameButtons.Sort(comparison);
	}
}
