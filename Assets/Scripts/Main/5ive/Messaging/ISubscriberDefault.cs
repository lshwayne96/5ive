namespace Main._5ive.Messaging {

    public interface ISubscriberDefault : ISubscriber {
        void Notify(IEvent @event);
    }

}