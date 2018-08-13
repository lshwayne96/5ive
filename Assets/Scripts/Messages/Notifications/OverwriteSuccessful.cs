/*
 * A notification class that gives the "Overwrite Successful!" message.
 */

public class OverwriteSuccessful : IMessage {
    public string text {
        get { return "Overwrite Successful!"; }
    }
}
