using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Animators
{
    public class LoopFlipper : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;

        [Header("Flip Axis")]
        public bool flipX = true;
        public bool flipY = false;

        [Header("Flip Speeds (per second)")]
        public float flipSpeedX = 1f;
        public float flipSpeedY = 0.5f;

        private void Start()
        {
            if (spriteRenderer == null)
                spriteRenderer = GetComponent<SpriteRenderer>();

            StartCoroutine(FlipRoutine());
        }

        private System.Collections.IEnumerator FlipRoutine()
        {
            while (true)
            {
                if (flipX)
                    spriteRenderer.flipX = !spriteRenderer.flipX;

                if (flipY)
                    spriteRenderer.flipY = !spriteRenderer.flipY;

                float waitX = flipX ? 1f / flipSpeedX : float.MaxValue;
                float waitY = flipY ? 1f / flipSpeedY : float.MaxValue;

                float waitTime = Mathf.Min(waitX, waitY);
                yield return new WaitForSeconds(waitTime);
            }
        }
    }

}

