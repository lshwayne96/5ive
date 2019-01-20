using UnityEngine;

public class MessageManager : MonoBehaviour {
	public virtual float VisibleDuration { get; }

	protected Coroutine currentCoroutine;

	protected bool hasVisibleMessage;
}
