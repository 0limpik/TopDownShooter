using UnityEngine;

namespace TopDown.Scripts.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class MovementScript : MonoBehaviour, IMovement
    {
        [field: SerializeField] public float Speed { get; private set; } = 5f;

        public Vector3 Direction { get => _Direction; set => _Direction = value.normalized; }

        public Vector3 Position => this.transform.position;

        private Vector3 _Direction;

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = this.GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            var move = _Direction * Speed;

            _rigidbody.velocity = new Vector3(move.x, _rigidbody.velocity.y, move.z);
        }
    }
}
