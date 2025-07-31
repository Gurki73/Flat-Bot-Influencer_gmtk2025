using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldTintManager : MonoBehaviour
{
    [System.Serializable]
    public class TintTarget
    {
        public Material material;
        public Color dayColor = Color.white;
        public Color nightColor = new Color(0.2f, 0.3f, 0.6f, 1f);
    }

    [Header("Tinted Materials")]
    public List<TintTarget> tintTargets;

    [Header("Durations (in seconds)")]
    public float dayDuration = 10f;
    public float nightDuration = 8f;
    public float transitionDuration = 2f;

    public int currentDay = 0;
    private Coroutine loopCoroutine;

    void Start()
    {
        // Ensure all day and night colors are fully opaque
        foreach (var target in tintTargets)
        {
            target.dayColor.a = 1f;
            target.nightColor.a = 1f;

            if (target.material != null)
            {
                // Also enforce current material alpha
                Color c = target.material.color;
                c.a = 1f;
                target.material.color = c;
            }
        }

        loopCoroutine = StartCoroutine(DayNightLoop());
    }

    IEnumerator DayNightLoop()
    {
        while (true)
        {
            // Switch to Day
            yield return StartCoroutine(TintAll(true));
            yield return new WaitForSeconds(dayDuration);

            // Switch to Night
            yield return StartCoroutine(TintAll(false));
            yield return new WaitForSeconds(nightDuration);

            currentDay++;
            Debug.Log($"🌙 Day {currentDay} complete.");
        }
    }

    IEnumerator TintAll(bool toDay)
    {
        float time = 0f;
        List<Color> startColors = new List<Color>();
        List<Color> targetColors = new List<Color>();

        foreach (var target in tintTargets)
        {
            if (target.material == null)
            {
                startColors.Add(Color.white);
                targetColors.Add(Color.white);
                continue;
            }

            Color current = target.material.color;
            Color targetColor = toDay ? target.dayColor : target.nightColor;

            startColors.Add(current);
            targetColors.Add(targetColor);
        }

        while (time < transitionDuration)
        {
            for (int i = 0; i < tintTargets.Count; i++)
            {
                var mat = tintTargets[i].material;
                if (mat != null)
                {
                    mat.color = Color.Lerp(startColors[i], targetColors[i], time / transitionDuration);
                }
            }

            time += Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < tintTargets.Count; i++)
        {
            var mat = tintTargets[i].material;
            if (mat != null)
                mat.color = targetColors[i];
        }
    }

    public void ResetCycle()
    {
        if (loopCoroutine != null)
            StopCoroutine(loopCoroutine);
        loopCoroutine = StartCoroutine(DayNightLoop());
    }
}
