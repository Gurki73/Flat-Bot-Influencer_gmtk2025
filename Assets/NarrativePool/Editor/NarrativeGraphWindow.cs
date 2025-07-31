using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

namespace Narrative
{
    public class NarrativeGraphWindow : EditorWindow
    {
        // At class level
        private List<Arc> arcs = new();
        private List<Chapter> chapters = new();
        private List<Storyline> storylines = new();
        private List<StoryBeat> beats = new();

        // Optionally: dictionary lookups
        private Dictionary<string, Arc> arcById = new();
        private Dictionary<string, Chapter> chapterById = new();
        private Dictionary<string, Storyline> storylineById = new();
        private Dictionary<string, StoryBeat> beatById = new();

        private enum ElementType { Arc, Chapter, Quest, Line, Beat }

        private struct NarrativeElement
        {
            public string id;
            public ElementType type;

            public NarrativeElement(string id, ElementType type)
            {
                this.id = id;
                this.type = type;
            }
        }

        private static readonly NarrativeElement[] mandatory = new NarrativeElement[]
        {
            new NarrativeElement("main", ElementType.Arc),

            new NarrativeElement("0", ElementType.Chapter),
            new NarrativeElement("1", ElementType.Chapter),
            new NarrativeElement("999", ElementType.Chapter),

            new NarrativeElement("main_0", ElementType.Line),
            new NarrativeElement("main_1", ElementType.Line),
            new NarrativeElement("main_999", ElementType.Line),

            new NarrativeElement("main_0_start", ElementType.Beat),
            new NarrativeElement("main_0_final", ElementType.Beat),
            new NarrativeElement("main_1_start", ElementType.Beat),
            new NarrativeElement("main_1_final", ElementType.Beat),
            new NarrativeElement("main_999_start", ElementType.Beat),
            new NarrativeElement("main_999_final", ElementType.Beat),
        };

        private void EnsureMandatoryElementsExist()
        {
            foreach (var element in mandatory)
            {
                switch (element.type)
                {
                    case ElementType.Arc:
                        if (AssetExists<Arc>(NarrativePaths.ARCS_FOLDER, element.id))
                        {
                            Debug.Log($"✅ Arc found: {element.id}");
                        }
                        else
                        {
                            Debug.LogWarning($"⚠️ Arc missing: {element.id}, creating...");
                            CreateAsset<Arc>(NarrativePaths.ARCS_FOLDER, element.id, arc =>
                            {
                                arc.arcID = element.id;
                                arc.description = "Main Arc (auto-created)";
                            });
                        }
                        break;

                    case ElementType.Chapter:
                        if (AssetExists<Chapter>(NarrativePaths.CHAPTERS_FOLDER, element.id))
                        {
                            Debug.Log($"✅ Chapter found: {element.id}");
                        }
                        else
                        {
                            Debug.LogWarning($"⚠️ Chapter missing: {element.id}, creating...");
                            CreateAsset<Chapter>(NarrativePaths.CHAPTERS_FOLDER, element.id, chapter =>
                            {
                                chapter.chapterName = $"Chapter {element.id} (auto-created)";
                                chapter.arcs = new List<Arc>();
                            });
                        }
                        break;
                    case ElementType.Line:
                        if (AssetExists<Storyline>(NarrativePaths.STORYLINES_FOLDER, element.id))
                        {
                            Debug.Log($"✅ Storyline found: {element.id}");
                        }
                        else
                        {
                            Debug.LogWarning($"⚠️ Storyline missing: {element.id}, creating...");
                            CreateAsset<Storyline>(NarrativePaths.STORYLINES_FOLDER, element.id, line =>
                            {
                                // Parse ID like "main_0" into arc and chapter
                                var parts = element.id.Split('_');
                                string arcID = parts.Length > 0 ? parts[0] : "main";
                                int chapterIndex = parts.Length > 1 && int.TryParse(parts[1], out int i) ? i : 0;

                                line.storylineID = element.id;
                                line.rootChapterIndex = chapterIndex;
                                line.rootBeatID = element.id + "_start";
                                line.arc = FindAsset<Arc>(arcID);
                                line.beats = new List<StoryBeat>(); // empty for now
                            });
                        }
                        break;
                    case ElementType.Beat:
                        if (AssetExists<StoryBeat>(NarrativePaths.STORYBEATS_FOLDER, element.id))
                        {
                            Debug.Log($"✅ StoryBeat found: {element.id}");
                        }
                        else
                        {
                            Debug.LogWarning($"⚠️ StoryBeat missing: {element.id}, creating...");
                            CreateAsset<StoryBeat>(NarrativePaths.STORYBEATS_FOLDER, element.id, beat =>
                            {
                                beat.beatID = element.id;
                                beat.description = "Auto-created beat";

                                // Extract the storyline ID by removing the "_start" or "_final" suffix
                                string[] parts = element.id.Split('_');
                                if (parts.Length >= 2)
                                {
                                    string storylineID = parts[0] + "_" + parts[1]; // e.g., main_0
                                    var storyline = FindAsset<Storyline>(NarrativePaths.STORYLINES_FOLDER, storylineID);
                                    beat.storyline = storyline;

                                    // Determine index
                                    if (element.id.EndsWith("_start"))
                                    {
                                        beat.index = 0;
                                    }
                                    else if (element.id.EndsWith("_final"))
                                    {
                                        beat.index = (storyline != null && storyline.beats != null)
                                            ? storyline.beats.Count
                                            : 1;
                                    }
                                }
                                else
                                {
                                    Debug.LogWarning($"Unable to parse storyline ID from beat: {element.id}");
                                }
                            });
                        }
                        break;
                }

            }
        }


