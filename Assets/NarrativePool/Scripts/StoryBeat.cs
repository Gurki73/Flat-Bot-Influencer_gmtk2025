using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Capsule.Core;

namespace Narrative
{
    [CreateAssetMenu(menuName = "Narrative/Story Beat")]
    public class StoryBeat : ScriptableObject
    {
        public string beatID;
        public string description;
        public GoalType goalType;
        public string targetID;
        public float requiredValue;

        public List<string> unlockPoolAttributes;
        public List<string> onBeatTriggers; // names of events, popups, etc.
        public List<string> hintMessages;
        public StoryBeat[] prerequisites; // beats that must be completed

        public bool isCompleted;

        // Internal linkage
        public Storyline storyline;
        public int index;

        // Runtime (not serialized)
        [System.NonSerialized]
        public float currentProgress = 0f;
    }
}