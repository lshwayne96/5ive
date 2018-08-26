using UnityEngine;

public class ActionableLadder : MonoBehaviour, IActionable {
    private SpriteRenderer[] spriteRenderers;
    private bool spriteRenderersState;

    private Ladder ladder;
    private bool ladderState;

    private void Start() {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        // Get the first spriteRenderer (any spriteRender will do actually)
        spriteRenderersState = spriteRenderers[0].enabled;

        ladder = GetComponent<Ladder>();
        ladderState = ladder.enabled;
    }

    public void StartAction() {
        ToggleState();
    }

    public void EndAction() {
        ToggleState();
    }

    private void ToggleState() {
        spriteRenderersState = !spriteRenderersState;
        for (int i = 0; i < spriteRenderers.Length; i++) {
            spriteRenderers[i].enabled = spriteRenderersState;
        }

        ladderState = !ladderState;
        ladder.enabled = ladderState;
    }
}
