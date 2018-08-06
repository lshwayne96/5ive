/*
 * This script is intended to be attached to a parent game object
 * that is high up in the hierarchy so that it can easily get scripts
 * from its children game objects.
 * The aim is to reduce code complexity, and possibly reduce
 * the time needed to get to certain scripts.
 */

using UnityEngine;

public class ComponentManager : MonoBehaviour {

    public T GetScript<T>() {
        return GetComponentInChildren<T>();
    }

    public T[] GetScripts<T>() {
        return GetComponentsInChildren<T>();
    }
}
