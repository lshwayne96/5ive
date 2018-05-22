using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleportRoom : MonoBehaviour {
	public MeshRenderer preview;

	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {
		if (Input.GetKeyDown(KeyCode.T)) {
			preview.enabled = true;
		}
		if (Input.GetKeyUp(KeyCode.T)) {
			preview.enabled = false;
			Teleport();
		}
	}

	private void Teleport() {
		// Swap the Player's and ball's rooms
		Vector3 positionDifference = PlayerStats.currentBallRoom.position - PlayerStats.currentRoom.position;
		transform.position += positionDifference;
		PlayerStats.ball.transform.position -= positionDifference;

		// Debug.Log("Teleported");
	}
}
