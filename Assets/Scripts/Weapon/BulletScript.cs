using System;
using System.Collections;
using UnityEngine;

namespace TopDown.Scripts.Weapon
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    internal class BulletScript : MonoBehaviour
    {
        public event Action<GameObject> OnCollision;

        public event Action<BulletScript, GameObject> OnRicochet;

        public Collider Collider { get; private set; }
        private Rigidbody _rigidbody;

        [HideInInspector] public float speed;
        [SerializeField] public float lifeTime = 10f;

        [HideInInspector] public WeaponScript weapon;

        void Awake()
        {
            Collider = this.GetComponent<Collider>();
            _rigidbody = this.GetComponent<Rigidbody>();

            StartCoroutine(nameof(DeathCorutine));
        }

        void Start()
        {
            BulletRepository.Add(this);
        }

        //void FixedUpdate()
        //{
        //    var move = _rigidbody.rotation * Vector3.forward * speed;

        //    var righbodyForward = _rigidbody.rotation * Vector3.forward;

        //    if (Physics.Raycast(_rigidbody.position, righbodyForward, out RaycastHit wallHit, speed * Time.fixedDeltaTime))
        //    {
        //        var forward = new Vector2(righbodyForward.x, righbodyForward.z) * -1;
        //        var normal = new Vector2(wallHit.normal.x, wallHit.normal.z);

        //        var angle = Vector2.SignedAngle(forward, normal);

        //        _rigidbody.rotation *= Quaternion.AngleAxis(180 - angle * 2, Vector3.up);

        //        move = _rigidbody.rotation * Vector3.forward * speed;
        //    }

        //    _rigidbody.velocity = new Vector3(move.x, _rigidbody.velocity.y, move.z);
        //}

        private IEnumerator DeathCorutine()
        {
            yield return new WaitForSeconds(lifeTime);
            Destroy(this.gameObject);
        }

        //private RaycastHit? hit;
        void FixedUpdate()
        {
            var move = this.transform.forward * speed;

            _rigidbody.velocity = new Vector3(move.x, _rigidbody.velocity.y, move.z);

            //var righbodyForward = _rigidbody.rotation * Vector3.forward;

            //if (hit != null)
            //{
            //    var forward = new Vector2(righbodyForward.x, righbodyForward.z) * -1;
            //    var normal = new Vector2(hit.Value.normal.x, hit.Value.normal.z);

            //    var angle = Vector2.SignedAngle(forward, normal);

            //    _rigidbody.rotation *= Quaternion.AngleAxis(180 - angle * 2, Vector3.up);

            //    OnRicochet?.Invoke(this, hit.Value.collider.gameObject);

            //    hit = null;
            //    return;
            //}

            //if (Physics.Raycast(_rigidbody.position, righbodyForward, out RaycastHit wallHit, speed * Time.fixedDeltaTime * 3))
            //{
            //    hit = wallHit;
            //}
        }

        void OnCollisionEnter(Collision collision)
        {
            OnCollision?.Invoke(collision.gameObject);

            var righbodyForward = _rigidbody.rotation * Vector3.forward;
            var forward = new Vector2(righbodyForward.x, righbodyForward.z) * -1;
            var normal = new Vector2(collision.contacts[0].normal.x, collision.contacts[0].normal.z);

            var angle = Vector2.SignedAngle(forward, normal);

            _rigidbody.rotation *= Quaternion.AngleAxis(180 - angle * 2, Vector3.up);

            OnRicochet?.Invoke(this, collision.gameObject);
        }

        void OnDestroy()
        {
            BulletRepository.Remove(this);
        }
    }
}
