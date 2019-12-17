using Main._5ive.Commons;
using UnityEngine;

namespace Main._5ive.Model.Trapdoor {

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