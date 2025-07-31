using System.Collections;
using System.Collections.Generic;

namespace Capsule.Core
{
    public enum Inputs
    {
        Jump,               // Cross
        Interact,           // Circle
        PrimaryAction,      // Square
        SecondaryAction,    // Triangle

        NextPage,           // R1
        PreviousPage,       // L1
        NextCategory,       // R2
        PreviousCategory,   // L2

        Zoom,               // R3
        Pan,                // L3

        DpadUp,
        DpadDown,
        DpadLeft,
        DpadRight,          // ← Might be replaced by SwapScheme

        Move,               // Left Stick (Vector2)
        Look,               // Right Stick (Vector2)

        Pause,              // Start / Esc
        Menu,               // Select / F1
    }
}