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

    public LevelData(Scene scene, PlayerData playerData,
                     BallData ballData, LeverData[] leverDatas,
                     StandButtonData[] standButtonDatas) {
        this.sceneBuildIndex = scene.buildIndex;
        this.playerData = playerData;
        this.ballData = ballData;
        this.leverDatas = leverDatas;
        this.standButtonDatas = standButtonDatas;
    }

    public void Restore() {
        playerData.Restore();
        ballData.Restore();

        /*
        GameObject[] leverGameObjects = GameObject.FindGameObjectsWithTag("Lever");
        int numLevers = leverGameObjects.Length;
        Lever[] levers = new Lever[numLevers];
        */

        GameObject tempObject = GameObject.FindGameObjectWithTag("Temp");
        Temp temp;
        if (tempObject)
        {
            temp = tempObject.GetComponent<Temp>();
            Lever[] levers = temp.Return();
            int numLevers = levers.Length;


            for (int i = 0; i < numLevers; i++)
            {
                leverDatas[i].Restore(levers[i]);
            }
        }

        GameObject[] standButtonGameObjects = GameObject.FindGameObjectsWithTag("StandButton");
        int numStandButtons = standButtonGameObjects.Length;
        StandButton[] standButtons = new StandButton[numStandButtons];

        for (int i = 0; i < numStandButtons; i++) {
            standButtons[i] = standButtonGameObjects[i].GetComponent<StandButton>();
            standButtonDatas[i].Restore(standButtons[i]);
        }
    }

    public int GetSceneBuildIndex() {
        return sceneBuildIndex;
    }
}