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
    private StandButtonData[] standButtonDatas;
    private LadderData[] ladderDatas;

    public LevelData(Scene scene, PlayerData playerData, BallData ballData,
                     LeverData[] leverDatas, StandButtonData[] standButtonDatas,
                     LadderData[] ladderDatas) {
        this.sceneBuildIndex = scene.buildIndex;
        this.playerData = playerData;
        this.ballData = ballData;
        this.leverDatas = leverDatas;
        this.standButtonDatas = standButtonDatas;
        this.ladderDatas = ladderDatas;
    }

    public void Restore() {
        playerData.Restore();
        ballData.Restore();

        GameObject componentManagerGO = GameObject.FindGameObjectWithTag("ComponentManager");
        if (componentManagerGO) {
            ComponentManager componentManager = componentManagerGO.GetComponent<ComponentManager>();

            Lever[] levers = componentManager.GetScripts<Lever>();
            for (int i = 0; i < levers.Length; i++) {
                leverDatas[i].Restore(levers[i]);
            }

            StandButton[] standButtons = componentManager.GetScripts<StandButton>();
            for (int i = 0; i < standButtons.Length; i++) {
                standButtonDatas[i].Restore(standButtons[i]);
            }

            Ladder[] ladders = componentManager.GetScripts<Ladder>();
            TopOfLadder[] topOfLadders = componentManager.GetScripts<TopOfLadder>();
            for (int i = 0; i < ladders.Length; i++) {
                ladderDatas[i].Restore(ladders[i], topOfLadders[i]);
            }
        }
    }

    public int GetSceneBuildIndex() {
        return sceneBuildIndex;
    }
}