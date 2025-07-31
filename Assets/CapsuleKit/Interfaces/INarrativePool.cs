using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Capsule.Core
{
    public interface INarrativePool : IGameModule
    {
        bool IsBeatCompleted(string beatID);
        bool HasUnlockedTag(string tag);
    }
}

