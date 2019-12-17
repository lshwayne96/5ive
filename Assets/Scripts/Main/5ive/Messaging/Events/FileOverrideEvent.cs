namespace Main._5ive.Messaging.Events {

    public class FileOverrideEvent : IEvent {
        public FileOverrideEvent(string fileName) {
            Topic = new Topic("FileOverrideEvent");
            FileName = fileName;
        }

        public Topic Topic { get; }

        public string FileName { get; }
    }

}