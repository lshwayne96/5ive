using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets._2D;
using UnityEngine;

public class Pause : MonoBehaviour {
    private GameObject player;
    private GameObject ball;
    private Platformer2DUserControl playerMovementScript;
    private Rigidbody2D playerRb;
    private Rigidbody2D ballRb;
    private bool isPaused;
    private Vector2 prevVelocity;

    // Use this for initialization
    void Start() {
        player = GameObject.FindWithTag("Player");
        ball = GameObject.FindWithTag("TeleportationBall");
        playerMovementScript = player.GetComponent<Platformer2DUserControl>();
        playerRb = player.GetComponent<Rigidbody2D>();
        ballRb = ball.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!isPaused) {
                prevVelocity = playerRb.velocity;
                playerRb.Sleep();
                ballRb.Sleep();

            } else {
                playerRb.velocity = prevVelocity;
                playerRb.WakeUp();
                ballRb.WakeUp();
            }

            playerMovementScript.enabled = !playerMovementScript.enabled;
            isPaused = !isPaused;
        }
    }
}
