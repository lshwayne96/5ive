using System;
using UnityEngine;

[Serializable]
public class InteractablesData {
    [NonSerialized]
    private GameObject[] interactableGameObjects;
    [NonSerialized]
    private IInteractable[] interactables;
    private int numInteractables;
    private bool[] hasToggled;

    private void Initialise() {
        interactableGameObjects = GameObject.FindGameObjectsWithTag("Interactable");
        numInteractables = interactableGameObjects.Length;

        interactables = new IInteractable[numInteractables];
        for (int i = 0; i < numInteractables; i++) {
            interactables[i] = interactableGameObjects[i].GetComponent<IInteractable>();
        }
    }

    /*
     * Cache the state of interactables in the scene
     * For e.g. whether the lever is rotated
     * and the corresponding ladder is made active
     */
    public void ScreenShot() {
        Initialise();
        hasToggled = new bool[numInteractables];
        for (int i = 0; i < numInteractables; i++) {
            if (interactables[i].HasToggled()) {
                hasToggled[i] = true;
            } else {
                hasToggled[i] = false;
            }
        }
    }

    public void Restore() {
        Initialise();
        for (int i = 0; i < numInteractables; i++) {
            if (hasToggled[i] == true) {
                interactables[i].Toggle();
            }
        }
    }
}
