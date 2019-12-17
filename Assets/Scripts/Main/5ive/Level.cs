using System;
using Boo.Lang;
using Main._5ive.Model;
using Main._5ive.Model.Collectible;
using Main._5ive.Storage;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main._5ive {

    /// <summary>
    /// This script represents a level.
    /// </summary>
    /// <remarks>
    /// Internally, a level corresponds to a scene.
    /// </remarks>
    public class Level : MonoBehaviour {
        private LevelState levelState;
        private IStorage storage;
        private Collectible collectible;

        /// <summary>
        /// Represents all the objects in the level that extend <see cref="PersistentObject"/>.
        /// </summary>
        /// <remarks>
        /// These objects are serialised when the user saves the level
        /// and deserialised when the level is loaded.
        /// </remarks>
        private PersistentObject[] persistentObjects;

        public void Init(IStorage storage) {
            this.storage = storage;
        }

        public void Start() {
            int sceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
            levelState = new LevelState(sceneBuildIndex);
            collectible = GetComponent<Collectible>();
            persistentObjects = GetComponentsInChildren<PersistentObject>();
        }

        public void CollectCollectible() {
            levelState.isCollectibleCollected = true;
            Destroy(collectible.gameObject);
        }

        public void Restart() {
            SceneManager.LoadScene(levelState.sceneBuildIndex);
        }

        public void Save() {
            var objectStates = new List<State>();
            foreach (PersistentObject o in persistentObjects) {
                objectStates.Add(o.Save());
            }
            levelState.persistentObjectStates = objectStates.ToArray();
            storage.StoreLevel(levelState, levelState.fileName);
        }

        public void RestoreWith(State state) {
            levelState = (LevelState) state;
            if (levelState.isCollectibleCollected) {
                Destroy(collectible);
            }
            for (int i = 0; i < persistentObjects.Length; i++) {
                persistentObjects[i].RestoreWith(levelState.persistentObjectStates[i]);
            }
        }

        [Serializable]
        private class LevelState : State {
            public string fileName;
            public bool hasBeenSaved;
            public int sceneBuildIndex;

            public bool isCollectibleCollected;
            public State[] persistentObjectStates;

            public LevelState(int sceneBuildIndex) {
                hasBeenSaved = false;
                this.sceneBuildIndex = sceneBuildIndex;
                isCollectibleCollected = false;
            }
        }
    }

}