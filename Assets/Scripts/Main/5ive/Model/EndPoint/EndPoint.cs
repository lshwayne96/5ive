using Main._5ive.Commons;
using Main._5ive.Messaging;
using Main._5ive.Messaging.Events;
using UnityEngine;

namespace Main._5ive.Model.EndPoint {

	/// <summary>
	/// This script is attached to the end point game object
	/// to allow the plater to get to the next level.
	/// </summary>
	public class EndPoint : MonoBehaviour {
		private EventsCentre eventsCentre;

		private SpriteRenderer spriteRenderer;

		private void Start() {
			eventsCentre = EventsCentre.GetInstance();
			spriteRenderer = GetComponent<SpriteRenderer>();
		}

		private void OnTriggerEnter2D(Collider2D collision) {
			if (collision.gameObject.CompareTag(Tags.Player)) {
				End();
			}
		}

		private void End() {
			eventsCentre.Publish(new LevelNextEvent());
		}
	}

}
