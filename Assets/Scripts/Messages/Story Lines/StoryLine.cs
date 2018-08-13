using UnityEngine;

public class StoryLine : MonoBehaviour, IMessage {
    public virtual string text { get; }

    private bool hasBeenSent;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player") && !hasBeenSent) {
            MessageManager.Send(this);
            hasBeenSent = true;
        }
    }
}
