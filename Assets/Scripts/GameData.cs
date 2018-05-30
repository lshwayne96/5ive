using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class serialises the following game properties.
 * 1. All flags that the player has.
 * 2. Completed game levels.
 * 3. Current game level.
 * 4. Collectibles obtained.
 * 5. Custom settings (sound, graphics)
 * 6. Saved games.
 * 7. Puzzles solved in the game level.
 * 8. Player's position in the game level.
 */

[Serializable]
public class GameData {
    // Puts in new data into or updates outdated data in this GameData object
    public void populate() {

    }
}
