using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using Capsule.UI;

namespace Capsule.Core
{

    public class GameStack : MonoBehaviour
    {

        public const string PLAYER_PREF_KEY = "GameStackState";
        public GameObject pauseScreen;
        public GameObject mainMenu;
        public static GameStack Instance { get; private set; }

        public readonly Stack<IGameplayModule> _stack = new();

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        void Update()
        {
            Current?.Tick();
        }

        public void OnAnalogInput(Inputs input, Vector2 direction)
        {
        }

        public void Push(IGameplayModule module)
        {
            _stack.PeekOrNull()?.OnExit();
            _stack.Push(module);
            Enter(module);
        }

        private void Enter(IGameplayModule module)
        {
            Debug.Log(" [GameStack] Enter ==> " + module?.GetType().Name);

            var moduleGO = (module as MonoBehaviour)?.gameObject;

            if (moduleGO == null)
            {
                Debug.LogWarning($"[GameStack] ENTER: {module?.GetType().Name}");
                return; // Skip pushing inactive module
            }
            moduleGO.SetActive(true);

            module.OnEnter();
            StackExtensions.SaveStackState();
        }
        public void Pop()
        {
            Debug.Log("module Count before --> " + _stack.Count);
            if (_stack.Count == 0)
            {
                Debug.LogWarning(" no Stack found");
                return;
            }

            var top = _stack.Pop();
            top.OnExit();

            Debug.Log("module Count Afer --> " + _stack.Count);

            Enter(_stack.PeekOrNull());
        }

        public IGameplayModule Current => _stack.Count > 0 ? _stack.Peek() : SearchCurrent();

        private IGameplayModule SearchCurrent()
        {
            // Start from this GameObject’s children
            foreach (Transform child in transform)
            {
                if (!child.gameObject.activeInHierarchy)
                    continue;

                var module = child.GetComponent<IGameplayModule>();
                if (module != null)
                    return module;

                // Optionally, search deeper recursively if needed:
                var foundInChild = SearchInChildren(child);
                if (foundInChild != null)
                    return foundInChild;
            }

            return null;
        }

        // Helper recursive search in children
        private IGameplayModule SearchInChildren(Transform parent)
        {
            foreach (Transform child in parent)
            {
                if (!child.gameObject.activeInHierarchy)
                    continue;

                var module = child.GetComponent<IGameplayModule>();
                if (module != null)
                    return module;

                var deeper = SearchInChildren(child);
                if (deeper != null)
                    return deeper;
            }
            return null;
        }


        public void Clear()
        {
            while (_stack.Count > 0)
            {
                _stack.Pop().OnExit();
                StackExtensions.SaveStackState();
            }
        }

        public void ForwardInput(Inputs input)
        {
            if (input == Inputs.Pause)
            {
                Current?.OnGamePaused();
                Push(pauseScreen.GetComponent<IGameplayModule>());
                return;
            }

            if (input == Inputs.Menu)
            {
                Current?.OnGamePaused();
                Push(mainMenu.GetComponent<IGameplayModule>());
                return;
            }

            Current?.OnInput(input);
        }
    }
}
