

using Main.Model;

namespace Main.Storage {

	public interface IStorage {

		void StoreGame(Data data);

		Data FetchGame();

		void StoreLevel(Data data, string name);

		Data FetchLevel(string name);

		GameButtonInfo FetchGameButton(string name);

		GameButtonInfo[] FetchGameButtons();

		bool HasGame();

		// Checks to see if there are still saved game files on the local machine
		bool HasLevels();

		void DeleteLevel(string name);

		void DeleteAllFiles();
	}

}