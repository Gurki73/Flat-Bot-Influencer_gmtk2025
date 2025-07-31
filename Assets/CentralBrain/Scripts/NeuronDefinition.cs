namespace CentralBrain
{
    public class NeuronDefinition
    {
        public string id;           // "N0"
        public string displayName;  // "Protagonist"
        public string[] attributes; // ["Morality", "Value", "StoryBeat"]

        public NeuronDefinition(string id, string displayName, string[] attributes)
        {
            this.id = id;
            this.displayName = displayName;
            this.attributes = attributes;
        }
    }
}