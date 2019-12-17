namespace Main._5ive.Messaging.Events {

    public class LevelLoadEvent : IEvent {
        public LevelLoadEvent(string fileName) {
            Topic = new Topic("FileLoad");
            FileName = fileName;
        }

        public Topic Topic { get; }

        public string FileName { get; }
    }

}