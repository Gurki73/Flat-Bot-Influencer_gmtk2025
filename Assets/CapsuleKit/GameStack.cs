using System.Collections.Generic;
using UnityEngine;

namespace Capsule.Core
{
    public class GameStack : MonoBehaviour
    {
        public GameObject pauseScreen;
        public GameObject mainMenu;
        public static GameStack Instance { get; private set; }

        private readonly Stack<IGameplayModule> _stack = new();

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
            module.OnEnter();
        }

        public void Pop()
        {
            Debug.Log("module was poped");
            if (_stack.Count == 0)
            {
                Debug.LogWarning(" Last was popped ");
                return;
            }
            var top = _stack.Pop();
            top.OnExit();

            _stack.PeekOrNull()?.OnEnter();
        }

        public IGameplayModule Current => _stack.Count > 0 ? _stack.Peek() : null;

        public void Clear()
        {
            while (_stack.Count > 0)
            {
                _stack.Pop().OnExit();
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

    public static class StackExtensions
    {
        public static T PeekOrNull<T>(this Stack<T> stack) where T : class
        {
            return stack.Count > 0 ? stack.Peek() : null;
        }
    }
}

