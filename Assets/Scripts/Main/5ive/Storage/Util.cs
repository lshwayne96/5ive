using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Main._5ive.Storage {

    internal static class Util {
        internal static void Serialise(string directoryPath, object data, string path) {
            if (!Directory.Exists(directoryPath)) {
                Directory.CreateDirectory(directoryPath);
            }
            FileStream fileStream = File.Create(path);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(fileStream, data);
            fileStream.Close();
        }

        internal static T Deserialise<T>(string path) {
            FileStream fileStream = File.Open(path, FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            T data = (T) binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
            return data;
        }
    }

}