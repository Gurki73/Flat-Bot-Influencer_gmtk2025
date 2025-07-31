using UnityEngine;

namespace Capsule.Core
{
    public abstract class GameplayModule_WorldBase : ModuleBase, IGameplayModule
    {
        [SerializeField] protected string id = "";

        public virtual string Id => string.IsNullOrWhiteSpace(id) ? ModuleName : id;

        public override void Initialize(int order)
        {
            InitializeWorld();
        }

        public virtual void OnEnter() { }
        public virtual void OnExit() { }
        public virtual void Tick() { }
        public virtual void OnInput(Inputs input) { }

        protected virtual void InitializeWorld() { }
        public virtual void OnAnalogInput(Inputs input, Vector2 direction)
        {

        }
    }
}
