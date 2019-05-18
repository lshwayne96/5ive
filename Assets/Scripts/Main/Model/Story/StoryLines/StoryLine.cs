using Main.Model.Story.Messages;
using System;
using Main.Commons;
using UnityEngine;

namespace Main.Model.Story.StoryLines {

	public class StoryLine : RestorableMonoBehaviour, IMessage {
		public virtual string Text { get; }

		public bool HasBeenSent { get; set; }

		public int Count { get; set; }

		protected bool toSend;

		private void OnTriggerEnter2D(Collider2D collision) {
			Count++;
			Preprocess(collision, Count);
			if (collision.CompareTag(Tags.Player) && toSend && !HasBeenSent) {
				StoryLineManager.Send(this);
				HasBeenSent = true;
			}
		}

		// Subclasses can use this method to customise the way the story line message is sent
		protected virtual void Preprocess(Collider2D collision, int Count) {
			toSend = true;
		}

		public override Data Save() {
			return new StoryLineData(this);
		}

		public override void RestoreWith(Data data) {
			StoryLineData storyLineData = (StoryLineData) data;
			HasBeenSent = storyLineData.hasBeenSent;
			Count = storyLineData.count;
		}

		[Serializable]
		public class StoryLineData : Data {

			public bool hasBeenSent;

			public int count;

			/// <summary>
			/// Initializes a new instance of the <see cref="T:StoryLineData"/> class.
			/// </summary>
			/// <param name="storyLine">Story line.</param>
			public StoryLineData(StoryLine storyLine) {
				hasBeenSent = storyLine.HasBeenSent;
				count = storyLine.Count;
			}
		}
	}

}