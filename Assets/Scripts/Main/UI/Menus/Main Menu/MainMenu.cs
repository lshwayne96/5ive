using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	public Text NewButtonText { private get; set; }

	/// <summary>
	/// Represents a possible form that the text of <see cref="NewButtonText"/>
	/// can take.
	/// </summary>
	/// <remarks>
	/// Used when the game has never been saved.
	/// </remarks>
	private const string NewButtonNewText = "New";

	/// <summary>
	/// Represents a possible form that the text of <see cref="NewButtonText"/>
	/// can take.
	/// </summary>
	/// <remarks>
	/// Used when the game has been saved before and indicates to the user
	/// that the game will start where the saved point was.
	/// </remarks>
	private const string NewButtonResumeText = "Resume";

	// Use this for initialization
	void Start() {
		NewButtonText = GameObject.FindWithTag(Tags.NewButton).GetComponent<Text>();
	}

	// Update is called once per frame
	void Update() {

	}

	private void InitButtons() {
		if (Game.instance.HasBeenSavedBefore) {
			InitFirstGame();
		} else {
			InitSubsequentGame();
		}
	}

	private void InitFirstGame() {
		NewButtonText.text = NewButtonNewText;
	}

	private void InitSubsequentGame() {
		NewButtonText.text = NewButtonResumeText;
	}
}
