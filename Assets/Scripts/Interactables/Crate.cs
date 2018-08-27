using UnityEngine;
using UnityStandardAssets._2D;

public class Crate : MonoBehaviour {

    private Player player;
    private PlatformerCharacter2D playerMovement;
    private Rigidbody2D crateRb;

    private bool isPlayerInContactWithCrate;
    private bool isMovingCrate;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerMovement = player.GetComponent<PlatformerCharacter2D>();
        crateRb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (isPlayerInContactWithCrate) {
                StartMovingCrate();
            } else {
                StopMovingCrate();
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.E)
            && !isMovingCrate) {
            isPlayerInContactWithCrate = true;
        }
    }

    private void StartMovingCrate() {
        transform.SetParent(player.transform);
        isPlayerInContactWithCrate = false;
        isMovingCrate = true;
        playerMovement.SlowMovementSpeed();
        transform.rotation = Quaternion.Euler(Vector3.zero);
        crateRb.velocity = Vector3.zero;
    }

    private void StopMovingCrate() {
        transform.parent = null;
        isMovingCrate = false;
        playerMovement.RestoreMovementSpeed();
    }
}
