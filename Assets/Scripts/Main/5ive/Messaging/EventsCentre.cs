using System.Collections.Generic;

namespace Main._5ive.Messaging {

    public delegate void Callback();

    public class EventsCentre {
        private static EventsCentre _instance;
        private readonly Dictionary<Topic, HashSet<KeyValuePair<ISubscriber, Callback>>> subscribers;

        private EventsCentre() {
            subscribers = new Dictionary<Topic, HashSet<KeyValuePair<ISubscriber, Callback>>>();
        }

        public static EventsCentre GetInstance() {
            return _instance ?? (_instance = new EventsCentre());
        }

        public void Subscribe(Topic topic, ISubscriber subscriber) {
            AddTopicIfNone(topic);
            subscribers[topic].Add(new KeyValuePair<ISubscriber, Callback>(subscriber, null));
        }

        public void Subscribe(Topic topic, ISubscriber subscriber, Callback callback) {
            AddTopicIfNone(topic);
            subscribers[topic].Add(new KeyValuePair<ISubscriber, Callback>(subscriber, callback));
        }

        private void AddTopicIfNone(Topic topic) {
            if (!subscribers.ContainsKey(topic)) {
                subscribers.Add(topic, new HashSet<KeyValuePair<ISubscriber, Callback>>());
            }
        }

        public void Publish(IEvent @event) {
            Topic topic = @event.Topic;
            foreach (var subscriber in subscribers[topic]) {
                Callback callback = subscriber.Value;
                if (callback == null) {
                    ISubscriberDefault subscriberDefault = (ISubscriberDefault) subscriber.Key;
                    subscriberDefault.Notify(@event);
                } else {
                    ISubscriberSlim subscriberSlim = (ISubscriberSlim) subscriber.Key;
                    subscriberSlim.Notify(subscriber.Value);
                }
            }
        }
    }

}