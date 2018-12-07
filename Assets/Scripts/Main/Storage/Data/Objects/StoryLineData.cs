using System;

[Serializable]
public class StoryLineData : Data {

	public bool HasBeenSent { get; private set; }
	public int Count { get; private set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="T:StoryLineData"/> class.
	/// </summary>
	/// <param name="storyLine">Story line.</param>
	public StoryLineData(StoryLine storyLine) {
		HasBeenSent = storyLine.HasBeenSent;
		Count = storyLine.Count;
	}

	public override void Restore(IRestorable restorable) {
		restorable.RestoreWith(this);
	}
}
