using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using Capsule.Core;

namespace Narrative
{
    [CustomEditor(typeof(StoryBeatManager))]
    public class StoryBeatManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("🔄 Load All Storylines & Beats"))
            {
                var manager = (StoryBeatManager)target;
                manager.LoadStorylinesAndBeats();

                EditorUtility.SetDirty(manager);
                Debug.Log($"[Narrative] Loaded {manager.AllBeatsCount} beats from storylines.");
            }
        }
    }
}