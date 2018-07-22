using UnityEngine;

public class Booster : MonoBehaviour {
    private Rigidbody2D ballRb;

    void Start() {
        ballRb = GameObject.FindWithTag("TeleportationBall").GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.F)) {
            Boost();
        }
    }

    public void Boost() {
        Vector2 force = new Vector2(1, 10);
        ballRb.AddForce(force, ForceMode2D.Impulse);
    }
}
