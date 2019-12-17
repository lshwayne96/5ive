using System.IO;
using Main._5ive.Model;
using UnityEngine;

namespace Main._5ive.Storage {

    public class LevelStorage : AbstractStorage {
        private static readonly string LevelDirectoryPath = Application.persistentDataPath + Path.DirectorySeparatorChar + "Levels";
        /// <summary>
        /// A tag added to game files to distinguish them from non-game files.
        /// </summary>
        private const string Tag = "_unity";

        //TODO
        public static bool HasLevels() {
            FileInfo[] fileInfos = Directory.CreateDirectory(LevelDirectoryPath).GetFiles();
            foreach (FileInfo fileInfo in fileInfos) {
                string fileName = fileInfo.Name;
                if (ContainsTag(fileName)) {
                    return true;
                }
            }

            return false;
        }

        public static void StoreLevel(State state, string name) {
            string path = ToPath(name);
            Util.Serialise(LevelDirectoryPath, state, path);
        }

        public static State FetchLevel(string name) {
            string path = ToPath(name);
            return Util.Deserialise<State>(path);
        }

        public static void DeleteLevelFile(string name) {
            string path = ToPath(name);
            File.Delete(path);
        }

        public static void DeleteAllLevelFiles() {
            var filePaths = Directory.GetFiles(LevelDirectoryPath);
            foreach (string path in filePaths) {
                File.Delete(path);
            }
        }

        /// <summary>
        /// Converts the file name to its corresponding path.
        /// </summary>
        /// <returns>The path.</returns>
        /// <param name="name">Name.</param>
        private static string ToPath(string name) {
            if (name.Contains(Tag)) {
                return LevelDirectoryPath + "/" + name;
            }
            return LevelDirectoryPath + "/" + string.Concat(Tag, name) + FileExtension;
        }

        /// <summary>
        /// Converts the path to its corresponding file name.
        /// </summary>
        /// <returns>The file name.</returns>
        /// <param name="path">Path.</param>
        private static string ToName(string path) {
            string pathWithoutExtension = TrimExtension(path);
            return TrimDirectory(pathWithoutExtension);
        }

        private static string TrimExtension(string path) {
            return path.Substring(0, path.Length - FileExtension.Length);
        }

        private static string TrimDirectory(string path) {
            return path.Substring(LevelDirectoryPath.Length, path.Length - LevelDirectoryPath.Length);
        }

        /// <summary>
        /// Extracts the tag from the file name and returns it.
        /// </summary>
        /// <remarks>
        /// The file name is first checked if
        /// it contains the tag in the first place.
        /// </remarks>
        /// <returns>The tag.</returns>
        /// <param name="fileName">File name.</param>
        public static string ExtractTag(string fileName) {
            if (!ContainsTag(fileName)) {
                return string.Empty;
            }

            return Tag;
        }

        /// <summary>
        /// Checks if the file name contains a tag.
        /// </summary>
        /// <returns>
        /// <c>true</c>, if a tag is present,
        /// <c>false</c> otherwise.</returns>
        /// <param name="fileName">File name.</param>
        public static bool ContainsTag(string fileName) {
            return fileName.Contains(Tag);
        }

        public static void Reset() {
            Directory.Delete(LevelDirectoryPath);
            Directory.CreateDirectory(LevelDirectoryPath);
        }
    }

}