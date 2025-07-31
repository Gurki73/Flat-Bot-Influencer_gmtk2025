using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Capsule.Core;

namespace Capsule.Attributes
{
    public class AttributeProvider : ModuleBase
    {
        public static AttributeProvider Instance { get; private set; }

        [SerializeField] private List<Attribute> allAttributes;

        public IReadOnlyList<Attribute> AllAttributes => allAttributes;

        public override void Initialize(int order)
        {
            Debug.Log($"[Capsule Init] {order}: {ModuleName} ({Priority})");
            if (Instance != null && Instance != this)
            {
                Debug.LogWarning($"Multiple {nameof(AttributeProvider)} instances detected. Destroying duplicate.");
                Destroy(gameObject);
                return;
            }
            Instance = this;

            AttributeLockManager.RegisterAttributes(allAttributes);
        }

        public override void Shutdown()
        {
            base.Shutdown();
            if (Instance == this)
                Instance = null;
        }

#if UNITY_EDITOR
public void SetAttributes(List<Attribute> attributes)
{
    allAttributes = attributes;
}
#endif

    }
}