using System;
using Main._5ive.Commons;
using Main._5ive.Model;
using Main._5ive.Storage;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main._5ive {

    /// <summary>
    /// This script represents the skeleton of the game.
    /// </summary>
    public class Game : MonoBehaviour {
        private GameState gameState;
        private IStorage storage;

        public void Init(IStorage storage) {
            this.storage = storage;
        }

        public void Start() {
            if (storage.DoesGameExist()) {
                gameState = (GameState) storage.FetchGame();
            } else {
                gameState = new GameState();
            }
        }

        public int CompletedLevelCount => gameState.completedLevelCount;

        public void Save() {
            gameState.Save();
            storage.StoreGame(gameState);
        }

        public void New() {
            SceneManager.LoadScene((int) LevelName.Denial);
        }

        public void Restart() {
            SceneManager.LoadScene(gameState.currentLevel);
        }

        public void LoadNextLevel() {
            SceneManager.LoadScene(gameState.currentLevel + 1);
        }

        [Serializable]
        private class GameState : State {
            public int currentLevel;
            public int completedLevelCount;
            public int lastCompletedLevel;
            public int lastSavedLevel;

            public GameState() {
                currentLevel = 0;
                completedLevelCount = 0;
                lastCompletedLevel = 0;
                lastSavedLevel = 0;
            }

            public void Save() {
                lastSavedLevel = SceneManager.GetActiveScene().buildIndex;
            }
        }
    }

}