/*
 * This script is attached to the End gameObject
 * to allow the player to get to the next scene.
 */

using UnityEngine.SceneManagement;
using UnityEngine;

public class EndLevel : MonoBehaviour {

    public int sceneBuildIndex;

    private ActionableEnd actionableEnd;
    private bool hasPlayerCollided;

    private void Start() {
        actionableEnd = GetComponent<ActionableEnd>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (actionableEnd && actionableEnd.IsAbleToEnd) {
            if (collision.gameObject.CompareTag("Player") && !hasPlayerCollided) {
                End();
            }

        } else {
            End();
        }
    }

    private void End() {
        hasPlayerCollided = true;
        GameDataManager.EndLevel(sceneBuildIndex);
        SceneManager.LoadScene(sceneBuildIndex);
    }
}
