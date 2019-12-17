namespace Main._5ive.Messaging.Events {

    public class UnlockedLevelsCountRequestEvent : IEvent {
        public UnlockedLevelsCountRequestEvent() {
            Topic = new Topic("UnlockedLevelsCountRequest");
        }

        public Topic Topic { get; }
    }

}