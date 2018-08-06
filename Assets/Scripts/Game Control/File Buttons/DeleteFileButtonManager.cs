/*
 * This script is attached to the delete buttons
 * to give them delete or delete all functionality.
 */

using UnityEngine;

public class DeleteFileButtonManager : MonoBehaviour {

    private FileButtonManager fileButtonManager;

    void Start() {
        fileButtonManager = transform.parent.GetComponentInChildren<FileButtonManager>();
    }

    public void DeleteAllGameFileButtons() {
        fileButtonManager.DeleteAll();
    }

    public void DeleteOneGameFileButton() {
        fileButtonManager.DeleteOne();
    }

}
