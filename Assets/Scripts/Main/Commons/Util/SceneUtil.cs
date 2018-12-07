using UnityEngine;
/// <summary>
/// This is a utility class that contains a util method
/// pertaining to level mapping.
/// </summary>
public static class SceneUtil {

	// x refers to any possible character
	private const string Preamble = "_x_";

	/// <summary>
	/// Gets the scene name that maps to the corresponding
	/// scene build index, removes the preamble and returns
	/// the rest of the string (also known as the level name).
	/// </summary>
	/// <returns>The level name.</returns>
	/// <param name="sceneBuildIndex">Scene build index.</param>
	public static string GetLevelName(int sceneBuildIndex) {
		SceneNames sceneName;
		switch (sceneBuildIndex) {
		case 0:
			sceneName = SceneNames._0_Main_Menu;
			break;

		case 1:
			sceneName = SceneNames._1_Denial;
			break;

		case 2:
			sceneName = SceneNames._2_Anger;
			break;

		case 3:
			sceneName = SceneNames._3_Bargaining;
			break;

		case 4:
			sceneName = SceneNames._4_Depresssion;
			break;

		case 5:
			sceneName = SceneNames._5_Acceptance;
			break;

		default:
			Debug.Assert(false, "Invalid scene build index");
			return string.Empty;
		}

		string levelName = RemovePreamble(sceneName.ToString());
		return levelName;
	}

	private static string RemovePreamble(string sceneName) {
		return sceneName.Substring(Preamble.Length);
	}
}

