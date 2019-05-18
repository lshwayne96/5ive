using System.Collections;
using Main.Model.Story.Messages;
using UnityEngine;
using UnityEngine.UI;

namespace Main.UI.Notification {

	/// <summary>
	/// This script manages the sending of notifications.
	/// </summary>
	public class NotificationManager : MessageManager {
		private static Image messageImage;

		private static Text messageText;

		private static bool hasNewMessage;

		private static float startTime;

		public override float VisibleDuration => 3f;

		private void Start() {
			messageImage = GetComponentInChildren<Image>();

			// Make the message image invisible
			messageImage.enabled = false;

			messageText = GetComponentInChildren<Text>();
		}

		private void Update() {
			if (hasNewMessage) {
				if (hasVisibleMessage) {
					InterruptDisappearance();
				}

				StartTimerToDisappearance();
			}
		}

		private void StartTimerToDisappearance() {
			currentCoroutine = StartCoroutine(Disappear());
			hasNewMessage = false;
		}

		private void InterruptDisappearance() {
			StopCoroutine(currentCoroutine);
			hasNewMessage = false;
		}

		/// <summary>
		/// Fades away the notification after a certain duration.
		/// </summary>
		/// <returns>Control to <code>Update()</code>.</returns>
		protected IEnumerator Disappear() {
			hasVisibleMessage = true;
			float diff = 0;

			while (diff < VisibleDuration) {
				diff = Time.time - startTime;
				yield return null;
			}

			hasVisibleMessage = false;
			currentCoroutine = null;

			// Make the notification box invisible
			messageImage.enabled = false;
			messageText.text = string.Empty;
			hasNewMessage = false;
		}

		public static void Send(IMessage message) {
			// Make the message image visible
			messageImage.enabled = true;
			messageText.text = message.Text;
			hasNewMessage = true;
			startTime = Time.time;
		}
	}

}