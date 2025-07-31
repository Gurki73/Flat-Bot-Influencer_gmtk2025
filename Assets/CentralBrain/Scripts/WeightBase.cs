using UnityEngine;

namespace CentralBrain
{
    public abstract class WeightBase : ScriptableObject
    {
        public string id;           // neuron id or similar identifier
        public string displayName;  // human-readable name
        public abstract int WeightCount { get; }
        public abstract float[] GetWeights();
    }
}

