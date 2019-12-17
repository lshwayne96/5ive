namespace Main._5ive.Messaging.Events {

    public class GameStartEvent : IEvent {
        public GameStartEvent() {
            Topic = new Topic("GameRestart");
        }

        public Topic Topic { get; }

        public override bool Equals(object obj) {
            if (this == obj) {
                return true;
            }

            if (obj == null) {
                return false;
            }

            if (!(obj is GameStartEvent)) {
                return false;
            }

            GameStartEvent @event = (GameStartEvent) obj;
            return Topic.Equals(@event.Topic);
        }

        public override int GetHashCode() {
            return Topic.GetHashCode();
        }
    }

}