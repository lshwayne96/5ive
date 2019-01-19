﻿using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script is attached to the End game object
/// to allow the plater to get to the next scene.
/// </summary>
public class EndLevel : MonoBehaviour {

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
		GameDataManager.EndLevel(sceneBuildIndex);
		SceneManager.LoadScene(sceneBuildIndex);
	}
}