using System;
using UnityEngine;

[Serializable]
public class ActiveInactiveData {

    [NonSerialized]
    private GameObject[] gameObjects;
    private int num;

    private bool[] isActive;

    private void Initialise() {
        gameObjects = GameObject.FindGameObjectsWithTag("ActiveInactive");
        num = gameObjects.Length;
        isActive = new bool[num];
    }

    /*
     * Cache the state of interactables in the scene
     * For e.g. whether the lever is rotated
     * and the corresponding ladder is made active
     */
    public void ScreenShot() {
        Initialise();

        for (int i = 0; i < num; i++) {
            if (directInteractables[i].IsInteracting()) {
                isDirectInteracting[i] = true;
            } else {
                isDirectInteracting[i] = false;
            }
        }

        isIndirectInteracting = new bool[numIndirectInteractables];
        for (int i = 0; i < numIndirectInteractables; i++) {
            if (indirectInteractables[i].IsInteracting()) {
                isIndirectInteracting[i] = true;
            } else {
                isIndirectInteracting[i] = false;
            }
        }
    }

    public void Restore() {
        Initialise();

        for (int i = 0; i < numDirectInteractables; i++) {
            if (isDirectInteracting[i] == true) {
                directInteractables[i].Restore();
            }
        }

        for (int i = 0; i < numIndirectInteractables; i++) {
            if (isIndirectInteracting[i] == true) {
                indirectInteractables[i].Restore();
            }
        }
    }
}
