#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Capsule.Core;

namespace Narrative
{
    public class StoryBeatManager : ModuleBase, INarrativePool
    {
        private bool onlyOnce = true;

        [SerializeField]
        private GameObject startMenuObject;
        private IGameplayModule startMenu;

        public static StoryBeatManager Instance { get; private set; }

        [SerializeField] private List<StoryBeat> allBeats = new();
        private readonly Dictionary<string, float> progressMap = new(); // progress per target

        public override void Initialize(int order)
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            if (startMenuObject != null)
            {
                startMenu = startMenuObject.GetComponent<IGameplayModule>();
                if (startMenu == null)
                    Debug.LogWarning("⚠️ startMenuObject does not implement IGameplayModule!");
            }


            Instance = this;
            Debug.Log($"[Capsule Init] {order}: {ModuleName} ({Priority})");

            foreach (StoryBeat beat in allBeats)
            {
                Debug.Log("[BeatManager] beats " + beat.name);
            }
        }
        private void Update()
        {
            foreach (var beat in allBeats)
            {
                if (!beat.isCompleted && CanTrigger(beat))
                {
                    TriggerBeat(beat);
                }
            }
        }

        private bool CanTrigger(StoryBeat beat)
        {
            foreach (var prereq in beat.prerequisites)
            {
                if (!prereq || !prereq.isCompleted) return false;
            }

            if (beat.goalType == GoalType.None) return true;

            progressMap.TryGetValue(beat.targetID, out float value);
            return value >= beat.requiredValue;
        }

        private void TriggerBeat(StoryBeat beat)
        {
            if (beat.onBeatTriggers != null && beat.onBeatTriggers.Contains("pushMainMenu") && onlyOnce)
            {
                onlyOnce = false;
                StartCoroutine(DelayedPushMainMenu());
                progressMap.Remove(beat.targetID);
                return;
            }

            beat.isCompleted = true;
            Debug.Log($"✅✅✅✅✅ [BeatManager] Beat triggered: {beat.beatID}");

            ApplyEffects(beat);
            progressMap.Remove(beat.targetID);
        }

        private IEnumerator DelayedPushMainMenu()
        {
            yield return null; // wait for next frame
            GameStack.Instance.Push(startMenu);
        }

        private void ApplyEffects(StoryBeat beat)
        {
            foreach (var tag in beat.unlockPoolAttributes)
            {
                Debug.Log($"🔓🔓🔓🔓 [BeatManager] Unlocked: {tag}");
                // PoolRegistry.Instance.UnlockTag(tag); // ← plug in actual logic
            }
        }

        // 📥 External modules call this to report progress (kills, currency etc.)
        public void ReportProgress(string targetID, float amount)
        {
            progressMap.TryGetValue(targetID, out float current);
            progressMap[targetID] = current + amount;
        }

        public void RegisterBeat(StoryBeat beat)
        {
            if (!allBeats.Contains(beat))
            {
                allBeats.Add(beat);
                if (beat.goalType == GoalType.None && CanTrigger(beat))
                {
                    TriggerBeat(beat);
                }
            }
        }

        // 🔍 Query Methods
        public bool IsBeatCompleted(string beatID) =>
            allBeats.Exists(b => b.beatID == beatID && b.isCompleted);

        public bool HasUnlockedTag(string tag)
        {
            foreach (var beat in allBeats)
            {
                if (beat.isCompleted && beat.unlockPoolAttributes.Contains(tag))
                    return true;
            }
            return false;
        }

        // 🔻 Standard Lifecycle
        public override void Shutdown()
        {
            if (Instance == this) Instance = null;
            base.Shutdown();
            Debug.Log("[BeatManager] StoryBeatManager shutdown.");
        }

        public override void OnGamePaused() => base.OnGamePaused();
        public override void OnGameResumed() => base.OnGameResumed();

        public void LoadStorylinesAndBeats()
        {
#if UNITY_EDITOR
            allBeats.Clear();

            string pathLine = "Assets/NarrativePool/StoryLines";
            string[] guids = AssetDatabase.FindAssets("t:Storyline", new[] { pathLine });

            foreach (var guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                var storyline = AssetDatabase.LoadAssetAtPath<Storyline>(path);

                if (storyline != null)
                {
                    int assigned = 0;
                    string[] beatGuids = AssetDatabase.FindAssets("t:StoryBeat", new[] { "Assets/NarrativePool/StoryBeats" });

                    foreach (var beatGuid in beatGuids)
                    {
                        string beatPath = AssetDatabase.GUIDToAssetPath(beatGuid);
                        var beat = AssetDatabase.LoadAssetAtPath<StoryBeat>(beatPath);

                        // 💥 PROBLEM MAY BE HERE
                        if (beat != null && beat.storyline == storyline)
                        {
                            storyline.beats.Add(beat);
                            allBeats.Add(beat);
                            assigned++;
                        }
                    }

                    Debug.Log($"✅ {storyline.storylineID}: Assigned {assigned} beats.");
                }
            }


            Debug.Log($"✅ Loaded {allBeats.Count} beats.");
#endif
        }

        public int AllBeatsCount => allBeats.Count;

    }
}