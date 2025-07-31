using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace Narrative
{
    [CustomEditor(typeof(Storyline))]
    public class StorylineEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("🔄 Auto-Fill Beats from Folder"))
            {
                var storyline = (Storyline)target;

                // You may want to clear old beats first or merge intelligently
                storyline.beats = new List<StoryBeat>();

                string folderPath = "Assets/NarrativePool/StoryBeats"; // adjust to your project structure
                string[] guids = AssetDatabase.FindAssets("t:StoryBeat", new[] { folderPath });

                int assigned = 0;
                foreach (var guid in guids)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guid);
                    var beat = AssetDatabase.LoadAssetAtPath<StoryBeat>(path);

                    if (beat != null && beat.storyline != null && beat.storyline.arc == storyline.arc)
                    {
                        beat.storyline = storyline; // link it
                        storyline.beats.Add(beat);
                        assigned++;
                        EditorUtility.SetDirty(beat); // mark changed beat
                    }

                }

                EditorUtility.SetDirty(storyline);
                Debug.Log($"[StorylineEditor] Assigned {assigned} beats to '{storyline.name}'.");
            }
        }
    }
}
