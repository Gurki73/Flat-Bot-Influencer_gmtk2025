using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Capsule.Core
{
    public interface IGameplayModule : IGameModule
    {
        string Id { get; }
        void OnEnter();
        void OnExit();
        void Tick(); // Optional: for manual updates
        void OnInput(Inputs input); // Core may forward raw or mapped input
        void OnAnalogInput(Inputs input, Vector2 direction);
    }
}