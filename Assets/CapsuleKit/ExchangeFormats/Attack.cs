using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Capsule.Core
{
    [CreateAssetMenu(menuName = "Beastarium/Attack")]
    public class Attack : ScriptableObject
    {
        public string name;
        public string ID;
        public Sprite Icon;
        public string group;
    }
}