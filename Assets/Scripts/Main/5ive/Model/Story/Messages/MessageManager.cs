using UnityEngine;

namespace Main._5ive.Model.Story.Messages {

	public class MessageManager : MonoBehaviour {
		public virtual float VisibleDuration { get; }

		protected Coroutine currentCoroutine;

		protected bool hasVisibleMessage;
	}

}
