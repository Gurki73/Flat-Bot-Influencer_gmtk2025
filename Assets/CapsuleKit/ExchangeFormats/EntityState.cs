using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Capsule.Core
{
    public enum EntityState
    {
        Inactive,    // Off, disabled, or dormant
        Active,      // Currently processing / visible / participating
        Locked,      // Frozen or not yet available
        Waiting,     // Awaiting input / trigger / Temporarily halted
        Error,       // Failed or invalid state
        Success,     // Completed, correct
        Highlighted, // Special focus or UI interaction
    }
}
