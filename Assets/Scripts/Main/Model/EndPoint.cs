using UnityEngine;

/// <summary>
/// This script is attached to the end point game object
/// to allow the plater to get to the next level.
/// </summary>
public class EndPoint : MonoBehaviour {

	public int sceneBuildIndex;

	private SpriteRenderer spriteRenderer;

	private void Start() {
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.CompareTag(Tags.Player)) {
			End();
		}
	}

	private void End() {
		Game.EndLevel(sceneBuildIndex);
	}
}
