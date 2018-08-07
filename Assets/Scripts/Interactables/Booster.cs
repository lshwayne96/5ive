using UnityEngine;

public class Booster : MonoBehaviour {

    public Vector2 pushForce;
    private Rigidbody2D rb;

    void Start() {
        pushForce = new Vector2(0, 20);
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move() {
        rb.AddForce(pushForce, ForceMode2D.Impulse);
    }
}
