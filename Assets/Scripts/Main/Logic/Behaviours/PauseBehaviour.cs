using UnityStandardAssets._2D;
using UnityEngine;
using System;

/// <summary>
/// This script pauses the game by bringing up the game menu
/// and makes all objects in the game freeze.
/// </summary>
/// <remarks>
/// /// To prevent further input from the user,
/// the player movement script, Platformer2DUserControl, is disabled.
/// </remarks>
public class PauseBehaviour {

	public bool IsActive { get; private set; }

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

	public PauseBehaviour() {
		menu = GameObject.FindWithTag(Tags.Menu);
		menu.SetActive(false);

		playerGO = GameObject.FindWithTag(Tags.Player);
		playerRb = playerGO.GetComponent<Rigidbody2D>();
		playerMovement = playerGO.GetComponent<Platformer2DUserControl>();
		playerAnimator = playerGO.GetComponent<Animator>();

		ballRb = GameObject.FindWithTag(Tags.Ball).GetComponent<Rigidbody2D>();
	}

	/// <summary>
	/// Pauses the game.
	/// </summary>
	/// <remarks>
	/// Time is not affected.
	/// </remarks>
	public void Enable() {
		prevPlayerVelo = playerRb.velocity;
		playerRb.Sleep();

		prevBallVelo = ballRb.velocity;
		ballRb.Sleep();

		menu.SetActive(true);

		playerMovement.enabled = false;
		playerAnimator.enabled = false;

		IsActive = true;
	}

	/// <summary>
	/// Unpauses the game.
	/// </summary>
	/// <remarks>
	/// Time is not affected.
	/// </remarks>
	public void Disable() {
		playerRb.velocity = prevPlayerVelo;
		playerRb.WakeUp();

		ballRb.velocity = prevBallVelo;
		ballRb.WakeUp();

		menu.SetActive(false);

		playerMovement.enabled = true;
		playerAnimator.enabled = true;

		IsActive = false;
	}
}
