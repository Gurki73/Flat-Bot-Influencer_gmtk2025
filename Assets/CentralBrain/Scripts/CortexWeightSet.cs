using System.Collections.Generic;
using UnityEngine;

namespace CentralBrain
{
    [CreateAssetMenu(menuName = "Cortex/CortexWeightSet")]
    public class CortexWeightSet : WeightBase
    {
        [SerializeField]
        private List<NeuronWeightSet> neuronWeightSets = new();

        public override int WeightCount => neuronWeightSets.Count;

        public override float[] GetWeights()
        {
            var all = new List<float>();
            foreach (var n in neuronWeightSets)
                all.AddRange(n.GetWeights());
            return all.ToArray();
        }

        public NeuronWeightSet GetNeuronSet(int index) =>
            index < neuronWeightSets.Count ? neuronWeightSets[index] : null;
    }
}
