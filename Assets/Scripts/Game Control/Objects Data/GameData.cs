/*
 * This class represents the data of the game and is used to restore
 * the game to its saved state.
 */

using System;

[Serializable]
public class GameData {
    private int numLevels;
    private int numLevelsCompleted;
    private bool hasSavedBefore;
    private bool[] collectibleLocations;
    private string lastSavedFileName;

    public GameData(int numLevels) {
        this.numLevels = numLevels;
        this.numLevelsCompleted = 1;
        this.hasSavedBefore = false;
        this.collectibleLocations = new bool[numLevels + 1];
        for (int i = 1; i < collectibleLocations.Length; i++) {
            collectibleLocations[i] = true;
        }
    }

    public bool DoesSceneContainCollectible(int sceneBuildIndex) {
        return collectibleLocations[sceneBuildIndex];
    }

    public void UpdateCollectibleLocations(int sceneBuildIndex) {
        collectibleLocations[sceneBuildIndex] = false;
    }

    public void UpdateNumLevelsCompleted() {
        numLevelsCompleted++;
    }

    public int GetNumLevelsCompleted() {
        return numLevelsCompleted;
    }

    public bool HasSavedBefore() {
        return hasSavedBefore;
    }

    public void SetHasSavedBefore(bool boolean) {
        hasSavedBefore = boolean;
    }

    public void SetLastSavedFileName(string fileName) {
        lastSavedFileName = fileName;
    }

    public string GetLastSavedFileName() {
        return lastSavedFileName;
    }

}
