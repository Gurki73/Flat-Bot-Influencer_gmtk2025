using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Capsule.Core;
using Capsule.Core;

namespace Capsule.Attributes
{
    [Serializable]
    public class AttributeLockState
    {
        public string AttributeId;
        public bool IsLocked;
    }
}