using System;

[Serializable]
public class StoryLineData {
    public bool HasBeenSent { get; private set; }
    public int Count { get; private set; }

    public StoryLineData(StoryLine storyLine) {
        HasBeenSent = storyLine.HasBeenSent;
        Count = storyLine.Count;
    }

    public void Restore(StoryLine storyLine) {
        storyLine.Restore(this);
    }
}
