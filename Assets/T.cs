using UnityEngine;

public class T : MonoBehaviour {

    private SpriteRenderer spriteRenderer;

    void Start() {
        spriteRenderer = transform.parent.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("Called");
        if (collision.CompareTag("TeleportationBall")) {
            Debug.Log("Enter");
            spriteRenderer.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("TeleportationBall")) {
            Debug.Log("Exit");
            spriteRenderer.enabled = false;
        }
    }
}
