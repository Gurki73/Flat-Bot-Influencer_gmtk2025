using System.Collections.Generic;
using UnityEngine;

namespace Capsule.Core
{
    public static class CapsuleKit
    {
        public struct ModuleRegistry
        {
            public GameObject module;
            public ModulePriority priority;
            public string name;
        }
        public static ModuleRegistry Registry { get; private set; }

    }
}