using UnityEngine;

public class ComponentManager : MonoBehaviour {

    public T[] GetScripts<T>() {
        return GetComponentsInChildren<T>();
    }
}
