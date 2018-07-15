using UnityEngine.EventSystems;

public class LoadMenuFileButton : FileButton, IPointerClickHandler {

    private bool isDoubleClick;

    public void OnPointerClick(PointerEventData eventData) {
        // If the button is clicked once by the left button on the mouse
        if (eventData.button == PointerEventData.InputButton.Left &&
            eventData.clickCount == 1) {
            LoadMenuFileButtonManager buttonManagerScript =
                transform.parent.GetComponent<LoadMenuFileButtonManager>();
            /*
             * Send the name of the file associated with the button to the ButtonManager instance
             * The ButtonManager instance will then use that information to delete
             * that file when the delete button is clicked on
             */
            buttonManagerScript.SetFileButtonToDelete(this);
        }

        // If the button is double clicked by the left button on the mouse
        if (eventData.button == PointerEventData.InputButton.Left &&
            eventData.clickCount == 2) {
            // Load the save file with fileName
            LoadGame loadGameScript = GetComponent<LoadGame>();
            // Load the game file associated with that button
            loadGameScript.Load(nameLabel.text);
        }
    }
}
