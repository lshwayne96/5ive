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
		LevelNames sceneName;
		switch (sceneBuildIndex) {
		case 0:
			sceneName = LevelNames.MainMenu;
			break;

		case 1:
			sceneName = LevelNames.Denial;
			break;

		case 2:
			sceneName = LevelNames.Anger;
			break;

		case 3:
			sceneName = LevelNames.Bargaining;
			break;

		case 4:
			sceneName = LevelNames.Depression;
			break;

		case 5:
			sceneName = LevelNames.Acceptance;
			break;

		default:
			Debug.Assert(false, "Invalid scene build index");
			return string.Empty;
		}

		return sceneName.ToString();
	}
}

