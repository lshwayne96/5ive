using UnityEngine;

public class TrapDoorTrigger : MonoBehaviour {

    public GameObject trapDoorGO;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            trapDoorGO.SetActive(true);
            Destroy(this);
        }
    }
}
