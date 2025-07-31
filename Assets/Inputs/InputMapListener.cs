using UnityEngine;
using Capsule.Core;

namespace Capsule.Input
{
    public class InputMapListener : MonoBehaviour, IInputReceiver
    {
        public string ModuleName => "InputMap";
        public ModulePriority Priority => ModulePriority.Source;

        public void Initialize(int order)
        {
            Debug.Log($"[Capsule Init] {order}: {ModuleName} ({Priority})");
            InputMap.Receiver = this;
        }

        public void Shutdown() { }

        public void OnGamePaused() { }
        public void OnGameResumed() { }

        public void OnInput(Inputs input)
        {
            GameStack.Instance?.ForwardInput(input);
        }

        public void OnAnalogInput(Inputs input, Vector2 direction)
        {
            GameStack.Instance?.Current?.OnAnalogInput(input, direction);
        }


        void Update()
        {
            InputMap.Tick(); // Poll keys
        }
    }
}