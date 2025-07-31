using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Capsule.UI;


namespace Capsule.Core
{
    public static class StackExtensions
    {
        public static T PeekOrNull<T>(this Stack<T> stack) where T : class
        {
            return stack.Count > 0 ? stack.Peek() : null;
        }

        public static void SaveStackState()
        {
            Debug.Log("[GameStack] Saving stack state...");

            var ids = GameStack.Instance._stack.Select(module =>
            {
                if (module is GameplayModule_UIBase uiModule)
                {
                    Debug.Log($"[GameStack] Saving module with ID: {uiModule.Id}");
                    return uiModule.Id;
                }
                else
                {
                    Debug.LogWarning("[GameStack] Module without ID or not of type GameplayModule_UIBase.");
                    return "";
                }
            })
            .Where(id => !string.IsNullOrEmpty(id))
            .ToList();

            var json = JsonUtility.ToJson(new StringListWrapper { list = ids });
            PlayerPrefs.SetString(GameStack.PLAYER_PREF_KEY, json);
            PlayerPrefs.Save();

            Debug.Log($"[GameStack] Stack state saved. JSON: {json}");
        }

        public static void LoadStackState()
        {
            Debug.Log("[GameStack] Attempting to load stack state...");

            if (!PlayerPrefs.HasKey(GameStack.PLAYER_PREF_KEY))
            {
                Debug.LogWarning("[GameStack] No saved stack state found.");
                return;
            }

            string json = PlayerPrefs.GetString(GameStack.PLAYER_PREF_KEY);
            Debug.Log($"[GameStack] Loaded JSON from PlayerPrefs: {json}");

            var wrapper = JsonUtility.FromJson<StringListWrapper>(json);

            if (wrapper?.list == null || wrapper.list.Count == 0)
            {
                Debug.LogWarning("[GameStack] Deserialized wrapper is null or empty.");
                return;
            }

            // Step 1: Normalize list (remove consecutive duplicates)
            var normalizedList = new List<string>();
            string last = null;
            foreach (var id in wrapper.list)
            {
                if (id != last)
                {
                    normalizedList.Add(id);
                    last = id;
                }
            }

            // Step 2: Remove "MainMenu" if it's the last (top of stack)
            if (normalizedList.Count > 0 && normalizedList[^1] == "MainMenu")
            {
                Debug.Log("[GameStack] Removing MainMenu from top of stack.");
                normalizedList.RemoveAt(normalizedList.Count - 1);
            }

            // Step 3: Clear current stack
            GameStack.Instance.Clear();
            Debug.Log("[GameStack] Cleared existing stack.");

            // Step 4: Load modules and push them to stack
            foreach (var id in normalizedList)
            {
                var uiModule = CapsuleRoot.Instance
                    .GetAllModules()
                    .OfType<GameplayModule_UIBase>()
                    .FirstOrDefault(m => m.Id == id);

                if (uiModule != null)
                {
                    GameStack.Instance._stack.Push(uiModule);
                    Debug.Log($"[GameStack] Pushed module: {uiModule.Id}");
                }
                else
                {
                    Debug.LogWarning($"[GameStack] Could not find module with ID: {id}");
                }
            }

            // Step 5: Activate top of stack
            var top = GameStack.Instance._stack.PeekOrNull();
            if (top != null)
            {
                GameStack.Instance.Push(top);
                Debug.Log("[GameStack] Stack restored and top module entered.");
            }
            else
            {
                Debug.LogWarning("[GameStack] Stack is empty after loading.");
            }
        }
    }
}
