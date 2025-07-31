using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CentralBrain
{
    [CreateAssetMenu(menuName = "Cortex/CortexManager")]
    public class CortexManager : ScriptableObject
    {
        [Header("Loaded Data")]
        public List<CortexWeightSet> cortexWeightSets = new();
        public List<NeuronWeightSet> neuronWeightSets = new();
        public List<PermutationWeightSet> permutationWeightSets = new();
        public List<StoryBeatWeightSet> storyBeatWeightSets = new();

        public string OutputPath = "Assets/CentralBrain/ScriptableObjects/Neurons/";

#if UNITY_EDITOR
        // Add a button for quick generation inside Inspector
        [ContextMenu("Generate Neurons")]
        public void GenerateNeurons()
        {
            foreach (var neuronDef in NeuronRegistry.NeuronAttributeMap)
            {
                var neuron = ScriptableObject.CreateInstance<NeuronWeightSet>();
                neuron.id = neuronDef.id;
                neuron.displayName = neuronDef.displayName;
                neuron.SetInputAttributes( new List<string>(neuronDef.attributes));

                string assetPath = $"{OutputPath}{neuronDef.id}_{neuronDef.displayName}.asset";

                AssetDatabase.CreateAsset(neuron, assetPath);
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log($"✅ Generated {NeuronRegistry.NeuronAttributeMap.Count} Neurons.");
        }

        public void LoadAllWeightSets()
        {
            cortexWeightSets.Clear();
            neuronWeightSets.Clear();
            permutationWeightSets.Clear();
            storyBeatWeightSets.Clear();

            LoadAssetsFromPath<CortexWeightSet>(CortexPaths.CORTEX_FOLDER, cortexWeightSets);
            LoadAssetsFromPath<NeuronWeightSet>(CortexPaths.NEURONS_FOLDER, neuronWeightSets);
            LoadAssetsFromPath<PermutationWeightSet>(CortexPaths.PERMUTATIONS_FOLDER, permutationWeightSets);
            LoadAssetsFromPath<StoryBeatWeightSet>(CortexPaths.STORYBEATS_FOLDER, storyBeatWeightSets);

            Debug.Log($"✅ Loaded CortexManager: " +
                      $"{cortexWeightSets.Count} cortex, " +
                      $"{neuronWeightSets.Count} neurons, " +
                      $"{permutationWeightSets.Count} permutations, " +
                      $"{storyBeatWeightSets.Count} storybeats.");
        }

        private void LoadAssetsFromPath<T>(string path, List<T> targetList) where T : ScriptableObject
        {
            var guids = AssetDatabase.FindAssets($"t:{typeof(T).Name}", new[] { path });
            foreach (var guid in guids)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                var asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
                if (asset != null) targetList.Add(asset);
            }
        }
#endif

    }
}
