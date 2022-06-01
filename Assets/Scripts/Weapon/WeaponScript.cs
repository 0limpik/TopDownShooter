using System;
using System.Collections;
using TopDown.Scripts.Unit;
using UnityEngine;

namespace TopDown.Scripts.Weapon
{
    internal class WeaponScript : MonoBehaviour
    {
        public event Action<BulletScript> OnShoot;

        [SerializeField] private UnitScript unit;

        [SerializeField] private float delay = 0.5f;
        [field: SerializeField] public float BulletSpeed { get; private set; } = 10f;
        [SerializeField] private BulletScript bulletPrefab;
        [SerializeField] private Transform bulletSpawner;
        public Vector3 BulletSpawner => bulletSpawner.transform.position;

        private float lastShootTime = float.NegativeInfinity;

        private bool isShoot;

        private BulletScript last;

        public bool CanShoot => !isShoot && _CanShoot;
        private bool _CanShoot => Time.time > lastShootTime + delay;

        void Awake()
        {
            StartCoroutine(nameof(ShootCorutine));
        }

        public void Shoot()
        {
            isShoot = true;
        }

        private IEnumerator ShootCorutine()
        {
            while (true)
            {
                yield return new WaitForEndOfFrame();
                if (isShoot && _CanShoot)
                {
                    lastShootTime = Time.time;

                    last = Instantiate(bulletPrefab, bulletSpawner.position, bulletSpawner.rotation);
                    Physics.IgnoreCollision(unit.Collider, last.GetComponent<Collider>(), true);
                    last.speed = BulletSpeed;
                    last.weapon = this;
                    OnShoot?.Invoke(last);
                }
                isShoot = false;
            }
        }
    }
}
