using UnityEngine;

/// <summary>
/// This script represents level file deletion functionality via a button.
/// </summary>
/// <remarks>
/// This script is meant to be attached to the delete button in the load menu.
/// </remarks>
public class DeleteAllButton : MonoBehaviour {

	private LoadMenu manager;

	private void Start() {
		manager = transform.parent.GetComponentInChildren<LoadMenu>();
	}

	public void DeleteAll() {
		manager.DeleteAllButtons();
	}
}
