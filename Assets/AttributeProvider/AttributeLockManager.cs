using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Capsule.Core;

namespace Capsule.Attributes
{
    public static class AttributeLockManager
    {
        private static bool isInitialized = false;
        public static bool IsInitialized => isInitialized;

        private static Dictionary<string, Attribute> attributes = new();

        public static void RegisterAttributes(IEnumerable<Attribute> attrs)
        {
            attributes.Clear();
            foreach (var attr in attrs)
            {
                attributes[attr.Id] = attr;
                Debug.Log("Attribute in LockMannager registered " + attr.name);
            }
            isInitialized = true;
        }

        public static void Ping()
        {
            Debug.Log("hello, AttributeLockManager ready to unlock Attributes");
        }

        public static Attribute Get(string id)
        {
            if (!isInitialized)
                Debug.LogError($"[AttributeManager] Attempted to access attribute '{id}' before initialization.");
            return attributes.TryGetValue(id, out var attr) ? attr : null;
        }

    }
}
