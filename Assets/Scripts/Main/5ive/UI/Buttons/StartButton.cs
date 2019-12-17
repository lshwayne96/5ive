using Main._5ive.Messaging;
using Main._5ive.Messaging.Events;
using UnityEngine;

namespace Main._5ive.UI.Buttons {

	public class StartButton : MonoBehaviour {
		private EventsCentre eventsCentre;

		public void Start() {
			eventsCentre = EventsCentre.GetInstance();
		}

		public void StartGame() {
			eventsCentre.Publish(new GameStartEvent());
		}
	}

}