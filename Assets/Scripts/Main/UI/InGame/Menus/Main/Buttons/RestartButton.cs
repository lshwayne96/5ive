using Main.Logic;
using UnityEngine;

namespace Main.UI.InGame.Buttons {

	public class RestartGameButton : MonoBehaviour {

		public void RestartGame() {
			Game.instance.ResetGame();
		}
	}

}