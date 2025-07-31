using UnityEngine;
using System.Collections;

public class CartLoopMovement : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 2f;
    public float rotationDuration = 0.3f;

    private int currentIndex = 0;
    private bool isTurning = false;

    void Update()
    {
        if (waypoints.Length == 0 || isTurning) return;

        Transform target = waypoints[currentIndex];
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) < 0.05f)
        {
            currentIndex = (currentIndex + 1) % waypoints.Length;
            StartCoroutine(RotateCart());
        }
    }

    IEnumerator RotateCart()
    {
        isTurning = true;

        float elapsed = 0f;
        float startRotation = transform.eulerAngles.z;
        float targetRotation = startRotation + 90f;

        while (elapsed < rotationDuration)
        {
            float zRotation = Mathf.Lerp(startRotation, targetRotation, elapsed / rotationDuration);
            transform.eulerAngles = new Vector3(0f, 0f, zRotation);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.eulerAngles = new Vector3(0f, 0f, targetRotation % 360f);
        isTurning = false;
    }
}



