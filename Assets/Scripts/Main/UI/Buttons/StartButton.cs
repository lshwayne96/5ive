using UnityEngine;

public class StartButton : MonoBehaviour {

	public void StartGame() {
		Game.instance.StartGame();
	}
}
