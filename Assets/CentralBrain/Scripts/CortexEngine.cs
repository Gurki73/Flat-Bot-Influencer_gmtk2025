using UnityEngine;
using Capsule.Core;

namespace CentralBrain
{
    public class CortexEngine : ModuleBase
    {
        public static CortexEngine Instance { get; private set; }

        [SerializeField] private CortexData cortexData; // Your ScriptableObject container

        public override void Initialize(int order)
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            Debug.Log($"[Cortex Init] {order}: {ModuleName} ({Priority})");
        }

        public override void Shutdown()
        {
            if (Instance == this)
                Instance = null;

            base.Shutdown();
            Debug.Log("[Cortex] CortexEngine shutdown.");
        }

        // Optional: weight access
        public float GetWeight(string neuronId, StoryBeatType beat, int permutationIndex)
        {
            // Hook into your ScriptableObject structure here
            return cortexData.GetWeight(neuronId, beat, permutationIndex);
        }

        public void ApplyNeuroplasticityFeedback(string neuronId, StoryBeatType beat, int permutationIndex, float delta)
        {
            // Example of modifying weights directly
            cortexData.ModifyWeight(neuronId, beat, permutationIndex, delta);
        }

        public void ProcessBeat(StoryBeatType beat)
        {
            // Calculate activations, apply feedback, etc.
        }
    }
}

