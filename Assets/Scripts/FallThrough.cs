using UnityEngine;

public class FallThrough : MonoBehaviour {

    private BoxCollider2D boxCollider2D;

    void Start() {
        boxCollider2D = GetComponent<BoxCollider2D>();

    }

    void Update() {
        if (Input.GetKey(KeyCode.DownArrow)) {
            boxCollider2D.isTrigger = true;
        } else {
            boxCollider2D.isTrigger = false;
        }
    }
}
