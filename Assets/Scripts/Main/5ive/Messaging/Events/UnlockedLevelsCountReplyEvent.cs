namespace Main._5ive.Messaging.Events {

    public class UnlockedLevelsCountReplyEvent : IEvent{
        public UnlockedLevelsCountReplyEvent(int count) {
            Topic = new Topic("UnlockedLevelsCountReply");
            Count = count;
        }

        public Topic Topic { get; }

        public int Count { get; }
    }

}