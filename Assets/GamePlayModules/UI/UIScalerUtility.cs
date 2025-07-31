using UnityEngine;
using UnityEngine.UI;
using Capsule.Core;

namespace Capsule.UI
{
    public static class UIScalerUtility
    {
        public static void ApplyDefaultScaler(Canvas canvas)
        {
            if (!canvas) return;

            var scaler = canvas.GetComponent<CanvasScaler>();
            if (!scaler)
                scaler = canvas.gameObject.AddComponent<CanvasScaler>();

            // 🔧 These are the key values you want set globally
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(960, 650);
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            scaler.matchWidthOrHeight = 0.5f; // 0 = width priority, 1 = height priority
        }
    }
}