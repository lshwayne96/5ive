using UnityEngine;

public class StoryLine : MonoBehaviour, IMessage {
    public virtual string text { get; }
    protected bool toSend;
    protected int count;

    private void OnTriggerEnter2D(Collider2D collision) {
        count++;
        Preprocess(collision, count);
        if (collision.CompareTag("Player") && toSend) {
            StoryLineManager.Send(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            count = 0;
        }
    }

    // Subclasses can use this method to customise the way the story line message is sent
    protected virtual void Preprocess(Collider2D collision, int count) {
        toSend = true;
    }
}
