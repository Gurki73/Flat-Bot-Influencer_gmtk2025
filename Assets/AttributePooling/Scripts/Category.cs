using UnityEngine;
using System;

namespace AttributePool
{
    public class Category : ScriptableObject
    {
        public string Id;                     // Unique GUID or Timestamp
        public string DisplayName;
        public string Description;
        public Sprite Icon;

        [HideInInspector] public bool InternalLocked;

        public static Category Create(string displayName)
        {
            var instance = ScriptableObject.CreateInstance<Category>();
            instance.Id = Guid.NewGuid().ToString();
            instance.DisplayName = displayName;
            return instance;
        }

    }
}