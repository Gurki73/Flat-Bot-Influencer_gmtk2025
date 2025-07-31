using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Capsule.Core
{
    [CreateAssetMenu(menuName = "Beastarium/Enemy")]
    public class Enemy : ScriptableObject
    {
        public string name;
        public string ID;
        public Sprite Icon;
        public string group;
    }
}