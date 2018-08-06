/*
 * A subclass of Notification that gives the "Overwrite Successful!" message.
 */

public class OverwriteSuccessful : Notification {
    public override string GetMessage() {
        return "Overwrite Successful!";
    }
}
