namespace Main._5ive.Messaging.Events {

    public class GameExitEvent : IEvent {
        public GameExitEvent() {
            Topic = new Topic("GameExit");
        }

        public Topic Topic { get; }
    }

}