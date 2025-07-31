using UnityEditor;
using UnityEngine;

namespace CentralBrain
{
    [CustomEditor(typeof(CortexManager))]
    public class CortexManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

#if UNITY_EDITOR
            if (GUILayout.Button("🔄 Load All Weights"))
            {
                var manager = (CortexManager)target;
                manager.LoadAllWeightSets();

                EditorUtility.SetDirty(manager);
                Debug.Log("[Cortex] All weights loaded.");
            }
#endif
        }
    }
}

