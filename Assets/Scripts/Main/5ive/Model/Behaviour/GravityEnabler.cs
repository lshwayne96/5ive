using UnityEngine;

namespace Main._5ive.Model.Behaviour {

	public class GravityEnabler : MonoBehaviour {

		private static bool isEnabled;

		private Rigidbody2D rb;

		void Start() {
			isEnabled = false;
			rb = GetComponent<Rigidbody2D>();
			if (rb != null) {
				rb.gravityScale = 0;
			}
		}

		void Update() {
			if (isEnabled && rb != null) {
				rb.gravityScale = 1;
			}
		}

		private void OnTriggerEnter2D(Collider2D collision) {
			isEnabled = true;
		}
	}

}