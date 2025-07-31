using UnityEngine;
using System.Collections.Generic;

namespace CentralBrain
{
    [CreateAssetMenu(fileName = "CortexData", menuName = "CentralBrain/CortexData")]
    public class CortexData : ScriptableObject
    {
        private Dictionary<string, float> weights = new();

        // Helper to construct a unique key from multiple parameters
        private string BuildKey(string neuronID, StoryBeatType beat, int permutationIndex)
        {
            return $"{neuronID}_{beat}_p{permutationIndex}";
        }

        // 3-parameter weight getter
        public float GetWeight(string neuronID, StoryBeatType beat, int permutationIndex)
        {
            string key = BuildKey(neuronID, beat, permutationIndex);
            return weights.TryGetValue(key, out float value) ? value : 0f;
        }

        // 3-parameter weight modifier
        public void ModifyWeight(string neuronID, StoryBeatType beat, int permutationIndex, float delta)
        {
            string key = BuildKey(neuronID, beat, permutationIndex);
            if (!weights.ContainsKey(key))
                weights[key] = 0f;
            weights[key] += delta;
        }
    }
}
