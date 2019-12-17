namespace Main._5ive.Messaging.Events {

    public class LevelSaveEvent : IEvent {
        public LevelSaveEvent() {
            Topic = new Topic("LevelSaveEvent");
            IsSimple = true;
        }

        public Topic Topic { get; }

        public bool IsSimple { get; }
    }

}