namespace Main._5ive.Messaging.Events {

    public class GameRestartEvent : IEvent {
        public GameRestartEvent() {
            Topic = new Topic("GameRestart");
        }

        public Topic Topic { get; }
    }

}