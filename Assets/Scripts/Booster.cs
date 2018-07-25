using UnityEngine;

public class Booster : MonoBehaviour, IMovable {

    // The force at which the gameObject's rigid body is pushed
    private Vector2 force;
    private Rigidbody2D rb;

    void Start() {
        force = new Vector2(0, 20);
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move() {
        rb.AddForce(force, ForceMode2D.Impulse);
    }
}
