using UnityEngine;
namespace Animators
{
    public class LoopRotator : MonoBehaviour
    {
        [Header("Rotation Settings")]
        public float speed = 90f; // degrees per second
        public bool clockwise = true;
        public Vector3 rotationAxis = Vector3.forward; // Z-axis for 2D, Y-axis for 3D

        void Update()
        {
            float direction = clockwise ? -1f : 1f;
            transform.Rotate(rotationAxis * speed * direction * Time.deltaTime);
        }
    }
}