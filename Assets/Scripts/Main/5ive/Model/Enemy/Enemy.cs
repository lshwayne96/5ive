using UnityEngine;

namespace Main._5ive.Model.Enemy {

	public class Enemy : MonoBehaviour {

		private float cooldown;

		public bool vertical = true;

		public bool horizontal = false;

		public float verticalKnockback = 8f;

		public float horizontalKnockback = 2500f;

		// Use this for initialization
		void Start() {

		}

		// Update is called once per frame
		void Update() {
			cooldown -= Time.deltaTime;
		}

		void OnTriggerEnter2D(Collider2D collision) {
			if (collision.gameObject.CompareTag("Player")) {
				if (cooldown <= 0) {
					if (vertical) {
						collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, verticalKnockback);
					}

					if (horizontal) {
						Vector2 dir = (Vector2) collision.transform.position - (Vector2) transform.position;
						dir = dir.normalized;
						collision.GetComponent<Rigidbody2D>().AddForce(dir * new Vector2(horizontalKnockback, 0f));
					}

					cooldown = 0f;
				}
			}
		}
	}

}

/*
 public class Wander : MonoBehaviour {

private Vector3 moveDirection;
public float moveSpeed = 3f;

// Use this for initialization
void Start () {
    moveDirection = Vector3.right;
}

// Update is called once per frame
void Update () {
    transform.position += moveDirection * Time.deltaTime * moveSpeed;
}

void OnCollisionEnter2D (Collision2D collision) {
    moveDirection = Vector3.Scale(moveDirection, new Vector3(-1,0,0));
}
}
 */