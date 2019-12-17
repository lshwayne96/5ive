using System;
using Main._5ive.Commons;
using Main._5ive.Model.Story.Messages;
using UnityEngine;

namespace Main._5ive.Model.Story.StoryLines {

	public class StoryLine : PersistentObject, IMessage {
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

		public override State Save() {
			return new StoryLineState(this);
		}

		public override void RestoreWith(State state) {
			StoryLineState storyLineState = (StoryLineState) state;
			HasBeenSent = storyLineState.hasBeenSent;
			Count = storyLineState.count;
		}

		[Serializable]
		public class StoryLineState : State {

			public bool hasBeenSent;

			public int count;

			/// <summary>
			/// Initializes a new instance of the <see cref="T:StoryLineData"/> class.
			/// </summary>
			/// <param name="storyLine">Story line.</param>
			public StoryLineState(StoryLine storyLine) {
				hasBeenSent = storyLine.HasBeenSent;
				count = storyLine.Count;
			}
		}
	}

}