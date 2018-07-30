/*
 * This script is attached to the End gameObject
 * to allow the player to get to the next scene.
 */

using UnityEngine.SceneManagement;
using UnityEngine;

public class EndScene : MonoBehaviour {

    public int scene;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            SceneManager.LoadScene(scene);
        }
    }
}
