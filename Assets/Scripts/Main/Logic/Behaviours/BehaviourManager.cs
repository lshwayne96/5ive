using UnityEngine;

namespace Main.Logic.Behaviours {

	public class BehaviourManager : MonoBehaviour {

		private PauseBehaviour pauseBehaviour;

		private void Start() {
			pauseBehaviour = new PauseBehaviour();
		}

		private void Update() {
			if (Input.GetKeyDown(KeyCode.Escape)) {
				pauseBehaviour.Run();
			}
		}
	}

}
