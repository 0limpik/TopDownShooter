using TopDown.Scripts.Movement;
using UnityEngine;

namespace TopDown.Scripts
{
    [RequireComponent(typeof(Camera))]
    internal class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform target;

        [SerializeField] private RotationScript rotation;

        [SerializeField] private float smoothTime = 0.3f;
        [SerializeField] private float rotationModifier = 0.1f;

        private Vector3 offset;
        private Vector3 velocity = Vector3.zero;

        private Camera _camera;

        private void Start()
        {
            _camera = this.GetComponent<Camera>();

            this.transform.parent = null;
            offset = this.transform.position - target.position;
        }

        private void LateUpdate()
        {
            var rottationOffset = rotation.LookAt - target.position;

            rottationOffset.x *= rotationModifier;
            rottationOffset.z *= rotationModifier * 1 / _camera.aspect;

            var targetPosition = target.position + offset + rottationOffset;
            this.transform.position = Vector3.SmoothDamp(this.transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}
