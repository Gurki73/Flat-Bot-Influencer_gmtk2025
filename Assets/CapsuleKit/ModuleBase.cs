using UnityEngine;

namespace Capsule.Core
{
    public abstract class ModuleBase : MonoBehaviour, IGameModule
    {
        [SerializeField] protected string moduleNameOverride = "";
        [SerializeField] protected ModulePriority priority = ModulePriority.Source;

        public virtual string ModuleName => string.IsNullOrWhiteSpace(moduleNameOverride) ? gameObject.name : moduleNameOverride;
        public virtual ModulePriority Priority => priority;

        public abstract void Initialize(int order);

        public virtual void Shutdown()
        {
            Destroy(gameObject);  // Destroy *this* GameObject
        }

        public virtual void OnGamePaused()
        {
            enabled = false;
        }

        public virtual void OnGameResumed()
        {
            enabled = true;
        }
    }
}
