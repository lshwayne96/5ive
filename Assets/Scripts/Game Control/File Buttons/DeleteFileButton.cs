/*
 * This script is attached to the delete buttons in the load menu
 * to give them delete or delete all functionality.
 * It calls DeleteOne() or DeleteAll() from the FileButtonManager instance
 * attached to the menu's scroll view content to remove buttons.
 */

using UnityEngine;

public class DeleteFileButton : MonoBehaviour {

    private FileButtonManager fileButtonManager;

    void Start() {
        fileButtonManager = transform.parent.GetComponentInChildren<FileButtonManager>();
    }

    public void DeleteOne() {
        fileButtonManager.DeleteOne();
    }

    public void DeleteAll() {
        fileButtonManager.DeleteAll();
    }
}
