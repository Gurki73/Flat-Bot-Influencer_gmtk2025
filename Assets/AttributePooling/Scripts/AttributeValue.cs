using System;
using System.Collections.Generic;
using UnityEngine;

namespace AttributePool
{
    [Serializable]
    public class AttributeValue
    {
        [Range(-1f, 1f)]
        public float Value;

        public string Name;

        public AttributeValue(string name, float value)
        {
            Name = name;
            Value = Mathf.Clamp(value, -1f, 1f); // Optional safety
        }
    }
}
