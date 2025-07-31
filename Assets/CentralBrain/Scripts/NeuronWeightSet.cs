using System.Collections.Generic;
using UnityEngine;

namespace CentralBrain
{
    [CreateAssetMenu(menuName = "Cortex/NeuronWeightSet")]
    public class NeuronWeightSet : WeightBase
    {
        [SerializeField]
        private List<StoryBeatWeightSet> storyBeatSets = new();

        // Private backing field
        [SerializeField]
        private List<string> inputAttributes = new List<string>();

        // Property for controlled access
        public IReadOnlyList<string> InputAttributes => inputAttributes.AsReadOnly();

        // Setter method to replace entire list
        public void SetInputAttributes(List<string> attributes)
        {
            inputAttributes = new List<string>(attributes);
        }

        // Or add single attribute method
        public void AddAttribute(string attribute)
        {
            inputAttributes.Add(attribute);
        }

        public override int WeightCount => storyBeatSets.Count;

        public override float[] GetWeights()
        {
            var all = new List<float>();
            foreach (var sb in storyBeatSets)
                all.AddRange(sb.GetWeights());
            return all.ToArray();
        }

        public StoryBeatWeightSet GetStoryBeatSet(int index) =>
            index < storyBeatSets.Count ? storyBeatSets[index] : null;
    }
}
