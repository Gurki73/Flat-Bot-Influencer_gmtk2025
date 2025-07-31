#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Capsule.Core;

namespace Capsule.Attributes
{
[CustomEditor(typeof(AttributeProvider))]
public class AttributeProviderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Auto-Fill Attributes from Folder"))
        {
            var provider = (AttributeProvider)target;
            string[] guids = AssetDatabase.FindAssets("t:Attribute", new[] { "Assets/AttributeProvider/Attributes" });

            var attributes = new List<Attribute>();
            foreach (var guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                var attr = AssetDatabase.LoadAssetAtPath<Attribute>(path);
                if (attr != null)
                    attributes.Add(attr);
            }

            // Optional method to allow controlled injection
            provider.SetAttributes(attributes);

            EditorUtility.SetDirty(provider);
            Debug.Log($"[Capsule] Auto-filled {attributes.Count} attributes.");
        }
    }
}
}
#endif

