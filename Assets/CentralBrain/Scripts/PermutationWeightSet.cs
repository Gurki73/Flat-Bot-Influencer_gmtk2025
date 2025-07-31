using UnityEngine;

namespace CentralBrain
{
    [CreateAssetMenu(menuName = "Cortex/PermutationWeightSet")]
    public class PermutationWeightSet : WeightBase
    {
        [SerializeField]
        private float[] weights = new float[6];

        public override int WeightCount => 6;

        public override float[] GetWeights() => weights;

        public float this[int i]
        {
            get => weights[i];
            set => weights[i] = value;
        }
    }
}


