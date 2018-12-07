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

	public static bool IsPaused { get; private set; }
	private bool isActive;
	// The original velocity of the player before it's rigidbody is disabled
	private Vector2 prePlayerVelocity;
	// The original velocity of the ball before it's rigidbody is disabled
	private Vector2 preBallVelocity;

	private void Start() {
		// pauseMenu is initially active to get it's reference
		pauseMenu = GameObject.FindWithTag("PauseMenu");
		// Hide the pauseMenu
		pauseMenu.SetActive(false);
		// Manually set isPaused to false since static variables don't reset when a scene is reloaded
		IsPaused = false;

		player = GameObject.FindWithTag("Player");
		playerRb = player.GetComponent<Rigidbody2D>();
		playerMovement = player.GetComponent<Platformer2DUserControl>();
		playerAnimator = player.GetComponent<Animator>();

		ballRb = GameObject.FindWithTag("TeleportationBall").GetComponent<Rigidbody2D>();
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (IsGamePaused()) { // If the game is paused, unpause it
				Unpause();

			} else if (IsGameUnpaused()) { // If the game is not paused, pause it
				Pause();
			}
		}
	}

	private bool IsGamePaused() {
		return IsPaused && pauseMenu.activeSelf;
	}

	private bool IsGameUnpaused() {
		return !IsPaused && !pauseMenu.activeSelf;
	}

	private void Pause() {
		prePlayerVelocity = playerRb.velocity;
		preBallVelocity = ballRb.velocity;
		playerRb.Sleep();
		ballRb.Sleep();

		pauseMenu.SetActive(true);
		playerMovement.enabled = false;
		playerAnimator.enabled = false;
		IsPaused = true;
	}

	private void Unpause() {
		playerRb.velocity = prePlayerVelocity;
		ballRb.velocity = preBallVelocity;
		playerRb.WakeUp();
		ballRb.WakeUp();

		pauseMenu.SetActive(false);
		playerMovement.enabled = true;
		playerAnimator.enabled = true;
		IsPaused = false;
	}
}
