using UnityEngine;

public class Temp : MonoBehaviour {

    public Lever[] Return() {
        return GetComponentsInChildren<Lever>();
    }
}
