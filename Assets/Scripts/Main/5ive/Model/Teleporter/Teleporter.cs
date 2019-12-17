using UnityEngine;

namespace Main._5ive.Model.Teleporter {

    public class Teleporter : MonoBehaviour {

        public bool isUsingCoordinates;

        public float x;

        public float y;

        private Transform teleportPosition;

        void Start() {
            teleportPosition = transform.GetChild(0);
        }

        void OnTriggerEnter2D(Collider2D collision) {
            if (isUsingCoordinates) {
                collision.transform.position = new Vector3(x, y, 0f);
            } else {
                collision.transform.position = teleportPosition.position;
            }
        }
    }

}