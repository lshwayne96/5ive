using UnityEngine;

public class MakeTheJump : StoryLine {
    public override string Text {
        get { return "Make the jump"; }
    }

    protected override void Preprocess(Collider2D collision, int Count) {
        if (Count == 10) {
            toSend = true;
        }
    }
}
