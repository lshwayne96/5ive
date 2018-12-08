using UnityEngine;

public class Booster : MonoBehaviour, IActionable {

	public Vector2 pushForce;
	private Rigidbody2D rb;

	void Start() {
		pushForce = new Vector2(0, 20);
		rb = GetComponent<Rigidbody2D>();
	}

	public void StartAction() {
		rb.AddForce(pushForce, ForceMode2D.Impulse);
	}

	public void EndAction() {

	}
}
