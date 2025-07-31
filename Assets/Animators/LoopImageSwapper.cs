using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Animators
{
    public class LoopImageSpriteSwapper : MonoBehaviour
    {
        [Header("Sprites to Choose From")]
        public Sprite[] spritePool;

        [Header("UI Image Targets")]
        public Image[] imageSlots;

        [Header("Random Swap Timing (seconds)")]
        public float minDelay = 0.5f;
        public float maxDelay = 2f;

        private void Start()
        {
            foreach (var img in imageSlots)
            {
                if (img != null)
                    StartCoroutine(SwapSpriteRoutine(img));
            }
        }

        private IEnumerator SwapSpriteRoutine(Image img)
        {
            while (true)
            {
                if (spritePool.Length > 0)
                {
                    img.sprite = spritePool[Random.Range(0, spritePool.Length)];
                }

                float wait = Random.Range(minDelay, maxDelay);
                yield return new WaitForSeconds(wait);
            }
        }
    }
}