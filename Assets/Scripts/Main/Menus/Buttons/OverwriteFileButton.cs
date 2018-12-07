/*
 * This script is attached to the update button in the save menu
 * to give them overwrite functionality.
 * It calls Overwrite() from the FileButtonManager instance
 * attached to the menu's scroll view content to override the contents
 * of a file and update the FileButton representing that file.
 */

using UnityEngine;

public class OverwriteFileButton : MonoBehaviour {

    private FileButtonManager fileButtonManager;

    void Start() {
        fileButtonManager = transform.parent.GetComponentInChildren<FileButtonManager>();
    }

    public void Overwrite() {
        fileButtonManager.Overwrite();
    }
}
