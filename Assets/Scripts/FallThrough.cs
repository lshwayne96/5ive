using UnityEngine;

public class FallThrough : MonoBehaviour {

    private PlatformEffector2D platformEffector2D;
    public LayerMask excludePlayer;

    void Start() {
        platformEffector2D = GetComponent<PlatformEffector2D>();

    }

    void Update() {
        if (Input.GetKey(KeyCode.DownArrow)) {
            platformEffector2D.colliderMask = excludePlayer;
        } else {
            platformEffector2D.colliderMask = -1;
        }
    }
}
