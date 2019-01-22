using UnityEngine;

/// <summary>
/// This script represents level file deletion functionality via a button.
/// </summary>
/// <remarks>
/// This script is meant to be attached to the delete buttons in the menu.
/// </remarks>
public class DeleteButton : MonoBehaviour {

	private LoadMenu manager;

	private void Start() {
		manager = transform.parent.GetComponentInChildren<LoadMenu>();
	}

	public void DeleteOne() {
		manager.DeleteTargetButton();
	}

	public void DeleteAll() {
		manager.DeleteAllButtons();
	}
}
