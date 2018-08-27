using UnityEngine;
using UnityStandardAssets._2D;

public class Crate : MonoBehaviour {

    private Player player;
    private PlatformerCharacter2D playerMovement;
    private Rigidbody2D crateRb;

    private bool isCrateMoveable;
    private bool isMovingCrate;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerMovement = player.GetComponent<PlatformerCharacter2D>();
        crateRb = GetComponent<Rigidbody2D>();
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
        playerMovement.SlowMovementSpeed();
        crateRb.simulated = false;
    }

    private void StopMovingCrate() {
        transform.parent = null;
        isCrateMoveable = true;
        isMovingCrate = false;
        playerMovement.RestoreMovementSpeed();
        crateRb.simulated = true;
    }
}
