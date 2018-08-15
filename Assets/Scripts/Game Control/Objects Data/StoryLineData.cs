using System;

[Serializable]
public class StoryLineData {
    private bool hasBeenSent;
    private int count;

    public StoryLineData(bool hasBeenSent, int count) {
        this.hasBeenSent = hasBeenSent;
        this.count = count;
    }

    public void Restore(StoryLine storyLine) {
        storyLine.Restore(hasBeenSent, count);
    }
}
