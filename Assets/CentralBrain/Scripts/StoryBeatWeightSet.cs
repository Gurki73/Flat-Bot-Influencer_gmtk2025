using System.Collections.Generic;
using UnityEngine;

namespace CentralBrain
{
    [CreateAssetMenu(menuName = "Cortex/StoryBeatWeightSet")]
    public class StoryBeatWeightSet : WeightBase
    {
        [SerializeField]
        private List<PermutationWeightSet> beatWeights = new();

        public override int WeightCount => beatWeights.Count;

        public override float[] GetWeights()
        {
            var all = new List<float>();
            foreach (var p in beatWeights)
                all.AddRange(p.GetWeights());
            return all.ToArray();
        }

        public PermutationWeightSet GetPermutationSet(StoryBeatType beat)
        {
            int index = (int)beat;
            return index < beatWeights.Count ? beatWeights[index] : null;
        }
    }
}
