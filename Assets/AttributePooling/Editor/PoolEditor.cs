using UnityEditor;
using UnityEngine;

namespace AttributePool.Editor
{
    public class PoolingEditorWindow : EditorWindow
    {
        private Vector2 leftScroll;
        private Vector2 mainScroll;

        [MenuItem("Tools/Attribute Pool/Editor")]
        public static void ShowWindow()
        {
            GetWindow<PoolingEditorWindow>("Attribute Pool Editor");
        }

        private void OnGUI()
        {
            DrawTopBar();

            EditorGUILayout.BeginHorizontal();

            DrawLeftPanel();
            DrawMainPanel();

            EditorGUILayout.EndHorizontal();

            DrawStatusBar();
        }

        // 🟦 Top Toolbar
        private void DrawTopBar()
        {
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);

            GUILayout.Label("🔍 Search: [placeholder]", GUILayout.Width(150));
            GUILayout.Label("Sort: [dropdown]", GUILayout.Width(100));

            GUILayout.FlexibleSpace();

            if (GUILayout.Button("+ Add Attribute", EditorStyles.toolbarButton))
            {
                Debug.Log("Add Attribute Clicked");
            }

            if (GUILayout.Button("+ Add Pool", EditorStyles.toolbarButton))
            {
                Debug.Log("Add Pool Clicked");
            }

            if (GUILayout.Button("⟳ Refresh", EditorStyles.toolbarButton))
            {
                Debug.Log("Refresh Clicked");
            }

            EditorGUILayout.EndHorizontal();
        }

        // 🟨 Left Sidebar (List of Pools)
        private void DrawLeftPanel()
        {
            EditorGUILayout.BeginVertical(GUILayout.Width(250));
            leftScroll = EditorGUILayout.BeginScrollView(leftScroll);

            GUILayout.Label("🟨 Pool List", EditorStyles.boldLabel);
            GUILayout.Space(10);
            GUILayout.Label("- Pool A");
            GUILayout.Label("- Pool B");
            GUILayout.Label("- Pool C");

            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
        }

        // 🟩 Main Panel (Pool Details)
        private void DrawMainPanel()
        {
            EditorGUILayout.BeginVertical();
            mainScroll = EditorGUILayout.BeginScrollView(mainScroll);

            GUILayout.Label("🟩 Main Panel – Pool Details", EditorStyles.boldLabel);
            GUILayout.Space(10);
            GUILayout.Label("Selected Pool Info Goes Here...");

            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
        }

        // 🟥 Bottom Status Bar
        private void DrawStatusBar()
        {
            EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
            GUILayout.Label("Pools: 0   Attributes: 0   Elements in Pool: 0");
            EditorGUILayout.EndHorizontal();
        }
    }
}
