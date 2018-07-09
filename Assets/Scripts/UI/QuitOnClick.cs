/*
 * This script quits the game upon
 * interacting with the attached gameObject.
 */

using UnityEngine;

public class QuitOnClick : MonoBehaviour {

    public void Quit() {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }
}
