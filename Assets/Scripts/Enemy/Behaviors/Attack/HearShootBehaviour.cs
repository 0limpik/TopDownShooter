using System;
using System.Threading.Tasks;
using TopDown.Scripts.Attributes;
using TopDown.Scripts.Enemy.Behaviors.Common;
using TopDown.Scripts.Enemy.Behaviors.Movement;
using TopDown.Scripts.Extensions;
using TopDown.Scripts.Unit;
using TopDown.Scripts.Visibility;
using TopDown.Scripts.Weapon;
using UnityEditor;
using UnityEngine;

namespace TopDown.Scripts.Enemy.Behaviors.Attack
{
    [Serializable]
    [Info("Стреляет из оружие в центр цели, учитывая ее будущюю позицию")]
    [Info("Если видно - пытается поразить рикошетом")]
    [Info("Если цель слышно, но не видно - пытается поразить рикошетом")]
    internal class HearShootBehaviour : UnitBehaviour, IAttackBehaviour
    {
        public IAttack Attack => attackCache;
        private AttackCache attackCache;

        [Tooltip("Gizmos -> Arc Blue")]
        [SerializeField] public float hearRadius = 8f;

        private IUnit target;
        private WeaponScript weapon;
        private IFuturePosition move;
        private IFuturePosition targetMove;

        private Vector3? rikoshetPoint;
        private Vector3? futurePoint;
        private Vector3 prediction;

        private Vector3 centerDelta;

        private Vector3 delta => target.Movement.Position - unit.Movement.Position;

        public void Init(IUnit unit, IUnit target, WeaponScript weapon,
            IFuturePosition move, IFuturePosition targetMove)
        {
            base.Init(unit);
            this.weapon = weapon;
            this.target = target;
            this.move = move;
            this.targetMove = targetMove;

            centerDelta = target.Movement.Position - target.Center;

            attackCache = new AttackCache(this);
        }

        protected override Task OnRun()
        {
            return base.OnRun();
        }

        protected override void OnStop()
        {

        }

        public override void Update(float deltaTime)
        {
            if (delta.magnitude < hearRadius)
                unit.Rotation.LookAt = target.Movement.Position;

            if (!weapon.CanShoot)
                return;

            if (TryShoot())
                return;

            if (TryRikoshet())
                return;
        }

        public bool CanAttack()
        {
            if (delta.magnitude < hearRadius)
                return attackCache.CanAttack = true;
            else
                return attackCache.CanAttack = false;
        }

        private bool TryShoot()
        {
            if (UnitVisibility.Instance.IsVisible(unit, target))
            {
                var futurePosition = GetFuturePosition();

                if (UnitVisibility.Instance.IsVisible(unit.Movement.Position, futurePosition))
                {
                    unit.Rotation.LookAt = futurePosition;
                }
                else
                {
                    var delta = futurePosition - target.Movement.Position;

                    unit.Rotation.LookAt = target.Movement.Position + delta / 2;
                }

                rikoshetPoint = null;
                futurePoint = null;
                weapon.Shoot();
                return true;
            }

            return false;
        }

        private bool TryRikoshet()
        {
            if (delta.magnitude > hearRadius)
                return false;

            var start = weapon.BulletSpawner;
            var end = GetFuturePosition();

            var points = RicochetVisibility.Instance.GetRicochetPoints(start, end);

            if (points.Count == 0)
                return false;

            var minPoint = points[0];
            var minDistanse = 0f;

            foreach (var point in points)
            {
                var distanse = GetDistanse(point);
                if (distanse < minDistanse)
                {
                    minDistanse = distanse;
                    minPoint = point;
                }
            }

            rikoshetPoint = minPoint;

            var currentDelta = (rikoshetPoint.Value - start) - (end - rikoshetPoint.Value);

            var time = currentDelta.magnitude / weapon.BulletSpeed;

            var weaponDelta = unit.Movement.Position - weapon.BulletSpawner;
            weaponDelta.y = 0;

            futurePoint = move.GetFuturePosition(time) + weaponDelta;

            var moveDelta = (futurePoint - start).Value;
            var bulletDelta = (end - futurePoint).Value;

            var futureTime = moveDelta.magnitude / unit.Movement.Speed
                + bulletDelta.magnitude / weapon.BulletSpeed;

            if (futureTime < time)
            {
                if (!UnitVisibility.Instance.IsVisible(weapon.BulletSpawner, target.Center))
                    return false;
            }

            unit.Rotation.LookAt = rikoshetPoint.Value;
            weapon.Shoot();

            return true;

            float GetDistanse(Vector3 point)
            {
                return (point - start).magnitude + (end - point).magnitude;
            }
        }


        private Vector3 GetFuturePosition()
        {
            prediction = targetMove.GetFuturePosition(GetTime);
            return prediction;
        }

        private float GetTime(Vector3 futurePosition)
        {
            var delta = (futurePosition - centerDelta) - weapon.BulletSpawner;
            return delta.magnitude / weapon.BulletSpeed;
        }

        protected override void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            GizmosEx.DrawWireArc(unit.Movement.Position + Vector3.up * 0.1f, Vector3.up, hearRadius);

            if (rikoshetPoint.HasValue)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawSphere(rikoshetPoint.Value, 0.25f);
                Gizmos.DrawSphere(futurePoint.Value, 0.25f);
            }

            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(prediction, 0.25f);

#if UNITY_EDITOR
            var predictionDelta = prediction - weapon.BulletSpawner;
            Handles.color = Color.cyan;
            Handles.Label(prediction, predictionDelta.magnitude.ToString());

            var delta = target.Center - weapon.BulletSpawner;

            Handles.Label(target.Center, delta.magnitude.ToString());
#endif
        }
    }
}
