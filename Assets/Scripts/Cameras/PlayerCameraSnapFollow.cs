using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraSnapFollow : MonoBehaviour {

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        gameObject.transform.position =
            new Vector3(SetCurrentRoom.currentPlayerRoom.position.x,
                        SetCurrentRoom.currentPlayerRoom.position.y,
                        -10f);
    }
}
