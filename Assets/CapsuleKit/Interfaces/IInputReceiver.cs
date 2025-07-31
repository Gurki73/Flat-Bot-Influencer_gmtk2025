using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Capsule.Core
{
    public interface IInputReceiver : IGameModule
    {
        void OnInput(Inputs input);
        void OnAnalogInput(Inputs input, Vector2 direction); // for Move, Look
    }
}