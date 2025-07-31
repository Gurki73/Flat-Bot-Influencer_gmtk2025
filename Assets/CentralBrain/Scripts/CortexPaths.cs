#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CentralBrain
{
    public static class CortexPaths
    {
        public const string CORTEX_FOLDER = "Assets/CentralBrain/ScriptableObjects/Cortex/";
        public const string NEURONS_FOLDER = "Assets/CentralBrain/ScriptableObjects/Neurons/";
        public const string PERMUTATIONS_FOLDER = "Assets/CentralBrain/ScriptableObjects/Permutations/";
        public const string STORYBEATS_FOLDER = "Assets/CentralBrain/ScriptableObjects/StoryBeats/";

        public static readonly string[] AllFolders = {
            CORTEX_FOLDER,
            NEURONS_FOLDER,
            PERMUTATIONS_FOLDER,
            STORYBEATS_FOLDER
        };


#if UNITY_EDITOR
        public static void CreateMissingFolders()
        {
            foreach (var folder in AllFolders)
            {
                var cleanPath = folder.TrimEnd('/');
                var parent = System.IO.Path.GetDirectoryName(cleanPath);
                var name = System.IO.Path.GetFileName(cleanPath);
        
                if (!AssetDatabase.IsValidFolder(cleanPath))
                {
                    AssetDatabase.CreateFolder(parent, name);
                }
            }
        }
#endif
    }
}

