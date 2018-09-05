using UnityEngine;

public class StoryLine : MonoBehaviour, IMessage, ICacheable<StoryLineData> {
    public virtual string Text { get; }
    public bool HasBeenSent { get; set; }
    public int Count { get; set; }
    protected bool toSend;

    private void OnTriggerEnter2D(Collider2D collision) {
        Count++;
        Preprocess(collision, Count);
        if (collision.CompareTag("Player") && toSend && !HasBeenSent) {
            StoryLineManager.Send(this);
            HasBeenSent = true;
            Debug.Log("Has been sent");
        }
    }

    // Subclasses can use this method to customise the way the story line message is sent
    protected virtual void Preprocess(Collider2D collision, int Count) {
        toSend = true;
    }

    public StoryLineData CacheData() {
        Debug.Log("Hello" + HasBeenSent);
        return new StoryLineData(this);
    }

    public void Restore(StoryLineData storyLineData) {
        HasBeenSent = storyLineData.HasBeenSent;
        Debug.Log(HasBeenSent);
        Count = storyLineData.Count;
    }
}
