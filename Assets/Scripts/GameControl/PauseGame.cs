using UnityStandardAssets._2D;
using UnityEngine;

/*
 * This script pauses the game by bringing up the pause menu
 * and making both the player and ball freeze.
 * To prevent further input from moving the player,
 * the player movement script, Platformer2DUserControl, is disabled
 */
public class PauseGame : MonoBehaviour {

    private GameObject pauseMenu;
    private GameObject player;
    private GameObject ball;
    private Platformer2DUserControl playerMovementScript;
    private Rigidbody2D playerRb;
    private Rigidbody2D ballRb;
    private bool isPaused;
    private bool isActive;
    // The original velocity of the player before it's movement script is disabled
    private Vector2 prevVelocity;

    void Start() {
        // pauseMenu is initially active to get it's reference
        pauseMenu = GameObject.FindWithTag("PauseControl");

        // Hide the pauseMenu
        pauseMenu.SetActive(false);

        player = GameObject.FindWithTag("Player");
        ball = GameObject.FindWithTag("TeleportationBall");

        playerRb = player.GetComponent<Rigidbody2D>();
        ballRb = ball.GetComponent<Rigidbody2D>();

        // Get the player movement script
        playerMovementScript = player.GetComponent<Platformer2DUserControl>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!isPaused) { // If the game is not paused, pause it
                prevVelocity = playerRb.velocity;
                playerRb.Sleep();
                ballRb.Sleep();
                pauseMenu.SetActive(true);


            } else { // If the game is paused, unpause it
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
