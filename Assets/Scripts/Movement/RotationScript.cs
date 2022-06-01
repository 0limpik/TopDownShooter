using UnityEngine;

namespace TopDown.Scripts.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class RotationScript : MonoBehaviour, IRotation
    {
        public Vector3 LookAt { get; set; }

        public Quaternion Quaternion => this.transform.rotation;

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = this.GetComponent<Rigidbody>();
            LookAt = this.transform.position + this.transform.forward;
        }

        void FixedUpdate()
        {
            var relativePos = LookAt - this.transform.position;

            if (relativePos != Vector3.zero)
            {
                var euler = Quaternion.LookRotation(relativePos, Vector3.up).eulerAngles;
                _rigidbody.rotation = Quaternion.Euler(
                    new Vector3(this.transform.eulerAngles.x, euler.y, this.transform.eulerAngles.z));
            }
        }
    }
}
