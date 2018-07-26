/*
 * This script represents a scene and is used to restore
 * a level to its saved state.
 */

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class LevelData {

    private int sceneBuildIndex;
    private PlayerData playerData;
    private BallData ballData;

    private LeverData[] leverDatas;

    public LevelData(Scene scene, PlayerData playerData,
                     BallData ballData, LeverData[] leverDatas) {
        this.sceneBuildIndex = scene.buildIndex;
        this.playerData = playerData;
        this.ballData = ballData;
        this.leverDatas = leverDatas;
    }

    public void Restore() {
        playerData.Restore();
        ballData.Restore();

        GameObject[] leverGameObjects = GameObject.FindGameObjectsWithTag("Lever");
        int numLevers = leverGameObjects.Length;
        Lever[] levers = new Lever[numLevers];

        for (int i = 0; i < numLevers; i++) {
            levers[i] = leverGameObjects[i].GetComponent<Lever>();
            leverDatas[i].Restore(levers[i]);
        }
    }

    public int GetSceneBuildIndex() {
        return sceneBuildIndex;
    }
}