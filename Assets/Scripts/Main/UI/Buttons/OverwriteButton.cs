using UnityEngine;

/// <summary>
/// This script represents level file overwriting functionality via a button.
/// </summary>
/// <remarks>
/// This script is meant to be attached to the overwrite button in the menu.
/// </remarks>
public class OverwriteButton : MonoBehaviour {

	private SaveMenu manager;

	void Start() {
		manager = transform.parent.GetComponentInChildren<SaveMenu>();
	}

	public void Overwrite() {
		manager.OverwriteTargetButton();
	}
}
