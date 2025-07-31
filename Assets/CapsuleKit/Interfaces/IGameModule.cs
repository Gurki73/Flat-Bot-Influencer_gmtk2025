using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Capsule.Core
{
    public interface IGameModule
    {
        string ModuleName { get; }
        ModulePriority Priority { get; }
        void Initialize(int order);
        void Shutdown();       // Optional clean-up
        void OnGamePaused();   // For pause-aware modules
        void OnGameResumed();  // For resume-aware modules
    }
}