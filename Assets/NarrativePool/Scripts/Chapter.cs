using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Capsule.Core;

namespace Narrative
{

    [CreateAssetMenu(menuName = "Narrative/Chapter")]
    public class Chapter : ScriptableObject
    {
        public string chapterName;
        public int chapterIndex; // To sort them
        public string description;
        public List<Arc> arcs = new();
    }
}