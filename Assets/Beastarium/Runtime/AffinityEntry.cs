using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Capsule.Core;

namespace Beastarium
{
    [System.Serializable]
    public class AffinityEntry
    {
        public Enemy enemy;
        public Attack attack;
        [Range(-1f, 1f)]
        public float modifier;
    }
}
