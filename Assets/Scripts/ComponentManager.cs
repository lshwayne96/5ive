using UnityEngine;

public class ComponentManager : MonoBehaviour {

    public T GetScript<T>() {
        return GetComponentInChildren<T>();
    }

    public T[] GetScripts<T>() {
        return GetComponentsInChildren<T>();
    }
}
