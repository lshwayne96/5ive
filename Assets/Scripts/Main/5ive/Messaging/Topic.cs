namespace Main._5ive.Messaging {

    public class Topic {
        public Topic(string description) {
            Description = description;
        }

        public string Description { get; }

        public override bool Equals(object obj) {
            if (this == obj) {
                return true;
            }

            if (obj == null) {
                return false;
            }

            if (!(obj is Topic)) {
                return false;
            }

            Topic topic = (Topic) obj;
            return Description.Equals(topic.Description);
        }

        public override int GetHashCode() {
            return (Description != null ? Description.GetHashCode() : 0);
        }
    }

}