using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreGame : MonoBehaviour {
    public static RestoreGame restoreGame;
    private LevelData levelData;
    private GameObject player;
    private GameObject ball;
    private bool hasSavedGame;

    private void Awake() {
        if (restoreGame == null) {
            DontDestroyOnLoad(gameObject);
            restoreGame = this;
            Debug.Log("Make this the restoreGame");

        } else if (restoreGame != this) {
            Destroy(gameObject);
            Debug.Log("This is not the restoreGame");
        }
    }

    public void Take(LevelData levelData, GameObject player, GameObject ball) {
        this.levelData = levelData;
        this.player = player;
        this.ball = ball;
        this.hasSavedGame = true;
    }

    public void Restore() {
        // Debug.Log("Restore");

        player = GameObject.FindWithTag("Player");
        ball = GameObject.FindWithTag("TeleportationBall");

        PlayerData playerData = levelData.playerData;
        player.GetComponent<Rigidbody2D>().velocity = playerData.velocity();
        player.transform.position = playerData.position();

        BallData ballData = levelData.ballData;
        ball.GetComponent<Rigidbody2D>().velocity = ballData.velocity();
        ball.transform.localPosition = ballData.position();

        /*
        Debug.Log("Velocity");

        Vector2 vector = player.GetComponent<Rigidbody2D>().velocity;
        Debug.Log(vector.x);
        Debug.Log(vector.y);

        Debug.Log("Position");

        Vector3 vector1 = player.transform.localPosition;
        Debug.Log(vector1.x);
        Debug.Log(vector1.y);
        Debug.Log(vector1.z);
        */
    }

    private void OnLevelWasLoaded(int level) {
        if (hasSavedGame) {
            RestoreGame.restoreGame.Restore();
        }
    }
}

