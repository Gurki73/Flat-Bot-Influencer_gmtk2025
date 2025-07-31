using UnityEngine;

namespace Capsule.Core
{
    public interface ICharacterPresentation
    {
        Transform Root { get; }                  // Main anchor, e.g., for camera or effects
        Transform FaceAnchor { get; }            // For UI (speech bubble, emotion icons)

        void SetExpression(EmotionExpression type);     // Changes mouth/eyes/etc.
        void Talk(bool isTalking);               // Mouth move, subtle head bob
        void SetFacing(bool isLeft);             // Flip for consistent dialog side
    }
}
