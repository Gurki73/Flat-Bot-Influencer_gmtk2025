using System.Collections;
using UnityEngine;

namespace CortexLoop
{
    [RequireComponent(typeof(LineRenderer))]
    public class SynapseVisual : MonoBehaviour
    {
        public LineRenderer lineRenderer;
        public Gradient activeGradient;
        public Gradient idleGradient;

        void Awake()
        {
            if (activeGradient == null)
                activeGradient = CreateGradient(Color.cyan, 1f, 0.2f);

            if (idleGradient == null)
                idleGradient = CreateGradient(Color.gray, 0.5f, 0.1f);
        }

        Gradient CreateGradient(Color color, float startAlpha, float endAlpha)
        {
            Gradient gradient = new Gradient();
            gradient.SetKeys(
                new GradientColorKey[] {
            new GradientColorKey(color, 0f),
            new GradientColorKey(color, 1f)
                },
                new GradientAlphaKey[] {
            new GradientAlphaKey(startAlpha, 0f),
            new GradientAlphaKey(endAlpha, 1f)
                }
            );
            return gradient;
        }

        public void SetData(SynapseData data, Vector3 start, Vector3 end)
        {
            if (lineRenderer == null)
                lineRenderer = GetComponent<LineRenderer>();

            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, end);

            float baseWidth = 0.05f;
            lineRenderer.startWidth = baseWidth * data.strength;
            lineRenderer.endWidth = baseWidth * data.strength;

            lineRenderer.colorGradient = data.isActive ? activeGradient : idleGradient;
        }

        public void Pulse()
        {
            StartCoroutine(PulseRoutine());
        }

        IEnumerator PulseRoutine()
        {
            float duration = 0.3f;
            float elapsed = 0f;
            float intensity = 1.5f;
            while (elapsed < duration)
            {
                float t = elapsed / duration;
                float scale = Mathf.Lerp(1f, intensity, Mathf.PingPong(t * 2f, 1f));
                lineRenderer.widthMultiplier = scale;
                elapsed += Time.deltaTime;
                yield return null;
            }
            lineRenderer.widthMultiplier = 1f;
        }
    }
}