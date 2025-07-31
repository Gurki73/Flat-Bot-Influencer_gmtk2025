using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Capsule.Core;

namespace Capsule.CharacterVisuals
{
    public class CharacterFaceAnimator : MonoBehaviour, ICharacterPresentation
    {
        [Header("Parts")]
        [SerializeField] private Image eyeLeftImage;
        [SerializeField] private Image eyeRightImage;
        [SerializeField] private Sprite eyeDefault;
        [SerializeField] private Sprite eyeClosed;

        [SerializeField] private RectTransform noseTransform;
        [SerializeField] private RectTransform headTransform;

        [Header("Blink Settings")]
        [SerializeField] private float blinkIntervalMin = 2f;
        [SerializeField] private float blinkIntervalMax = 5f;
        [SerializeField] private float blinkDuration = 0.15f;

        [Header("Nose Wiggle Settings")]
        [SerializeField] private float noseWiggleAngle = 5f;
        [SerializeField] private float noseWiggleSpeed = 3f;
        [SerializeField] private float noseWiggleDuration = 0.6f;
        [SerializeField] private float noseWiggleDelayMin = 4f;
        [SerializeField] private float noseWiggleDelayMax = 8f;

        [Header("Head Tilt Settings")]
        [SerializeField] private float headTiltAngle = 3f;
        [SerializeField] private float headTiltDuration = 0.5f;
        [SerializeField] private float headTiltDelayMin = 3f;
        [SerializeField] private float headTiltDelayMax = 7f;

        private void OnEnable()
        {
            StartCoroutine(BlinkLoop());
            StartCoroutine(NoseWiggleLoop());
            StartCoroutine(HeadTiltLoop());
        }

        // Interface stuff
        public Transform Root { get; }                  // Main anchor, e.g., for camera or effects
        public Transform FaceAnchor { get; }            // For UI (speech bubble, emotion icons)

        public void SetExpression(EmotionExpression type) { }     // Changes mouth/eyes/etc.
        public void Talk(bool isTalking) { }               // Mouth move, subtle head bob
        public void SetFacing(bool isLeft) { }            // Flip for consistent dialog side

        #region Blink

        private IEnumerator BlinkLoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(blinkIntervalMin, blinkIntervalMax));
                yield return StartCoroutine(Blink());
            }
        }

        private IEnumerator Blink()
        {
            eyeRightImage.sprite = eyeClosed;
            eyeLeftImage.sprite = eyeClosed;
            yield return new WaitForSeconds(blinkDuration);
            eyeRightImage.sprite = eyeDefault;
            eyeLeftImage.sprite = eyeDefault;
        }

        #endregion

        #region Nose Wiggle

        private IEnumerator NoseWiggleLoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(noseWiggleDelayMin, noseWiggleDelayMax));
                yield return StartCoroutine(NoseWiggleRoutine());
            }
        }

        private IEnumerator NoseWiggleRoutine()
        {
            float time = 0f;
            while (time < noseWiggleDuration)
            {
                float angle = Mathf.Sin(time * noseWiggleSpeed * Mathf.PI * 2f) * noseWiggleAngle;
                if (noseTransform != null)
                    noseTransform.localRotation = Quaternion.Euler(0, 0, angle);

                time += Time.deltaTime;
                yield return null;
            }

            if (noseTransform != null)
                noseTransform.localRotation = Quaternion.identity;
        }

        #endregion

        #region Head Tilt

        private IEnumerator HeadTiltLoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(headTiltDelayMin, headTiltDelayMax));
                yield return StartCoroutine(HeadTiltRoutine());
            }
        }

        private IEnumerator HeadTiltRoutine()
        {
            float halfDur = headTiltDuration / 2f;
            float t = 0f;
            float direction = Random.value < 0.5f ? -1f : 1f;

            while (t < halfDur)
            {
                float angle = Mathf.Lerp(0f, direction * headTiltAngle, t / halfDur);
                if (headTransform != null)
                    headTransform.localRotation = Quaternion.Euler(0, 0, angle);

                t += Time.deltaTime;
                yield return null;
            }

            t = 0f;
            while (t < halfDur)
            {
                float angle = Mathf.Lerp(direction * headTiltAngle, 0f, t / halfDur);
                if (headTransform != null)
                    headTransform.localRotation = Quaternion.Euler(0, 0, angle);

                t += Time.deltaTime;
                yield return null;
            }

            if (headTransform != null)
                headTransform.localRotation = Quaternion.identity;
        }

        #endregion
    }
}
