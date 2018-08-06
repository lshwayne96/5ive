using System;

[Serializable]
public class GameData {
    public int numLevels;
    public int numLevelsCompleted;
    public bool[] collectibleLocations;

    public GameData(int numLevels) {
        this.numLevels = numLevels;
        this.collectibleLocations = new bool[numLevels + 1];
        for (int i = 1; i < collectibleLocations.Length; i++) {
            collectibleLocations[i] = true;
        }
    }

    public bool DoesSceneContainCollectible(int sceneBuildIndex) {
        return collectibleLocations[sceneBuildIndex];
    }

    public void UpdateCollectibleLocation(int sceneBuildIndex) {
        collectibleLocations[sceneBuildIndex] = false;
    }

    public void UpdateNumLevelsCompleted() {
        numLevelsCompleted++;
    }

    public int GetNumLevelsCompleted() {
        return numLevelsCompleted;
    }

}
