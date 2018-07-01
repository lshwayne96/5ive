using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {
    private GameObject player;
    private GameObject ball;
    private Rigidbody2D playerRb;
    private Rigidbody2D ballRb;
    private bool isPaused;

    // Use this for initialization
    void Start() {
        player = GameObject.FindWithTag("Player");
        ball = GameObject.FindWithTag("TeleportationBall");
        playerRb = player.GetComponent<Rigidbody2D>();
        ballRb = ball.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!isPaused) {
                playerRb.Sleep();
                ballRb.Sleep();

            } else {
                playerRb.WakeUp();
                ballRb.WakeUp();
                Debug.Log("Awake");
            }

            isPaused = !isPaused;
        }

        if (isPaused) {
            if (Input.anyKey) {
                playerRb.Sleep();
            }
        }
    }
}
