using UnityEngine;

namespace Capsule.Input
{
    [CreateAssetMenu(menuName = "ScriptableObject/Input Icon Set")]
    public class InputIconSet : ScriptableObject
    {
        public InputDeviceStyle device;

        [Header("Face Buttons")]
        public Sprite jump;              // Cross / A / space
        public Sprite interact;          // Circle / B / enter
        public Sprite primaryAction;     // Square / X / f
        public Sprite secondaryAction;   // Triangle / Y / e

        [Header("Shoulders & Triggers")]
        public Sprite nextPage;          // R1, pg_down
        public Sprite previousPage;      // L1, pg_ip
        public Sprite nextCategory;      // R2, end
        public Sprite previousCategory;  // L2, pos_1 home

        [Header("Sticks & Navigation")]
        public Sprite zoom;              // R3 / z
        public Sprite pan;               // L3 / shift
        public Sprite move;              // Left Stick / asdw
        public Sprite look;              // Right Stick / arrow_keys

        [Header("D-Pad (Single Sprite + Rotation)")]
        public Sprite dPadBase;          // One image, rotated in code / Numpad

        [Header("System")]
        public Sprite pause;             // Start / Esc
        public Sprite menu;              // Select / F1
    }
}



