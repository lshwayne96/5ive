namespace Main._5ive.Messaging.Events {

    public class FileDeleteEvent : IEvent {
        public FileDeleteEvent(string fileName) {
            Topic = new Topic("FileDeleteEvent");
            FileName = fileName;
        }

        public Topic Topic { get; }

        public string FileName { get; }

    }

}