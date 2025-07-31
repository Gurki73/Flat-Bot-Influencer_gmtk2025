using System.Collections.Generic;

namespace CentralBrain
{
    public static class NeuronRegistry
    {
        public static readonly List<NeuronDefinition> NeuronAttributeMap = new()
        {
            new NeuronDefinition("N0", "Protagonist", new[] { "Morality", "Value", "StoryBeat" }),
            new NeuronDefinition("N1", "Antagonist", new[] { "Morality", "Mood", "Energy" }),
            new NeuronDefinition("N2", "Ally", new[] { "Morality", "Persistence", "WorldNature" }),
            new NeuronDefinition("N3", "PrimaryLocation", new[] { "Value", "WorldNature", "StoryBeat" }),
            new NeuronDefinition("N4", "SecondaryLocation", new[] { "Value", "Mood", "Energy" }),
            new NeuronDefinition("N5", "Intention", new[] { "Morality", "Persistence", "Value" }),
            new NeuronDefinition("N6", "Action", new[] { "Energy", "Mood", "StoryBeat" }),
            new NeuronDefinition("N7", "Decision", new[] { "Value", "Persistence", "Morality" }),
            new NeuronDefinition("N8", "MacGuffin", new[] { "Value", "WorldNature", "Persistence" }),
            new NeuronDefinition("N9", "Tension", new[] { "Morality", "Mood", "Persistence" }),
            new NeuronDefinition("N10", "Object", new[] { "Value", "Energy", "WorldNature" }),
            new NeuronDefinition("N11", "EnvironmentalEffect", new[] { "Mood", "Energy", "StoryBeat" }),
        };
    }
}

