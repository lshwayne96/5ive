using System.Collections;
using Main._5ive.Commons;
using UnityEngine;

namespace Main._5ive.Model.Collectible {

	/// <summary>
	/// Represents a collectible and its interactions.
	/// </summary>
	/// <remarks>
	/// When a collectible is collected, it will fade until it
	/// is almost invisible before being destroyed.
	/// </remarks>
	public class Collectible : MonoBehaviour {

		private SpriteRenderer[] spriteRenderers;

		private Level level;

		private void Start() {
			spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
			level = GetComponent<Level>();
		}

		private void OnTriggerEnter2D(Collider2D collision) {
			if (collision.gameObject.CompareTag(Tags.Player)) {
				StartCoroutine(Collect());
			}
		}

		private IEnumerator Collect() {
			for (float f = 1f; f >= 0; f -= 0.1f) {
				foreach (SpriteRenderer spriteRenderer in spriteRenderers) {
					Color c = spriteRenderer.material.color;
					c.a = f;
					spriteRenderer.material.color = c;
				}

				yield return null;
			}

			level.CollectCollectible();
		}
	}

}
