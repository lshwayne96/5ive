using UnityStandardAssets._2D;
using UnityEngine;

/*
 * This script pauses the game by bringing up the pause menu
 * and making both the player and ball freeze.
 * To prevent further input from moving the player,
 * the player movement script, Platformer2DUserControl, is disabled
 */
public class PauseLevel : MonoBehaviour {

    private GameObject pauseMenu;
    private GameObject player;
    private GameObject ball;

    private Platformer2DUserControl playerMovementScript;
    private Rigidbody2D playerRb;
    private Rigidbody2D ballRb;

    private static bool isPaused;
    private bool isActive;
    // The original velocity of the player before it's rigidbody is disabled
    private Vector2 prePlayerVelocity;
    // The original velocity of the ball before it's rigidbody is disabled
    private Vector2 preBallVelocity;

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

        Time.timeScale = 1;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!isPaused) { // If the game is not paused, pause it
                prePlayerVelocity = playerRb.velocity;
                preBallVelocity = ballRb.velocity;
                playerRb.Sleep();
                ballRb.Sleep();
                pauseMenu.SetActive(true);
                Time.timeScale = 0;

            } else { // If the game is paused, unpause it
                playerRb.velocity = prePlayerVelocity;
                ballRb.velocity = preBallVelocity;
                playerRb.WakeUp();
                ballRb.WakeUp();
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
            }

            playerMovementScript.enabled = !playerMovementScript.enabled;
            isPaused = !isPaused;
        }
    }

    public static bool IsLevelPaused() {
        return isPaused;
    }
}
