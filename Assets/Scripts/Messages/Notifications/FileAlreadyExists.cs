/*
 * A notification class that gives the "File Already Exists!" message.
 */

public class FileAlreadyExists : IMessage {
    public string text {
        get { return "File Already Exists!"; }
    }
}
