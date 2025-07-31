using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Rendering;

namespace CortexLoop
{
    public class NeuronVisual : MonoBehaviour
    {
        public string id;
        public TextMeshPro label;
        public SpriteRenderer background;

        public Color activeBg = Color.white;
        public Color activeText = Color.magenta;

        public Color inactiveBg = Color.gray;
        public Color inactiveText = Color.white;

        public Color lockedBg = Color.black;
        public Color lockedText = Color.clear;

        public void SetData(NeuronData data)
        {
            label.text = data.displayName;

            switch (data.state)
            {
                case NeuronState.Active:
                    SetStyle(activeBg, activeText, 1f);
                    break;

                case NeuronState.Inactive:
                    SetStyle(inactiveBg, inactiveText, 0.3f);
                    break;

                case NeuronState.Locked:
                    SetStyle(lockedBg, lockedText, 0f);
                    break;
            }
        }

        void SetStyle(Color bg, Color text, float opacity)
        {
            background.color = new Color(bg.r, bg.g, bg.b, opacity);
            label.color = new Color(text.r, text.g, text.b, opacity);
        }

        public void Highlight()
        {
            // You could pulse the scale, color, glow etc.
            // Example:
            StartCoroutine(PulseHighlight());
        }

        IEnumerator PulseHighlight()
        {
            Vector3 start = transform.localScale;
            Vector3 end = start * 1.2f;
            float t = 0f;

            while (t < 1f)
            {
                transform.localScale = Vector3.Lerp(start, end, Mathf.PingPong(t * 2, 1));
                t += Time.deltaTime;
                yield return null;
            }

            transform.localScale = start;
        }
    }
}
