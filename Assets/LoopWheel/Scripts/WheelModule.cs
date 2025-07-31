using System;
using System.Collections.Generic;
using UnityEngine;
using Capsule.Core;
using Capsule.UI;
using Wheel; // Assuming WheelOption lives here

public class WheelModule : GameplayModule_UIBase
{
    private Action<WheelOption> onComplete;
    private List<WheelOption> currentOptions;

    public override void InitializeUI(int order)
    {
        // Optional: UI-specific setup
    }

    public override void OnEnter()
    {
        rootCanvas.enabled = true;
        // Show animation, input activation etc.
    }

    public override void OnExit()
    {
        rootCanvas.enabled = false;
        // Clean up, hide wheel, etc.
    }

    public void Setup(List<WheelOption> options, Action<WheelOption> callback)
    {
        currentOptions = options;
        onComplete = callback;
        // Populate UI from options list
    }
}

