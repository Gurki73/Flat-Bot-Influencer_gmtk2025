using UnityEngine;
using System.Collections;

namespace CameraCrew
{
    public class CameraMover : MonoBehaviour
    {
        [Header("Fade Settings")]
        public CanvasGroup fadeCanvasGroup;
        public float fadeDuration = 0.5f;

        [Header("Camera Settings")]
        public Transform[] waypoints; // Set these in the Inspector (size 3)
        public float moveDuration = 1.0f;

        private bool isMoving = false;

        public void MoveToWaypoint(int index)
        {
            if (index < 0 || index >= waypoints.Length || isMoving)
            {
                Debug.LogWarning("[CameraMover] Invalid waypoint index or already moving.");
                return;
            }

            StartCoroutine(MoveCameraRoutine(waypoints[index].position));
        }

        public IEnumerator MoveCameraRoutine(Vector3 targetPosition)
        {
            isMoving = true;

            // Fade out
            yield return StartCoroutine(Fade(1));

            // Move camera
            Vector3 startPos = transform.position;
            float t = 0;
            while (t < moveDuration)
            {
                t += Time.deltaTime;
                transform.position = Vector3.Lerp(startPos, targetPosition, t / moveDuration);
                yield return null;
            }
            transform.position = targetPosition;

            // Fade in
            yield return StartCoroutine(Fade(0));

            isMoving = false;
        }

        private IEnumerator Fade(float targetAlpha)
        {
            float startAlpha = fadeCanvasGroup.alpha;
            float t = 0;
            while (t < fadeDuration)
            {
                t += Time.deltaTime;
                fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, t / fadeDuration);
                yield return null;
            }
            fadeCanvasGroup.alpha = targetAlpha;
        }
    }
}
