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
    private StandButtonData[] standButtonDatas;
    private LadderData[] ladderDatas;

    private StoryLineData[] storyLineDatas;

    public LevelData(Scene scene, PlayerData playerData, BallData ballData,
                     LeverData[] leverDatas, StandButtonData[] standButtonDatas,
                     LadderData[] ladderDatas, StoryLineData[] storyLineDatas) {
        this.sceneBuildIndex = scene.buildIndex;
        this.playerData = playerData;
        this.ballData = ballData;
        this.leverDatas = leverDatas;
        this.standButtonDatas = standButtonDatas;
        this.ladderDatas = ladderDatas;
        this.storyLineDatas = storyLineDatas;
    }

    public void Restore() {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerData.Restore(player);
        Ball ball = GameObject.FindGameObjectWithTag("TeleportationBall").GetComponent<Ball>();
        ballData.Restore(ball);

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

            StoryLine[] storyLines = componentManager.GetScripts<StoryLine>();
            for (int i = 0; i < storyLines.Length; i++) {
                storyLineDatas[i].Restore(storyLines[i]);
            }
        }
    }

    public int GetSceneBuildIndex() {
        return sceneBuildIndex;
    }
}