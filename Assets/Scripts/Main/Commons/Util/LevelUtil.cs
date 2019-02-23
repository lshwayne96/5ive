using UnityEngine;
/// <summary>
/// This is a utility class that contains a utility method
/// pertaining to level mapping.
/// </summary>
public static class LevelUtil {

	/// <summary>
	/// Gets the level name that maps to the corresponding
	/// scene build index.
	/// </summary>
	/// <returns>The level name.</returns>
	/// <param name="sceneBuildIndex">Scene build index.</param>
	public static string GetLevelName(int sceneBuildIndex) {
		LevelName sceneName;
		switch (sceneBuildIndex) {
		case 0:
			sceneName = LevelName.MainMenu;
			break;

		case 1:
			sceneName = LevelName.Denial;
			break;

		case 2:
			sceneName = LevelName.Anger;
			break;

		case 3:
			sceneName = LevelName.Bargaining;
			break;

		case 4:
			sceneName = LevelName.Depression;
			break;

		case 5:
			sceneName = LevelName.Acceptance;
			break;

		default:
			Debug.Assert(false, "Invalid scene build index");
			return string.Empty;
		}

		return sceneName.ToString();
	}

	public static bool IsMainMenu(int sceneBuildIndex) {
		return sceneBuildIndex == (int) LevelName.MainMenu;
	}
}

