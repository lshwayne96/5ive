using UnityEngine;

namespace Main._5ive {

    public class Placeholder : MonoBehaviour {
        private void Awake() {
            GameObject gameControllerGameObject = GameObject.FindWithTag("Game Controller");
            int childrenCount = transform.childCount;
            for (int i = 0; i < childrenCount; i++) {
                transform.GetChild(0).SetParent(gameControllerGameObject.transform);
            }

            Destroy(gameObject);
        }
    }

}