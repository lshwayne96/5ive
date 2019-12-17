namespace Main._5ive.Messaging {

    public interface ISubscriberSlim : ISubscriber {
        void Notify(Callback callback);
    }

}