namespace Main._5ive.Messaging.Events {

    public class LevelNextEvent : IEvent {
        public LevelNextEvent() {
            Topic = new Topic("LevelNext");
            IsSimple = true;
        }

        public Topic Topic { get; }

        public bool IsSimple { get; }

    }

}