using Main._5ive.Messaging;
using Main._5ive.Messaging.Events;
using UnityEngine;

namespace Main._5ive.UI.Buttons {

	public class GameRestartButton : MonoBehaviour {
		private EventsCentre eventsCentre;

		public void Start() {
			eventsCentre = EventsCentre.GetInstance();
		}

		public void Restart() {
			eventsCentre.Publish(new GameRestartEvent());
		}
	}

}