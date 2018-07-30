/*
 * This script represents a scene and is used to restore
 * a level to its saved state.
 */

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class SceneData {

    private int sceneBuildIndex;
    private PlayerData playerData;
    private BallData ballData;

    private LeverData[] leverDatas;
    private StandButtonData[] standButtonDatas;

    public SceneData(Scene scene, PlayerData playerData, BallData ballData,
                     LeverData[] leverDatas, StandButtonData[] standButtonDatas) {
        this.sceneBuildIndex = scene.buildIndex;
        this.playerData = playerData;
        this.ballData = ballData;
        this.leverDatas = leverDatas;
        this.standButtonDatas = standButtonDatas;
    }

    public void Restore() {
        playerData.Restore();
        ballData.Restore();

        GameObject componentManagerGO = GameObject.FindGameObjectWithTag("ComponentManager");
        if (componentManagerGO) {
            ComponentManager componentManager = componentManagerGO.GetComponent<ComponentManager>();

            Lever[] levers = componentManager.GetScripts<Lever>();
            int numLevers = levers.Length;
            for (int i = 0; i < numLevers; i++) {
                leverDatas[i].Restore(levers[i]);
            }

            StandButton[] standButtons = componentManager.GetScripts<StandButton>();
            int numStandButtons = standButtons.Length;
            for (int i = 0; i < numStandButtons; i++) {
                standButtonDatas[i].Restore(standButtons[i]);
            }
        }
    }

    public int GetSceneBuildIndex() {
        return sceneBuildIndex;
    }
}