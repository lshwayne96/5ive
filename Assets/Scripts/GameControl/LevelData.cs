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

    public LevelData(Scene scene, GameObject player, GameObject ball) {
        this.sceneBuildIndex = scene.buildIndex;

        // Cache player data
        Vector2 velocity = player.GetComponent<Rigidbody2D>().velocity;
        Vector3 position = player.transform.position;
        this.playerData = new PlayerData(velocity, position);

        // Cache ball data
        velocity = ball.GetComponent<Rigidbody2D>().velocity;
        position = ball.transform.position;
        this.ballData = new BallData(velocity, position);
    }

    public int GetSceneBuildIndex() {
        return sceneBuildIndex;
    }

    public PlayerData GetPlayerData() {
        return playerData;
    }

    public BallData GetBallData() {
        return ballData;
    }
}