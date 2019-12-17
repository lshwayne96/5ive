using Main._5ive.Model;

namespace Main._5ive.Storage {

	public class StorageFacade : IStorage {
		public bool DoesGameExist() {
			return GameStorage.DoesGameExist();
		}

		public void StoreGame(State state) {
			GameStorage.StoreGame(state);
		}

		public State FetchGame() {
			return GameStorage.FetchGame();
		}

		public void StoreLevel(State state, string name) {
			LevelStorage.StoreLevel(state, name);
		}

		public State FetchLevel(string name) {
			return LevelStorage.FetchLevel(name);
		}

		public void DeleteLevelFile(string name) {
			LevelStorage.DeleteLevelFile(name);
		}

		public void StoreGameButtons(State[] states) {
			GameButtonStorage.StoreGameButtons(states);
		}

		public State[] FetchGameButtons() {
			return GameButtonStorage.FetchGameButtons();
		}

		public void Reset() {
			GameStorage.Reset();
			LevelStorage.Reset();
			GameButtonStorage.Reset();
		}
	}
}