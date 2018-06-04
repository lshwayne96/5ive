using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCameraSnapFollow : MonoBehaviour {

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        gameObject.transform.position =
            new Vector3(SetCurrentRoom.currentBallRoom.position.x,
                        SetCurrentRoom.currentBallRoom.position.y,
                        -10f);
    }
}
