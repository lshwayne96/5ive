using System.Collections.Generic;
using Main._5ive.Storage;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main._5ive {

    public class LevelFiles : MonoBehaviour {
        private IStorage storage;
        private Dictionary<string, int> levelFiles;

        public void Init(IStorage storage) {
            this.storage = storage;
        }

        public void Start() {
            DontDestroyOnLoad(gameObject);
            levelFiles = new Dictionary<string, int>();
        }

        public void LoadFile(string levelFileName) {
            SceneManager.LoadScene(levelFiles[levelFileName]);
        }

        public void OverrideFile(string levelFileName) {
        }

        public void DeleteFile(string levelFileName) {
            levelFiles.Remove(levelFileName);
            storage.DeleteLevelFile(levelFileName);
        }
    }

}