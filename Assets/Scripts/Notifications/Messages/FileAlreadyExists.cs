/*
 * A subclass of Notification that gives the "File Already Exists!" message.
 */

public class FileAlreadyExists : Notification {
    public override string GetMessage() {
        return "File Already Exists!";
    }
}
