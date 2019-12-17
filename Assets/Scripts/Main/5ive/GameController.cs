using Main._5ive.Messaging;
using Main._5ive.Messaging.Events;
using Main._5ive.Storage;
using UnityEngine;
using UnityEngine.Assertions;

namespace Main._5ive {

    public class GameController : MonoBehaviour, ISubscriberDefault, ISubscriberSlim {
        private Game game;
        private Level level;
        private LevelFiles levelFiles;

        private EventsCentre eventsCentre;

        private void Awake() {
            DontDestroyOnLoad(gameObject);

            game = GetComponent<Game>();
            level = GetComponent<Level>();
            levelFiles = GetComponent<LevelFiles>();

            IStorage storage = new StorageFacade();
            game.Init(storage);
            level.Init(storage);
            levelFiles.Init(storage);

            eventsCentre = EventsCentre.GetInstance();
            eventsCentre.Subscribe(new Topic("GameStart"), this, NewGame);
            eventsCentre.Subscribe(new Topic("GameRestart"), this, RestartGame);
            eventsCentre.Subscribe(new Topic("GameExit"), this, ExitGame);

            eventsCentre.Subscribe(new Topic("LevelSave"), this, SaveLevel);
            eventsCentre.Subscribe(new Topic("LevelEnd"), this, NextLevel);

            eventsCentre.Subscribe(new Topic("FileDelete"), this);
            eventsCentre.Subscribe(new Topic("FileOverride"), this);
            eventsCentre.Subscribe(new Topic("FileLoad"), this);

            eventsCentre.Subscribe(new Topic("UnlockedLevelsCountRequest"), this);
        }

        public void OnApplicationQuit() {
            ExitGame();
        }

        public void Notify(IEvent @event) {
            string topicDescription = @event.Topic.Description;
            switch (topicDescription) {
                case "FileDelete":
                    FileDeleteEvent fileDeleteEvent = (FileDeleteEvent) @event;
                    DeleteLevelFile(fileDeleteEvent.FileName);
                    break;
                case "FileOverride":
                    FileOverrideEvent fileOverrideEvent = (FileOverrideEvent) @event;
                    OverrideLevelFile(fileOverrideEvent.FileName);
                    break;
                case "FileLoad":
                    LevelLoadEvent levelLoadEvent = (LevelLoadEvent) @event;
                    LoadLevelFile(levelLoadEvent.FileName);
                    break;
                case "UnlockedLevelsCountRequest":
                    eventsCentre.Publish(new UnlockedLevelsCountReplyEvent(game.CompletedLevelCount));
                    break;
                default:
                    throw new AssertionException("Error", "Error");
            }
        }

        public void Notify(Callback callback) {
            callback();
        }

        private void NewGame() {
            game.New();
        }

        private void RestartGame() {
            game.Restart();
        }

        private void ExitGame() {
            game.Save();
        }

        private void NextLevel() {
            game.LoadNextLevel();
        }

        private void SaveLevel() {
            level.Save();
        }

        public void RestartLevel() {
            level.Restart();
        }

        private void LoadLevelFile(string levelName) {
            levelFiles.LoadFile(levelName);
        }

        private void DeleteLevelFile(string fileName) {
            levelFiles.DeleteFile(fileName);
        }

        private void OverrideLevelFile(string fileName) {
            levelFiles.OverrideFile(fileName);
        }
    }

}