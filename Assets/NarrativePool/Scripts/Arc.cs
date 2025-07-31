using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Capsule.Core;

namespace Narrative
{
    [CreateAssetMenu(menuName = "Narrative/Arc")]
    public class Arc : ScriptableObject
    {
        public string arcID;
        public string description;
        public List<Storyline> storylines;
    }
}
