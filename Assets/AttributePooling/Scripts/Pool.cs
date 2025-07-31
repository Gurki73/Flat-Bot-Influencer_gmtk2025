using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AttributePool
{
    [CreateAssetMenu(menuName = "AttributePool/Pool")]
    public class Pool : ScriptableObject
    {
        public string PoolId;
        public string PoolName;
        public string Category; // e.g. "Potion", "Background", "Villain"

        public List<string> AttributeKeys;
        public List<Element> Elements;
        public bool CanMatchRequest(Dictionary<string, float> request)
        {
            return request.Keys.All(key => AttributeKeys.Contains(key));
        }
    }
}

