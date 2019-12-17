using System.IO;
using Main._5ive.Model;
using UnityEngine;
using static Main._5ive.Storage.Util;

namespace Main._5ive.Storage {

    public class GameButtonStorage : AbstractStorage {
	    private static readonly string GameButtonDirectoryPath = Application.persistentDataPath + Path.DirectorySeparatorChar + "GameButtons";
	    private const string GameButtonFileName = "game_buttons" + FileExtension;
	    private static readonly string GameButtonFilePath = GameButtonDirectoryPath + Path.DirectorySeparatorChar + GameButtonFileName;

	    public static void StoreGameButtons(State[] states) {
		    Serialise(GameButtonDirectoryPath, states, GameButtonFilePath);
	    }

	    public static State[] FetchGameButtons() {
		    return Deserialise<State[]>(GameButtonDirectoryPath);
	    }

	    public static void Reset() {
		    File.Delete(GameButtonFilePath);
	    }
    }

}