        private bool AssetExists<T>(string folder, string id) where T : ScriptableObject
        {
            string[] guids = AssetDatabase.FindAssets($"t:{typeof(T).Name}", new[] { folder });
            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var asset = AssetDatabase.LoadAssetAtPath<T>(path);
                if (asset != null && asset.name == id)
                    return true;
            }
            return false;
        }

        private void CreateAsset<T>(string folder, string id, System.Action<T> initializer = null) where T : ScriptableObject
        {
            T asset = ScriptableObject.CreateInstance<T>();
            initializer?.Invoke(asset);
            AssetDatabase.CreateAsset(asset, Path.Combine(folder, $"{id}.asset"));
            AssetDatabase.SaveAssets();
            Debug.Log($"[Narrative] Created {typeof(T).Name} → {id}");
        }

        [MenuItem("Tools/Narrative Graph")]
        public static void ShowWindow()
        {
            var window = GetWindow<NarrativeGraphWindow>("Narrative Graph");
            window.EnsureMandatoryElementsExist();
            window.LoadExistingElements();
        }
        private Vector2 scrollPos;
        private StoryBeat selectedBeat;
        private Editor beatEditor;

        // Dummy data for now
        private string searchFilter = "";
        private string selectedArc = "Main";

        private void OnDisable()
        {
            if (beatEditor != null)
                DestroyImmediate(beatEditor);
        }

        private void OnGUI()
        {
            DrawToolbar();

            EditorGUILayout.BeginHorizontal();

            // Left: Narrative Graph
            EditorGUILayout.BeginVertical(GUILayout.Width(position.width * 0.6f));
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            DrawChapter("Onboarding", Color.white);
            DrawChapter("Gameplay", Color.red);
            DrawChapter("Ending", Color.white);
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();

            // Right: Inspector
            EditorGUILayout.BeginVertical("box", GUILayout.Width(position.width * 0.4f));
            EditorGUILayout.LabelField("Selected Beat", EditorStyles.boldLabel);

            if (selectedBeat != null)
            {
                if (beatEditor == null)
                    beatEditor = Editor.CreateEditor(selectedBeat);

                beatEditor.OnInspectorGUI();
            }
            else
            {
                EditorGUILayout.LabelField("No beat selected.");
            }

            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();
        }
        private void LoadExistingElements()
        {
            // Clear previous data
            arcs.Clear();
            chapters.Clear();
            storylines.Clear();
            beats.Clear();
            arcById.Clear();
            chapterById.Clear();
            storylineById.Clear();
            beatById.Clear();

            foreach (string folderPath in NarrativePaths.AllFolders)
            {
                string[] guids = AssetDatabase.FindAssets("t:ScriptableObject", new[] { folderPath });

                foreach (string guid in guids)
                {
                    string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                    ScriptableObject asset = AssetDatabase.LoadAssetAtPath<ScriptableObject>(assetPath);

                    if (asset == null)
                        continue;

                    switch (asset)
                    {
                        case Arc arc:
                            arcs.Add(arc);
                            arcById[arc.arcID] = arc;
                            break;

                        case Chapter chapter:
                            chapters.Add(chapter);
                            chapterById[chapter.chapterName] = chapter; // or use chapterIndex as key
                            break;

                        case Storyline storyline:
                            storylines.Add(storyline);
                            storylineById[storyline.storylineID] = storyline;
                            break;

                        case StoryBeat beat:
                            beats.Add(beat);
                            beatById[beat.beatID] = beat;
                            break;
                    }
                }
            }

            Debug.Log($"✅ Loaded: {arcs.Count} Arcs, {chapters.Count} Chapters, {storylines.Count} Storylines, {beats.Count} Beats.");
        }

