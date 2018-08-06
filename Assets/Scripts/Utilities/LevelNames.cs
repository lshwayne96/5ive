/*
 * This is a utility class.
 * It provides a method to get the name associated with
 * a particular level.
 */

public class LevelName {

    // Hide the constructor
    private LevelName() {

    }

    public static string GetLevelName(int sceneBuildIndex) {
        switch (sceneBuildIndex) {
            case 1:
            return "1_Denial";

            case 2:
            return "2_Anger";

            case 3:
            return "3_Bargaining";

            case 4:
            return "4_Depression";

            case 5:
            return "5_Acceptance";

            default:
            return System.String.Empty;
        }
    }
}

