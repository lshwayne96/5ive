using UnityEngine;

public class Crate : MonoBehaviour {

    public Vector3 rightPosition;

    private Player player;

    private bool isCrateMoveable;
    private bool isMovingCrate;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (isCrateMoveable) {
                StartMovingCrate();
            } else {
                StopMovingCrate();
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.E)
            && !isMovingCrate) {
            isCrateMoveable = true;
        }
    }

    private void StartMovingCrate() {
        transform.SetParent(player.transform);
        isCrateMoveable = false;
        isMovingCrate = true;
    }

    private void StopMovingCrate() {
        transform.parent = null;
        isCrateMoveable = true;
        isMovingCrate = false;
    }
}
