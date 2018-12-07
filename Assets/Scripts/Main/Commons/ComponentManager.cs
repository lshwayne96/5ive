using UnityEngine;

/// <summary>
/// This script is intended to be attached to a parent game object
/// that is high up in the hierarchy so that it can easily get scripts
/// from its children game objects.
/// </summary>
/// <remarks>
/// The aim is to reduce code complexity.
/// </remarks>
public class ComponentManager : MonoBehaviour {

	public T GetScript<T>() {
		return GetComponentInChildren<T>();
	}

	public T[] GetScripts<T>() {
		return GetComponentsInChildren<T>();
	}
}
