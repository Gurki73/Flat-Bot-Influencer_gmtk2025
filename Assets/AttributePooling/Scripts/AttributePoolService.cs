using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Capsule.Core;

namespace AttributePool
{
    public class AttributePoolService : ModuleBase, IAttributePool
    {
        public string ModuleName => string.IsNullOrWhiteSpace(moduleNameOverride) ? gameObject.name : moduleNameOverride;
        public ModulePriority Priority => priority;

        public override void Initialize(int order)
        {
            Debug.Log($"[Capsule Init] {order}: {ModuleName} ({Priority})");
            // Pool setup logic here
        }

        public List<GameObject> GetElementByAttributes(List<IAttribute> attributes, int count)
        {
            return new List<GameObject>();
        }

    }
}

