using UnityEngine;
using UnityEngine.UI;
using Capsule.Core;

namespace Capsule.UI
{
    public abstract class GameplayModule_UIBase : ModuleBase, IGameplayModule
    {
        [SerializeField] protected string id = "";

        public GameObject module;
        public string name;

        public virtual string Id => string.IsNullOrWhiteSpace(id) ? ModuleName : id;

        public abstract void OnEnter();
        public abstract void OnExit();

        public virtual void Tick() { }
        public virtual void OnInput(Inputs input) { }
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

