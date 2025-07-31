using UnityEngine;

namespace CortexLoop
{

    [CreateAssetMenu(fileName = "NeuronData", menuName = "Cortex/Neuron")]
    public class NeuronData : ScriptableObject
    {
        public string id;
        public string displayName;
        public NeuronState state;
        public float activationValue;
    }
    public enum NeuronState
    {
        Locked,
        Inactive,
        Active
    }

}
