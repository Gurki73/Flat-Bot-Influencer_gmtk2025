using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Capsule.Core;

namespace Narrative
{
    [CreateAssetMenu(menuName = "Narrative/Storyline")]
    public class Storyline : ScriptableObject
    {
        public int rootChapterIndex;
        public string rootBeatID;
        public string storylineID;
        public Arc arc;
        public List<StoryBeat> beats = new();
    }

}
