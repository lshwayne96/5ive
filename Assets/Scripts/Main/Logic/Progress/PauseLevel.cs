using UnityStandardAssets._2D;
using UnityEngine;

/// <summary>
/// This script pauses the game by bringing up the game menu
/// and makes all objects in the game freeze.
/// </summary>
/// <remarks>
/// /// To prevent further input from the user,
/// the player movement script, Platformer2DUserControl, is disabled.
/// </remarks>
public class PauseLevel : MonoBehaviour {

	public static bool IsPaused { get; private set; }

	private GameObject menu;

	private GameObject playerGO;
	private Rigidbody2D playerRb;
	private Platformer2DUserControl playerMovement;
	private Animator playerAnimator;

	private Rigidbody2D ballRb;

	// The velocity of the player before it's rigidbody is disabled
	private Vector2 prevPlayerVelo;

	// The velocity of the ball before it's rigidbody is disabled
	private Vector2 prevBallVelo;

	private void Start() {
		InitAndHideMenu();

		// Manually set to false since static variables don't reset when a scene is reloaded
		IsPaused = false;

		playerGO = GameObject.FindWithTag(Tags.Player);
		playerRb = playerGO.GetComponent<Rigidbody2D>();
		playerMovement = playerGO.GetComponent<Platformer2DUserControl>();
		playerAnimator = playerGO.GetComponent<Animator>();

		ballRb = GameObject.FindWithTag(Tags.Ball).GetComponent<Rigidbody2D>();
	}

	/// <summary>
	/// Initialises the menu game object and hides it immediately after.
	/// </summary>
	private void InitAndHideMenu() {
		// menu is initially active to get it's reference
		menu = GameObject.FindWithTag(Tags.Menu);
		// Hide the pauseMenu
		menu.SetActive(false);
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (IsGamePaused()) {
				DisablePause();
			} else {
				EnablePause();
			}
		}
	}

	/// <summary>
	/// Checks if the game is paused.
	/// </summary>
	/// <returns><c>true</c>, if game is paused, <c>false</c> otherwise.</returns>
	private bool IsGamePaused() {
		return IsPaused && menu.activeSelf;
	}

	/// <summary>
	/// Enables pause.
	/// </summary>
	/// <remarks>
	/// Time is not affected.
	/// </remarks>
	private void EnablePause() {
		prevPlayerVelo = playerRb.velocity;
		playerRb.Sleep();

		prevBallVelo = ballRb.velocity;
		ballRb.Sleep();

		menu.SetActive(true);

		playerMovement.enabled = false;
		playerAnimator.enabled = false;

		IsPaused = true;
	}

	/// <summary>
	/// Disables 
	/// </summary>
	/// <remarks>
	/// Time is not affected.
	/// </remarks>
	private void DisablePause() {
		playerRb.velocity = prevPlayerVelo;
		playerRb.WakeUp();

		ballRb.velocity = prevBallVelo;
		ballRb.WakeUp();

		menu.SetActive(false);

		playerMovement.enabled = true;
		playerAnimator.enabled = true;

		IsPaused = false;
	}
}
