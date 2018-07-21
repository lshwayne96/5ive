/*
 * This script represents a scene and is used to restore
 * a level to its saved state.
 */

using System;
using UnityEngine.SceneManagement;

[Serializable]
public class LevelData {

    private int sceneBuildIndex;
    private PlayerData playerData;
    private BallData ballData;
    private InteractablesData interactablesData;

    public LevelData(Scene scene, PlayerData playerData,
                     BallData ballData, InteractablesData interactablesData) {
        this.sceneBuildIndex = scene.buildIndex;
        this.playerData = playerData;
        this.ballData = ballData;
        this.interactablesData = interactablesData;
    }

    public void Restore() {
        playerData.Restore();
        ballData.Restore();
        interactablesData.Restore();
    }

    public int GetSceneBuildIndex() {
        return sceneBuildIndex;
    }
}