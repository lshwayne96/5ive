﻿using UnityEngine;

namespace Main._5ive.Model.Story.Clues {

    public class AngerGameClue : MonoBehaviour {

        private SpriteRenderer spriteRenderer;

        private void Start() {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.CompareTag("Player")) {
                spriteRenderer.enabled = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision) {
            if (collision.CompareTag("Player")) {
                spriteRenderer.enabled = false;
            }
        }
    }

}
