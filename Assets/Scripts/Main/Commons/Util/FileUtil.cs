using System.IO;

namespace Main.Commons.Util {

	public static class FileUtil {

		public static void CreateFile(string path) {
			if (!File.Exists(path)) {
				File.Create(path).Close();
			}
		}

		public static void DeleteFile(string path) {
			File.Delete(path);
		}

		public static bool DoesFileExist(string path) {
			return File.Exists(path);
		}
	}

}
