using System.IO;
using Main._5ive.Model;
using UnityEngine;
using static Main._5ive.Storage.Util;

namespace Main._5ive.Storage {

    public class GameStorage : AbstractStorage {
        private static readonly string GameDirectoryPath = Application.persistentDataPath + Path.DirectorySeparatorChar + "Games";
        private const string GameFileName = "5ive" + FileExtension;
        private static readonly string GameFilePath = GameDirectoryPath + Path.DirectorySeparatorChar + GameFileName;

        public static bool DoesGameExist() {
            return File.Exists(GameFilePath);
        }

        public static void StoreGame(State state) {
            Serialise(GameDirectoryPath, state, GameFilePath);
        }

        public static State FetchGame() {
            return Deserialise<State>(GameFilePath);
        }

        public static void DeleteGameFile() {
            File.Delete(GameFilePath);
        }

        public static void Reset() {
            File.Delete(GameFilePath);
        }
    }

}