using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Capsule.Core
{
    public enum ModulePriority
    {
        Sink = 1,         // UI, feedback, output
        Utility = 2,      // Helpers, validators, converters
        Processor = 3,    // Main logic transformers, gameplay rules
        Source = 4,       // Inputs, level data, runtime generators
        Bridge = 5,       // Cross-domain connectors (e.g., Save, Sync, Net)
        Bootstrap = 6     // Critical startup systems
    }

    public class CapsuleRoot : MonoBehaviour
    {
        public static CapsuleRoot Instance { get; private set; }

        private readonly List<IGameModule> _modules = new();

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            RegisterAllModulesInChildren();
            InitializeModulesByPriority();
            CheckMandatoryModules();
        }
        private void CheckMandatoryModules()
        {
            try
            {
                Capsule.Attributes.AttributeLockManager.Ping();
            }
            catch (System.Exception)
            {
                Debug.LogWarning("[Capsule] AttributeManager was not initialized! " +
                                 "You may be missed import Attributtes Package");
            }
        }


        private void RegisterAllModulesInChildren()
        {
            var modules = GetComponentsInChildren<MonoBehaviour>(true)
                          .OfType<IGameModule>();

            Debug.Log(" +++ found modules in Hierarchy +++");
            Debug.Log("");

            foreach (var module in modules)
            {
                _modules.Add(module);

                Debug.Log($"[Capsule] Registered module: {module.ModuleName}");
            }
        }

        private void InitializeModulesByPriority()
        {
            Debug.Log(" +++ start Initialicing modules +++");
            Debug.Log("");
            int order = 0;
            foreach (var module in _modules.OrderByDescending(m => m.Priority))
            {
                module.Initialize(order);
                order++;
            }
        }

        public void ShutdownModules()
        {
            foreach (var module in _modules)
            {
                module.Shutdown();
            }
        }

        public T GetModule<T>() where T : class, IGameModule
        {
            foreach (var m in _modules)
                if (m is T t)
                    return t;

            return null;
        }

        public IEnumerable<IGameModule> GetAllModules() => _modules;
    }
}
