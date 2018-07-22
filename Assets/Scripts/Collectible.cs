using UnityEngine;

public class Collectible : MonoBehaviour {

    private bool canCollect;

    private void Update() {
        if (canCollect && Input.GetKeyDown(KeyCode.C)) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            canCollect = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            canCollect = false;
        }
    }
}
