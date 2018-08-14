using UnityEngine;

public class StoryLine : MonoBehaviour, IMessage {
    public virtual string text { get; }
    protected bool toTrigger;
    protected int count;

    private bool hasBeenSent;

    private void OnTriggerEnter2D(Collider2D collision) {
        count++;
        Preprocess(collision, count);
        if (collision.CompareTag("Player") && !hasBeenSent && toTrigger) {
            MessageManager.Send(this);
            hasBeenSent = true;
        }
    }

    // Subclasses can use this method to customise the way the story line message is sent
    protected virtual void Preprocess(Collider2D collision, int count) {
        toTrigger = true;
    }
}
