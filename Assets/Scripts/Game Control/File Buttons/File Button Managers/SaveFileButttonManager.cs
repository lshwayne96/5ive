using UnityEngine;
using UnityEngine.UI;

public class SaveFileButttonManager : FileButtonManager {

    private Button overwriteButton;
    private string saveTaggedFileNameToOverwrite;

    public override void Initialise() {
        base.Initialise();

        overwriteButton = GameObject.FindGameObjectWithTag("OverwriteButton").GetComponent<Button>();
        actionButtons = new Button[] { overwriteButton };
    }

    public override void UpdateButtons() {
        if (HaveFiles()) {
            CreateFileButtons();
        } else {
            DeleteAll();
            ClearCache();
        }
    }

    public override void Overwrite() {
        FileButton fileButton = fileButtonToActOn.GetComponent<FileButton>();
        fileButton.OverwriteFile();
        overwriteButton.interactable = false;
    }

    public override void SetFileButtonToActOn(FileButton fileButton) {
        saveTaggedFileNameToOverwrite = LevelFile.AddTag(fileButton.nameLabel.text);
        fileButtonToActOn = fileButton.gameObject;
        // Allow the player to click the overwrite button
        overwriteButton.interactable = true;
    }
}
