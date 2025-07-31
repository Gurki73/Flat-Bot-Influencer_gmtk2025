using System.Collections.Generic;
using UnityEngine;

namespace Narrative
{
    public static class NarrativePaths
    {
        public static readonly string[] AllFolders = {
            ARCS_FOLDER,
            CHAPTERS_FOLDER,
            QUESTS_FOLDER,
            STORYBEATS_FOLDER,
            STORYLINES_FOLDER
        };

        public const string ARCS_FOLDER = "Assets/NarrativePool/Arcs/";
        public const string CHAPTERS_FOLDER = "Assets/NarrativePool/Chapters/";
        public const string QUESTS_FOLDER = "Assets/NarrativePool/Quests/";
        public const string STORYBEATS_FOLDER = "Assets/NarrativePool/StoryBeats/";
        public const string STORYLINES_FOLDER = "Assets/NarrativePool/StoryLines/";

#if UNITY_EDITOR
        // Only available in the Editor
        public static void CreateMissingFolders()
        {
            foreach (var folder in AllFolders)
            {
                var cleanPath = folder.TrimEnd('/');
                var parent = System.IO.Path.GetDirectoryName(cleanPath);
                var name = System.IO.Path.GetFileName(cleanPath);

                if (!UnityEditor.AssetDatabase.IsValidFolder(cleanPath))
                {
                    UnityEditor.AssetDatabase.CreateFolder(parent, name);
                }
            }
        }
#endif
    }
}
