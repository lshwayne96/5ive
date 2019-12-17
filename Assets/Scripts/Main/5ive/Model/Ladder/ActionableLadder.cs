using UnityEngine;

namespace Main._5ive.Model.Ladder {

    public class ActionableLadder : MonoBehaviour, IActionable {
        private SpriteRenderer[] spriteRenderers;

        private Ladder ladder;

        private void Start() {
            spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
            ladder = GetComponent<Ladder>();
        }

        public void StartAction() {
            for (int i = 0; i < spriteRenderers.Length; i++) {
                spriteRenderers[i].enabled = true;
            }

            ladder.enabled = true;
        }

        public void EndAction() {
            for (int i = 0; i < spriteRenderers.Length; i++) {
                spriteRenderers[i].enabled = false;
            }

            ladder.enabled = false;
        }
    }

}