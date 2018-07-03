using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class LevelData {
    public int sceneBuildIndex;
    public PlayerData playerData;
    public BallData ballData;

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
}