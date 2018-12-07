using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour {
    public virtual float visibleDuration { get; }

    protected Coroutine currentCoroutine;
    protected bool hasVisibleMessage;
}
