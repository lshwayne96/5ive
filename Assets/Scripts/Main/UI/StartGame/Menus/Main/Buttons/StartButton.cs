using Main.Logic;
using UnityEngine;

namespace Main.UI.StartGame.Menus.Main.Buttons {

	public class StartButton : MonoBehaviour {

		public void StartGame() {
			Game.instance.StartGame();
		}
	}

}