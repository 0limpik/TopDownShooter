using System;
using System.Threading.Tasks;
using TopDown.Scripts.Enemy.Behaviors.Common;
using TopDown.Scripts.Extensions;
using TopDown.Scripts.Unit;
using TopDown.Scripts.Weapon;
using UnityEngine;
using UnityEngine.AI;

namespace TopDown.Scripts.Enemy.Behaviors.Movement
{
    [Serializable]
    internal class BullertIgnoreBehaviour : UnitBehaviour
    {
        private WeaponScript weapon;

        private BulletPredictor lastBullet;

        [SerializeField] private bool enabled = true;

        public void Init(IUnit unit, WeaponScript weapon)
        {
            base.Init(unit);
            this.weapon = weapon;

            foreach (var bullet in BulletRepository.Bullets)
            {
                OnAdd(bullet);
            }

            BulletRepository.OnAdd += OnAdd;
            BulletRepository.OnRemove += OnRemove;
        }

        protected override Task OnRun()
        {
            return base.OnRun();
        }

        protected override void OnStop() { }

        private void OnRemove(BulletScript bullet)
        {
            if (lastBullet?.Bullet == bullet)
            {
                lastBullet = null;
                futurePosition = Vector3.zero;
                closest = Vector3.zero;
                deltaFuture = Vector3.zero;
            }
        }

        private void OnAdd(BulletScript bullet)
        {
            if (bullet.weapon != weapon)
            {
                lastBullet = new BulletPredictor(bullet);
            }
        }

        private Vector3 futurePosition;
        private Vector3 closest;
        private Vector3 deltaFuture;

        public override void Update(float deltaTime)
        {
            if (!enabled)
                return;

            if (lastBullet == null)
                return;

            lastBullet.Update(deltaTime);

            var unitPosition = unit.Movement.Position;

            var bulletPosition = lastBullet.Bullet.transform.position;

            var delta = bulletPosition - unitPosition;

            var size = unit.Collider.bounds.size.x;

            var time = size / unit.Movement.Speed;

            if (delta.magnitude < time * lastBullet.Bullet.speed)
            {
                if (!running)
                {
                    Run().Forget();
                }

                futurePosition = lastBullet.GetFuturePosition(time);

                deltaFuture = bulletPosition + (bulletPosition - futurePosition) / 2;
                deltaFuture.y -= 1;

                var plane = new Plane(futurePosition, bulletPosition, deltaFuture);

                closest = plane.ClosestPointOnPlane(unitPosition);

                var safe = unitPosition - closest;

                if (NavMesh.SamplePosition(safe, out NavMeshHit hit, time, NavMesh.AllAreas))
                {
                    unit.Movement.Direction = unitPosition - closest;
                }
            }
            else
            {
                Stop();
            }
        }

        protected override void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawSphere(futurePosition, 0.25f);
            Gizmos.DrawSphere(closest, 0.25f);
        }

        private class BulletPredictor : IFuturePosition
        {
            public BulletScript Bullet { get; private set; }

            private PredictionPositionBehaviour predictor;

            public BulletPredictor(BulletScript bullet)
            {
                this.Bullet = bullet;
                predictor = new PredictionPositionBehaviour();
                predictor.Init(() => bullet.transform.position);
            }

            public void Update(float deltaTime)
            {
                predictor.Update(deltaTime);
            }

            public Vector3 GetFuturePosition(float time) => predictor.GetFuturePosition(time);

            public Vector3 GetFuturePosition(GetTime GetTime) => predictor.GetFuturePosition(GetTime);
        }
    }
}
