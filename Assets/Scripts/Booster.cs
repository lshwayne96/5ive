using UnityEngine;

public class Booster : MonoBehaviour {

    // The force at which the gameObject's rigid body is pushed
    private Vector2 force;
    private Rigidbody2D rb;
    private bool hasInteracted;

    void Start() {
        force = new Vector2(0, 20);
        rb = GetComponent<Rigidbody2D>();
    }

    public void Interact() {
        rb.AddForce(force, ForceMode2D.Impulse);
        hasInteracted = true;
    }

    public bool HasInteracted() {
        return hasInteracted;
    }
}
