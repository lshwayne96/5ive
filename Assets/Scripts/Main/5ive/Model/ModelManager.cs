using UnityEngine;

namespace Main._5ive.Model {

	public class ModelManager : MonoBehaviour, IModel {

		private PersistentObject[] restorables;

		private void Start() {
			restorables = GetComponentsInChildren<PersistentObject>();
		}

		public PersistentObject[] GetRestorableMonoBehaviours() {
			return restorables;
		}

		public void RestoreRestorableMonoBehaviours() {
			throw new System.NotImplementedException();
		}
	}

}