#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using Capsule.Core;

namespace Beastarium {
    public static class BeastariumAssetCreator
    {
        [MenuItem("Assets/Create/Beastarium/Empty Table")]
        public static void CreateTableAsset()
        {
            var asset = ScriptableObject.CreateInstance<BeastariumTable>();
            string path = AssetDatabase.GenerateUniqueAssetPath("Assets/Data/Tables/NewBeastariumTable.asset");
            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
        }
    }
}
#endif

