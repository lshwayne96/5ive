/*
 * This script is attached to the load button in the load menu
 * to give them load functionality.
 * It calls Load() from the FileButtonManager instance
 * attached to the menu's scroll view content to load the level
 * deserialised in a file.
 */

using UnityEngine;

public class LoadFileButton : MonoBehaviour {

    private FileButtonManager fileButtonManager;

    void Start() {
        fileButtonManager = transform.parent.GetComponentInChildren<FileButtonManager>();
    }

    public void Load() {
        fileButtonManager.Load();
    }
}
