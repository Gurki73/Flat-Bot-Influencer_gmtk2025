using UnityEngine;
using Capsule.Core;

namespace Capsule.Input
{
    public class InputVisualProvider : MonoBehaviour, IInputVisualProvider
    {
        public static InputVisualProvider Instance { get; private set; }

        [Header("Current Icon Set")]
        public InputIconSet activeIconSet;

        [Header("Prefab")]
        public GameObject visualPrefab;

        public string ModuleName => "InputVisualProvider";
        public ModulePriority Priority => ModulePriority.Utility;

        public void Initialize(int order)
        {
            Debug.Log($"[Capsule Init] {order}: {ModuleName} ({Priority})");

            if (Instance != null && Instance != this)
            {
                Debug.LogWarning("Multiple InputVisualProviders detected. Destroying duplicate.");
                Destroy(this.gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void Shutdown()
        {
            if (Instance == this)
                Instance = null;
        }

        public void OnGamePaused() { }
        public void OnGameResumed() { }

        public GameObject RequestVisual(Capsule.Core.Inputs input, string optionalKeyLabel = null)
        {
            Sprite icon = GetSpriteForInput(input);
            if (!icon) return null;

            var visual = Instantiate(visualPrefab);
            var renderer = visual.GetComponentInChildren<SpriteRenderer>();
            if (renderer) renderer.sprite = icon;

            var text = visual.GetComponentInChildren<TMPro.TextMeshPro>();
            if (text && !string.IsNullOrEmpty(optionalKeyLabel))
                text.text = optionalKeyLabel;

            return visual;
        }

        private Sprite GetSpriteForInput(Capsule.Core.Inputs input)
        {
            if (activeIconSet == null) return null;

            switch (input)
            {
                case Inputs.Jump: return activeIconSet.jump;
                case Inputs.Interact: return activeIconSet.interact;
                case Inputs.PrimaryAction: return activeIconSet.primaryAction;
                case Inputs.SecondaryAction: return activeIconSet.secondaryAction;

                //  case Inputs.DpadUp:
                //  case Inputs.DpadDown:
                //  case Inputs.DpadLeft:
                //  case Inputs.DpadRight:
                //      return activeIconSet.dpad;

                case Inputs.Pause: return activeIconSet.pause;
                case Inputs.Menu: return activeIconSet.menu;

                default: return activeIconSet.jump;
            }
        }
    }
}
