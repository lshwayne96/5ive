using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets._2D;
using UnityEngine;

public class Pause : MonoBehaviour {
    private GameObject pauseMenu;
    private GameObject player;
    private GameObject ball;
    private Platformer2DUserControl playerMovementScript;
    private Rigidbody2D playerRb;
    private Rigidbody2D ballRb;
    private bool isPaused;
    private bool isActive;
    private Vector2 prevVelocity;

    // Use this for initialization
    void Start() {
        // pauseMenu is initially active to get it's reference
        pauseMenu = GameObject.FindWithTag("PauseControl");

        // Hide the pauseMenu
        pauseMenu.SetActive(false);

        player = GameObject.FindWithTag("Player");
        ball = GameObject.FindWithTag("TeleportationBall");

        playerRb = player.GetComponent<Rigidbody2D>();
        ballRb = ball.GetComponent<Rigidbody2D>();

        playerMovementScript = player.GetComponent<Platformer2DUserControl>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!isPaused) {
                prevVelocity = playerRb.velocity;
                playerRb.Sleep();
                ballRb.Sleep();
                pauseMenu.SetActive(true);

            } else {
                playerRb.velocity = prevVelocity;
                playerRb.WakeUp();
                ballRb.WakeUp();
                pauseMenu.SetActive(false);
            }

            playerMovementScript.enabled = !playerMovementScript.enabled;
            isPaused = !isPaused;
        }
    }
}
