using UnityEngine;

namespace CortexLoop
{
    public class SynapseData : ScriptableObject
    {
        public string fromId;
        public string toId;
        public float strength; // for line width / glow intensity
        public bool isActive;  // highlight when firing
    }
}

