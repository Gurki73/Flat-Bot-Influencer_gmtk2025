using System;
using System.Collections.Generic;
using UnityEngine;
using Capsule.Core;

namespace Wheel
{
    public class WheelCoordinator : ModuleBase, IWheelCoordinator
    {
        [SerializeField] private GameObject wheelPrefab;

        public override void Initialize(int order)
        {
            Debug.Log($"[Init] {ModuleName} (order {order})");
        }

        public void ShowPlayerWheel(List<WheelOption> options, Action<WheelOption> onComplete)
        {
            SpawnAndPushWheel(options, onComplete);
        }

        public void ShowAgingWheel(List<WheelOption> states, Action<WheelOption> onComplete)
        {
            SpawnAndPushWheel(states, onComplete);
        }

        public void ShowAIWheel(List<WheelOption> phases, Action<WheelOption> onComplete)
        {
            SpawnAndPushWheel(phases, onComplete);
        }

        private void SpawnAndPushWheel(List<WheelOption> options, Action<WheelOption> callback)
        {
            //  GameObject instance = Instantiate(wheelPrefab);
            //  WheelModule wheel = instance.GetComponent<WheelModule>();
            //  wheel.Setup(options, callback);
            //  GameStack.Instance.Push(wheel);
        }
    }
}

