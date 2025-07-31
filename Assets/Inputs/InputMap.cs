using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Capsule.Core;

namespace Capsule.Input
{

    public static class InputMap
    {
        public static IInputReceiver Receiver { get; set; }

        public static void Tick()
        {
            if (Receiver == null)
            {
                Debug.Log("no reciver");
                return;
            }
            // face buttons
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
                Receiver.OnInput(Inputs.Jump);

            if (UnityEngine.Input.GetKeyDown(KeyCode.Return))
                Receiver.OnInput(Inputs.Interact);

            if (UnityEngine.Input.GetKeyDown(KeyCode.F))
                Receiver.OnInput(Inputs.PrimaryAction);

            if (UnityEngine.Input.GetKeyDown(KeyCode.E))
                Receiver.OnInput(Inputs.SecondaryAction);


            // Shoulder and Trigger
            if (UnityEngine.Input.GetKeyDown(KeyCode.PageDown))
                Receiver.OnInput(Inputs.NextPage);

            if (UnityEngine.Input.GetKeyDown(KeyCode.PageUp))
                Receiver.OnInput(Inputs.PreviousPage);

            if (UnityEngine.Input.GetKeyDown(KeyCode.End))
                Receiver.OnInput(Inputs.NextCategory);

            if (UnityEngine.Input.GetKeyDown(KeyCode.Home))
                Receiver.OnInput(Inputs.PreviousCategory);

            // Digital buttons
            if (UnityEngine.Input.GetKeyDown(KeyCode.Z))
                Receiver.OnInput(Inputs.Zoom);

            if (UnityEngine.Input.GetKeyDown(KeyCode.LeftShift))
                Receiver.OnInput(Inputs.Pan);

            // Analog simulation with keyboard
            Vector2 moveVector = Vector2.zero;
            Vector2 lookVector = Vector2.zero;

            // WASD for Move
            if (UnityEngine.Input.GetKey(KeyCode.W)) moveVector.y += 1;
            if (UnityEngine.Input.GetKey(KeyCode.S)) moveVector.y -= 1;
            if (UnityEngine.Input.GetKey(KeyCode.A)) moveVector.x -= 1;
            if (UnityEngine.Input.GetKey(KeyCode.D)) moveVector.x += 1;

            // Arrow Keys for Look
            if (UnityEngine.Input.GetKey(KeyCode.UpArrow)) lookVector.y += 1;
            if (UnityEngine.Input.GetKey(KeyCode.DownArrow)) lookVector.y -= 1;
            if (UnityEngine.Input.GetKey(KeyCode.LeftArrow)) lookVector.x -= 1;
            if (UnityEngine.Input.GetKey(KeyCode.RightArrow)) lookVector.x += 1;

            // Send if used
            if (moveVector != Vector2.zero)
                Receiver.OnAnalogInput(Inputs.Move, moveVector.normalized);

            if (lookVector != Vector2.zero)
                Receiver.OnAnalogInput(Inputs.Look, lookVector.normalized);


            if (UnityEngine.Input.GetKeyDown(KeyCode.PageUp))
                Receiver.OnInput(Inputs.PreviousPage);

            // D-Pad (Numpad Simulation)

            if (UnityEngine.Input.GetKeyDown(KeyCode.Keypad6))
                Receiver.OnInput(Inputs.DpadRight);

            if (UnityEngine.Input.GetKeyDown(KeyCode.Keypad4))
                Receiver.OnInput(Inputs.DpadLeft);

            if (UnityEngine.Input.GetKeyDown(KeyCode.Keypad8))
                Receiver.OnInput(Inputs.DpadUp);

            if (UnityEngine.Input.GetKeyDown(KeyCode.Keypad2))
                Receiver.OnInput(Inputs.DpadDown);

            // System
            if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
                Receiver.OnInput(Inputs.Pause);

            if (UnityEngine.Input.GetKeyDown(KeyCode.F1))
                Receiver.OnInput(Inputs.Menu);

        }
    }

}