/*
 * This script is for a Singleton object which will not be destroyed
 * when a new scene is loaded.
 * It restores the game data from a saved game file after
 * the scene of the game file is loaded.
 */

using UnityEngine;

public class RestoreGame : MonoBehaviour {

    // The Singleton RestoreGame instance
    public static RestoreGame restoreGame;
    private LevelData levelData;
    private GameObject player;
    private GameObject ball;
    private bool hasSavedGame;

    // Ensures that there is only one RestoreGame instancd
    private void Awake() {
        if (restoreGame == null) {
            DontDestroyOnLoad(gameObject);
            restoreGame = this;
            //Debug.Log("Make this the restoreGame");

        } else if (restoreGame != this) {
            Destroy(gameObject);
            //Debug.Log("This is not the restoreGame");
        }
    }

    // Caches data from the LoadGame script
    public void Take(LevelData levelData, GameObject player, GameObject ball) {
        this.levelData = levelData;
        this.player = player;
        this.ball = ball;
        this.hasSavedGame = true;
    }

    // Restores the previous game data
    public void Restore() {
        /*
         * The player and ball references are made here and not in
         * Awake() or Start() since references are lost if
         * a saved game is loaded from a scene different
         * from the saved game's
         */
        player = GameObject.FindWithTag("Player");
        ball = GameObject.FindWithTag("TeleportationBall");

        // Restore player data
        PlayerData playerData = levelData.GetPlayerData();
        player.GetComponent<Rigidbody2D>().velocity = playerData.GetVelocity();
        player.transform.position = playerData.GetPosition();

        // Restore ball data
        BallData ballData = levelData.GetBallData();
        ball.GetComponent<Rigidbody2D>().velocity = ballData.GetVelocity();
        ball.transform.localPosition = ballData.GetPosition();

        // Restore player camera
        player.GetComponent<DetectRoom>().GetCurrentRoom();
    }

    private void OnLevelWasLoaded(int level) {
        if (hasSavedGame) {
            RestoreGame.restoreGame.Restore();
        }
    }
}

