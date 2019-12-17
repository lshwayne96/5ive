using UnityEngine;

namespace Main._5ive.Model.Platform {

    public class Platform : MonoBehaviour {

        public LayerMask excludePlayer;

        private PlatformEffector2D platformEffector2D;

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

}