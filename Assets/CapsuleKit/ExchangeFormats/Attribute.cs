using UnityEngine;
using System;
using System.Linq;

namespace Capsule.Core
{
    [CreateAssetMenu(fileName = "NewAttribute", menuName = "AttributePool/Attribute")]
    public class Attribute : ScriptableObject
    {
        [SerializeField] private string id;
        [SerializeField] private string name;

        public string Id => id;
        public string Name => name;

        public bool IsActive = true;

        [Header("Meta (Optional)")]
        public DateTime CreatedAt;

        [TextArea(2, 5)]
        public string explanation;
        public string LabelNegative = "-1 = ?";  // e.g., "Magical"
        public string LabelNeutral = "±0 = ?";   // e.g., "Realistic"
        public string LabelPositive = "+1 = ?"; // e.g., "Sci-Fi"
        [Range(-1f, 1f)] public float MinValue = -1f;
        [Range(-1f, 1f)] public float MaxValue = 1f;
        public Sprite Icon;

        [HideInInspector] public bool InternalLocked;

        public static Attribute Create(string displayName)
        {
            var instance = ScriptableObject.CreateInstance<Attribute>();
            instance.id = Guid.NewGuid().ToString();     // assign to field
            instance.name = displayName;                 // assign to field
            instance.CreatedAt = DateTime.Now;
            instance.IsActive = true;
            return instance;
        }

    }
}

