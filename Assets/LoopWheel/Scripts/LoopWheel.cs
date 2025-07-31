using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Wheel
{
    public class LoopWheel : MonoBehaviour
    {

        public Sprite[] wheelRingSprites; // Assign in Inspector: index 0 = 2 arrows, 1 = 3 arrows, etc.

        public float spinDuration = 0.4f;
        private bool spinning = false;

        [SerializeField] private Image wheelRingImage;

        public List<RectTransform> icons = new List<RectTransform>();
        public float radius = 100f;
        public bool mirrored = false;

        [Header("Optional Visuals")]
        public Vector3 normalScale = new Vector3(0.6f, 0.6f, 1f);
        public Vector3 highlightScale = new Vector3(1.2f, 1.2f, 1f);

        private int currentIndex = 0;

        void Start()
        {
            SetupWheelSprite();
            PositionIcons();
            HighlightIcon(currentIndex);
        }

        public void SpinToNext()
        {
            if (!spinning)
                StartCoroutine(SpinStepCoroutine(1)); // +1 step
        }
        public void PositionIcons()
        {
            int count = icons.Count;
            float stepAngle = 360f / count;

            for (int i = 0; i < count; i++)
            {
                float angle = i * stepAngle;
                if (mirrored) angle = 360f - angle;

                Vector3 dir = Quaternion.Euler(0, 0, angle) * Vector3.up;
                Vector3 pos = dir * radius;
                icons[i].anchoredPosition = pos;

                // Keep icon upright regardless of wheel rotation
                icons[i].rotation = Quaternion.identity;
            }
        }


        void SetupWheelSprite()
        {
            int Options = icons.Count;
            int spriteIndex = Options - 2; // since you have 2,3,4,5 arrows
            Debug.Log(" icon count ==> " + Options + " / " + spriteIndex);
            if (spriteIndex >= 0 && spriteIndex < wheelRingSprites.Length)
            {
                wheelRingImage.sprite = wheelRingSprites[spriteIndex];
            }
            else
            {
                Debug.LogWarning("No sprite found for this option count");
            }
        }


        public void HighlightIcon(int index)
        {
            for (int i = 0; i < icons.Count; i++)
            {
                icons[i].localScale = (i == index) ? highlightScale : normalScale;
            }
        }

        public void StepForward()
        {
            currentIndex = (currentIndex + 1) % icons.Count;
            HighlightIcon(currentIndex);
        }

        public void StepBackward()
        {
            currentIndex = (currentIndex - 1 + icons.Count) % icons.Count;
            HighlightIcon(currentIndex);
        }

        public void SetRotation(float progress) // 0.0 - 1.0
        {
            float angle = -progress * 360f;
            transform.localRotation = Quaternion.Euler(0, 0, angle);
        }

        private System.Collections.IEnumerator SpinStepCoroutine(int direction)
        {
            spinning = true;

            int count = icons.Count;
            float angleStep = 360f / count;
            float startAngle = 0f;
            float targetAngle = direction * angleStep;
            float time = 0f;

            Transform ring = wheelRingImage.transform;

            while (time < spinDuration)
            {
                time += Time.deltaTime;
                float t = Mathf.Clamp01(time / spinDuration);
                float angle = Mathf.Lerp(startAngle, targetAngle, t);
                ring.localRotation = Quaternion.Euler(0, 0, angle);
                yield return null;
            }

            // Snap back to 0 rotation
            ring.localRotation = Quaternion.identity;

            // Update index and icon positions
            currentIndex = (currentIndex - direction + count) % count;
            HighlightIcon(currentIndex);
            PositionIcons(); // repositions icons so highlight is back at 3 o'clock

            spinning = false;
        }

#if UNITY_EDITOR
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Debug.Log(" wheel right arrow ");
                StepForward();
                StartCoroutine(SpinStepCoroutine(1));
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Debug.Log(" wheel left arrow ");
                StepBackward();
                StartCoroutine(SpinStepCoroutine(-1));
            }
        }
#endif

    }
}
