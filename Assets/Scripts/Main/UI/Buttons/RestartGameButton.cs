using UnityEngine;

public class RestartGameButton : MonoBehaviour {

	public void RestartGame() {
		Game.instance.Reset();
	}
}
