using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoopImageSwapper : MonoBehaviour
{
    [System.Serializable]
    public class SwapTarget
    {
        public Image image;
        public bool flipX = true;
        public bool flipY = false;
    }

    [Header("Sprites to Loop Through")]
    public Sprite[] sprites;

    [Header("Target UI Images")]
    public SwapTarget[] targets;

    [Header("Swap Timing")]
    public float minDelay = 0.5f;
    public float maxDelay = 2f;

    private void Start()
    {
        foreach (var target in targets)
        {
            if (target.image != null)
                StartCoroutine(SwapRoutine(target));
        }
    }

    private IEnumerator SwapRoutine(SwapTarget target)
    {
        Image img = target.image;
        RectTransform rt = img.rectTransform;

        while (true)
        {
            // Swap to a random sprite
            if (sprites.Length > 0)
                img.sprite = sprites[Random.Range(0, sprites.Length)];

            // Flip using scale
            Vector3 scale = rt.localScale;
            if (target.flipX) scale.x *= -1f;
            if (target.flipY) scale.y *= -1f;
            rt.localScale = scale;

            float waitTime = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
