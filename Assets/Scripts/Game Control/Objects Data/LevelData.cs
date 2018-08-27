/*
 * This class represents the data of a level and is used to restore
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
    private FloorButtonData[] floorButtonDatas;
    private LadderData[] ladderDatas;

    private StoryLineData[] storyLineDatas;

    public LevelData(Scene scene, PlayerData playerData, BallData ballData,
                     LeverData[] leverDatas, FloorButtonData[] floorButtonDatas,
                     LadderData[] ladderDatas, StoryLineData[] storyLineDatas) {
        sceneBuildIndex = scene.buildIndex;
        this.playerData = playerData;
        this.ballData = ballData;
        this.leverDatas = leverDatas;
        this.floorButtonDatas = floorButtonDatas;
        this.ladderDatas = ladderDatas;
        this.storyLineDatas = storyLineDatas;
    }

    public void Restore() {
        GameObject componentManagerGO = GameObject.FindGameObjectWithTag("ComponentManager");
        if (componentManagerGO) {
            ComponentManager componentManager = componentManagerGO.GetComponent<ComponentManager>();

            Lever[] levers = componentManager.GetScripts<Lever>();
            for (int i = 0; i < levers.Length; i++) {
                leverDatas[i].Restore(levers[i]);
            }

            FloorButton[] standButtons = componentManager.GetScripts<FloorButton>();
            for (int i = 0; i < standButtons.Length; i++) {
                floorButtonDatas[i].Restore(standButtons[i]);
            }

            Ladder[] ladders = componentManager.GetScripts<Ladder>();
            TopOfLadder[] topOfLadders = componentManager.GetScripts<TopOfLadder>();
            for (int i = 0; i < ladders.Length; i++) {
                ladderDatas[i].Restore(ladders[i], topOfLadders[i]);
            }

            StoryLine[] storyLines = componentManager.GetScripts<StoryLine>();
            for (int i = 0; i < storyLines.Length; i++) {
                storyLineDatas[i].Restore(storyLines[i]);
            }
        }

        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerData.Restore(player);
        Ball ball = GameObject.FindGameObjectWithTag("TeleportationBall").GetComponent<Ball>();
        ballData.Restore(ball);
    }

    public int GetSceneBuildIndex() {
        return sceneBuildIndex;
    }
}