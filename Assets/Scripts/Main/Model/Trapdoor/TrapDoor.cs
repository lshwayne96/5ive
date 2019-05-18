using Main.Commons;
using UnityEngine;

namespace Main.Model.TrapDoor {

	public class TrapDoor : MonoBehaviour {

		public GameObject trapDoorGO;

		private void OnTriggerEnter2D(Collider2D collision) {
			if (collision.CompareTag(Tags.Player)) {
				trapDoorGO.SetActive(true);
				Destroy(this);
			}
		}
	}

}