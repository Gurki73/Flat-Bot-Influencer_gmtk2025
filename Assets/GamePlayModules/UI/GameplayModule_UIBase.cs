using UnityEngine;
using UnityEngine.UI;
using Capsule.Core;

namespace Capsule.UI
{
    public abstract class GameplayModule_UIBase : ModuleBase
    {
        [SerializeField] protected string id = "";

        public enum SpeakerType { AI, Producer, Both }

        [SerializeField] private SpeakerType _speakerType = SpeakerType.Both;
        public virtual SpeakerType speakerType => _speakerType;

        public GameObject module;
        public string name;

        public virtual string Id => string.IsNullOrWhiteSpace(id) ? ModuleName : id;

        public abstract void OnEnter();
        public abstract void OnExit();

        public virtual void Tick() { }
        public void OnInput(Inputs input)
        {
            if (input == Inputs.SecondaryAction) Skip();  // Use '==' for comparison, not '='
            else OnInput_UI(input);
        }

        public virtual void OnInput_UI(Inputs input) { }
        public virtual void Skip() { }

        [SerializeField] protected Canvas rootCanvas;

        public override void Initialize(int order)
        {
            if (rootCanvas != null)
                UIScalerUtility.ApplyDefaultScaler(rootCanvas);

            InitializeUI(order); // Call individual setup
        }

        // Optional override in children
        public abstract void InitializeUI(int order);

        void Shutdown()
        {

        }
        void OnGamePaused()
        {

        }
        void OnGameResumed()
        {

        }
        public virtual void OnAnalogInput(Inputs input, Vector2 direction) { }
    }
}

