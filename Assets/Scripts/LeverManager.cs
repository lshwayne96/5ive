using UnityEngine;

public class LeverManager : MonoBehaviour {

    public GameObject[] leverGameObjects;
    public GameObject interactable;
    public bool[] bools;

    private Lever[] levers;
    private bool activate;
    private int numLevers;

    private void Start() {
        numLevers = leverGameObjects.Length;
        levers = new Lever[numLevers];
        for (int i = 0; i < numLevers; i++) {
            levers[i] = leverGameObjects[i].GetComponent<Lever>();
        }
    }

    void Update() {
        if (CanActivate()) {
            interactable.SetActive(false);
        } else {
            interactable.SetActive(true);
        }
    }

    private bool CanActivate() {
        for (int i = 0; i < bools.Length; i++) {
            if (levers[i].HasSwitchedRotation() != bools[i]) {
                return false;
            }
        }
        return true;
    }
}
