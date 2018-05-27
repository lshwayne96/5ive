using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomOfLadder : MonoBehaviour {
    private Ladder ladderScript;

    // Use this for initialization
    void Start() {
        ladderScript = GetComponentInParent<Ladder>();
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D collision) {

    }
}

