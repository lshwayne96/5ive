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

    private Platformer2DUserControl playerMovement;
    private Animator playerAnimator;
    private Rigidbody2D playerRb;
    private Rigidbody2D ballRb;

    public static bool isPaused { get; private set; }
    private bool isActive;
    // The original velocity of the player before it's rigidbody is disabled
    private Vector2 prePlayerVelocity;
    // The original velocity of the ball before it's rigidbody is disabled
    private Vector2 preBallVelocity;

    void Start() {
        // pauseMenu is initially active to get it's reference
        pauseMenu = GameObject.FindWithTag("PauseMenu");
        // Hide the pauseMenu
        pauseMenu.SetActive(false);
        // Manually set isPaused to false since static variables don't reset when a scene is reloaded
        isPaused = false;

        player = GameObject.FindWithTag("Player");
        playerRb = player.GetComponent<Rigidbody2D>();
        playerMovement = player.GetComponent<Platformer2DUserControl>();
        playerAnimator = player.GetComponent<Animator>();

        ballRb = GameObject.FindWithTag("TeleportationBall").GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Debug.Log("IsPaused: " + isPaused + " pauseMenu: " + pauseMenu.activeSelf);

            if (isPaused && pauseMenu.activeSelf) { // If the game is paused, unpause it
                Debug.Log("Unpause game");
                playerRb.velocity = prePlayerVelocity;
                ballRb.velocity = preBallVelocity;
                playerRb.WakeUp();
                ballRb.WakeUp();

                pauseMenu.SetActive(false);
                playerMovement.enabled = true;
                playerAnimator.enabled = true;
                isPaused = false;

            } else if (!isPaused && !pauseMenu.activeSelf) { // If the game is not paused, pause it
                Debug.Log("Pause game");
                prePlayerVelocity = playerRb.velocity;
                preBallVelocity = ballRb.velocity;
                playerRb.Sleep();
                ballRb.Sleep();

                pauseMenu.SetActive(true);
                playerMovement.enabled = false;
                playerAnimator.enabled = false;
                isPaused = true;
            }
        }
    }
}
