#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using Capsule.Core;

namespace GlobalPurse
{
    [CustomEditor(typeof(TargetCenter))]
    public class TargetCenterEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Auto-Fill Currencies from Folder"))
            {
                var center = (TargetCenter)target;
                var currencySet = center.GetType().GetField("currencySet", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(center) as CurrencySet;

                if (currencySet == null)
                {
                    Debug.LogWarning("No CurrencySet assigned.");
                    return;
                }

                string[] guids = AssetDatabase.FindAssets("t:Currency", new[] { "Assets/GlobalPurse/Currencies" });
                var found = new List<Currency>();
                foreach (var guid in guids)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guid);
                    var asset = AssetDatabase.LoadAssetAtPath<Currency>(path);
                    if (asset != null)
                        found.Add(asset);
                }

                currencySet.currencies = found;
                EditorUtility.SetDirty(currencySet);
                Debug.Log($"[TargetCenter] Found and assigned {found.Count} currencies.");
            }
        }
    }
}
#endif
