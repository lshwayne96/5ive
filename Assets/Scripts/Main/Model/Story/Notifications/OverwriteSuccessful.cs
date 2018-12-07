/*
 * A notification class that gives the "Overwrite Successful!" message.
 */

public class OverwriteSuccessful : IMessage {
    public string Text {
        get { return "Overwrite Successful!"; }
    }
}
