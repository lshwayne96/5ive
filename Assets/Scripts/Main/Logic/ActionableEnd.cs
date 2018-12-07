using UnityEngine;

public class ActionableEnd : MonoBehaviour, IActionable {

    private SpriteRenderer spriteRenderer;
    public bool IsAbleToEnd { get; private set; }

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void StartAction() {
        spriteRenderer.enabled = true;
        IsAbleToEnd = true;
    }

    public void EndAction() {
        spriteRenderer.enabled = false;
        IsAbleToEnd = false;
    }
}