        private void DrawToolbar()
        {
            GUILayout.BeginHorizontal(EditorStyles.toolbar);
            GUILayout.Label("Filter: Arc", GUILayout.Width(70));
            selectedArc = EditorGUILayout.TextField(selectedArc, GUILayout.Width(100));
            GUILayout.FlexibleSpace();
            searchFilter = GUILayout.TextField(searchFilter, GUI.skin.FindStyle("ToolbarSeachTextField"), GUILayout.Width(200));

            if (GUILayout.Button("Rehook", EditorStyles.toolbarButton, GUILayout.Width(60)))
            {
                Debug.Log("Rehook clicked");
            }

            if (GUILayout.Button("Delete ▼", EditorStyles.toolbarButton, GUILayout.Width(80)))
            {
                Debug.Log("Delete clicked");
            }

            GUILayout.EndHorizontal();
        }

        private void DrawChapter(string chapterName, Color arcColor)
        {
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField("▾ Chapter: " + chapterName, EditorStyles.boldLabel);

            EditorGUI.indentLevel++;
            DrawStoryline("MainPath", arcColor);
            if (GUILayout.Button("+ Add Arc", GUILayout.Width(100)))
            {
                Debug.Log("Add Arc in chapter: " + chapterName);
            }
            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(10);
        }

        private void DrawStoryline(string storylineName, Color arcColor)
        {
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField("┌ Storyline: " + storylineName + " (" + arcColor.ToString() + ")", EditorStyles.label);

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("+ Beat", GUILayout.Width(60)))
            {
                Debug.Log("Add Beat to: " + storylineName);
            }
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(4);
            EditorGUILayout.BeginHorizontal();
            DrawLineButton("Line A 💡 (HasItem)");
            DrawLineButton("Line B 🎲 30%");
            DrawLineButton("Line C ❗ Missable");
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.LabelField("└───────────────", GUILayout.Height(5));
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(5);
        }

        private void DrawBeat(string label, Color color, bool showNewLineButton)
        {
            Color original = GUI.color;
            GUI.color = color;

            if (GUILayout.Button("• " + label, GUILayout.Height(25), GUILayout.MinWidth(120)))
            {
                Debug.Log("Selected Beat: " + label);

                selectedBeat = CreateInstance<StoryBeat>();
                selectedBeat.beatID = label;
                selectedBeat.description = "Description for " + label;

                if (beatEditor != null)
                    DestroyImmediate(beatEditor);
                beatEditor = null;

                Repaint();

                EditorUtility.SetDirty(selectedBeat);
                AssetDatabase.SaveAssets();
            }

            GUI.color = original;

            if (showNewLineButton)
            {
                if (GUILayout.Button("+ Line (cond)", GUILayout.Width(100)))
                {
                    Debug.Log("Add conditional line from: " + label);
                }
            }
        }

        private void DrawLineButton(string label)
        {
            if (GUILayout.Button(label, GUILayout.Width(120)))
            {
                Debug.Log("Conditional branch: " + label);
            }
        }

        private static T FindAsset<T>(string id) where T : ScriptableObject
        {
            string[] guids = AssetDatabase.FindAssets($"{id} t:{typeof(T).Name}");
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                T asset = AssetDatabase.LoadAssetAtPath<T>(path);
                if (asset != null && asset.name == id)
                    return asset;
            }
            return null;
        }

        private static T FindAsset<T>(string folderPath, string id) where T : ScriptableObject
        {
            string[] guids = AssetDatabase.FindAssets($"{id} t:{typeof(T).Name}", new[] { folderPath });
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                T asset = AssetDatabase.LoadAssetAtPath<T>(path);
                if (asset != null && asset.name == id)
                    return asset;
            }
            return null;
        }

    }
}