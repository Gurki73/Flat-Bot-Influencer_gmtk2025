using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Capsule.Core
{
    public enum GoalType
    {
        None,             // Start triggers, auto-complete
        LoopCounter,      // Just call ReportProgress("key", +1)
        CurrencyAmount,   // Purse goal
        InventoryAmount,
        ReachArea,
        KillTarget,
        CollectItem,
        // Later:
        CompleteMinigame,
        WaitForTime,
    }
}
