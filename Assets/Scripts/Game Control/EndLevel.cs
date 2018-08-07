/*
 * This script is attached to the End gameObject
 * to allow the player to get to the next scene.
 */

using UnityEngine.SceneManagement;
using UnityEngine;

public class EndLevel : MonoBehaviour {

    public int sceneBuildIndex;

    /*
     * Becomes true when one of the player collider enters the trigger
     * When true, it prevents the other collider of the player from triggering
     * the OnTriggerEnter2D method
     */
    private bool hasEnded;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player") && !hasEnded) {
            hasEnded = true;

            GameDataManager.UpdateNumLevelsCompleted();
            GameDataManager.SetLastUnlockedLevel(sceneBuildIndex);
            SceneManager.LoadScene(sceneBuildIndex);
        }
    }
}
