/*
 * This script encapsulates the behaviour of a collectible.
 * When the user interacts with the collectible,
 * the collectible will fade until it is almost invisible
 * before it is destroyed.
 */

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collectible : MonoBehaviour {

    private SpriteRenderer[] spriteRenderers;

    private void Start() {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            StartCoroutine(Collected());
        }
    }

    // Collect this collectible and destroy it
    private IEnumerator Collected() {
        for (float f = 1f; f >= 0; f -= 0.1f) {
            foreach (SpriteRenderer spriteRenderer in spriteRenderers) {
                Color c = spriteRenderer.material.color;
                c.a = f;
                spriteRenderer.material.color = c;
            }
            yield return null;
        }

        int currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        Game.MarkSceneCollectibleAsCollected(currentSceneBuildIndex);

        Destroy(gameObject);
    }
}
