using Main._5ive.Model;

namespace Main._5ive.Storage {
	public interface IStorage {
		bool DoesGameExist();
		void StoreGame(State state);
		State FetchGame();

		void StoreLevel(State state, string name);
		State FetchLevel(string name);
		void DeleteLevelFile(string name);

		void StoreGameButtons(State[] states);
		State[] FetchGameButtons();

		void Reset();
	}

}