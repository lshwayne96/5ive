using UnityEngine;

public class ModelManager : MonoBehaviour, IModel {

	private RestorableMonoBehaviour[] restorables;

	private void Start() {
		restorables = GetComponentsInChildren<RestorableMonoBehaviour>();
	}

	public RestorableMonoBehaviour[] GetRestorableMonoBehaviours() {
		return restorables;
	}

	public void RestoreRestorableMonoBehaviours() {
		throw new System.NotImplementedException();
	}
}
