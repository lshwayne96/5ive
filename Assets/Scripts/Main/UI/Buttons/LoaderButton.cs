using UnityEngine;

/// <summary>
/// This script represents level loading functionality via a button.
/// </summary>
/// <remarks>
/// This script is meant to be attached to the load button in the menu.
/// </remarks>
public class LoaderButton : MonoBehaviour {

	private LoadMenu manager;

	void Start() {
		manager = transform.parent.GetComponentInChildren<LoadMenu>();
	}

	public void Load() {
		manager.LoadTargetButton();
	}
}
