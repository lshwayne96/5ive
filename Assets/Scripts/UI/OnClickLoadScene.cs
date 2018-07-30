/*
 * This script looks a specified scene upon
 * interacting with the attached gameObject.
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClickLoadScene : MonoBehaviour {

    public void LoadByIndex(int sceneIndex) {
        SceneManager.LoadScene(sceneIndex);
    }
}
