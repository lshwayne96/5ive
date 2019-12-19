using UnityEngine;

namespace Main._5ive.Messaging.Events {

    public class RoomChangeEvent : IEvent {
        public RoomChangeEvent(Vector3 position) {
            Topic = new Topic("RoomChange");
            Position = position;
        }

        public Topic Topic { get; }

        public Vector3 Position { get; }
    }

}