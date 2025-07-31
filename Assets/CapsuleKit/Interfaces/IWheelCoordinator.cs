using System;
using System.Collections.Generic;
using Wheel;

namespace Capsule.Core
{
    public interface IWheelCoordinator
    {
        void ShowPlayerWheel(List<WheelOption> options, Action<WheelOption> onComplete);
        void ShowAgingWheel(List<WheelOption> states, Action<WheelOption> onComplete);
        void ShowAIWheel(List<WheelOption> phases, Action<WheelOption> onComplete);
    }
}

