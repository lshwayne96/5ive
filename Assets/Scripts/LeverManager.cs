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

    // Update is called once per frame
    void Update() {
        if (CanActivate()) {
            Activate();
        }
    }

    private bool CanActivate() {
        for (int i = 0; i < bools.Length; i++) {
            if (levers[i].HasRotated() != bools[i]) {
                return false;
            }
        }
        return true;
    }

    private void Activate() {
        interactable.SetActive(false);
    }
}
