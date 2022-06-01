using System;
using System.Threading.Tasks;
using TopDown.Scripts.Attributes;
using TopDown.Scripts.Enemy.Behaviors.Base;
using TopDown.Scripts.Enemy.Behaviors.Common;
using TopDown.Scripts.Enemy.Behaviors.Movement;
using TopDown.Scripts.Extensions;
using TopDown.Scripts.Unit;
using TopDown.Scripts.Visibility;
using TopDown.Scripts.Weapon;
using UnityEngine;

namespace TopDown.Scripts.Enemy.Behaviors.Attack
{
    [Serializable]
    [Info("Преследует цель в ее последнее местоположенение и стреляет")]
    internal class AttackBehaviour : UnitBehaviour, IAttackBehaviour
    {
        public IAttack Attack => attackCache;
        private AttackCache attackCache;
        private VisibilityAttackBehaviour visibilityAttack;

        [Tooltip("Gizmos -> Arc White")]
        [SerializeField] public float shootRadius = 12f;
        [Tooltip("Gizmos -> Arc Red")]
        [SerializeField] public float attackRadius = 20f;

        [SerializeField] private HearShootBehaviour hearShoot;
        [SerializeField] private StrafeFollowBehaviour strafeFollow;
        [SerializeField] private StrafeCircleBehaviour strafeCircle;
        [SerializeField] private PredictionPositionBehaviour predictionPosition;
        [SerializeField] private BullertIgnoreBehaviour bullertIgnore;

        private IUnit target;

        private Vector3 delta => target.Movement.Position - unit.Movement.Position;

        public void Init(IUnit unit, IUnit target, WeaponScript weapon)
        {
            base.Init(unit);
            this.target = target;

            strafeFollow.Init(unit, target);
            strafeCircle.Init(unit, target);
            predictionPosition.Init(() => target.Movement.Position);
            hearShoot.Init(unit, target, weapon, strafeFollow, predictionPosition);
            bullertIgnore.Init(unit, weapon);

            visibilityAttack = new VisibilityAttackBehaviour(unit, target);
            attackCache = new AttackCache(this, hearShoot.Attack, visibilityAttack.Attack);
        }

        protected override async Task OnRun()
        {
            await base.OnRun();
            await bullertIgnore.Run();
        }

        protected override void OnStop()
        {
            strafeFollow.Stop();
            strafeCircle.Stop();
        }

        public override void Update(float deltaTime)
        {
            var visible = UnitVisibility.Instance.IsVisible(unit, target);

            strafeFollow.IgnoreVisible = delta.magnitude < hearShoot.hearRadius;

            if (delta.magnitude > strafeFollow.followRadius || !visible)
            {
                UpdateBehaviour(deltaTime, strafeFollow, strafeCircle);
            }
            else
            {
                UpdateBehaviour(deltaTime, strafeCircle, strafeFollow);
            }

            if (visible && delta.magnitude < attackRadius)
                unit.Rotation.LookAt = target.Movement.Position;

            UpdateOrStop(delta.magnitude < attackRadius && visible, predictionPosition, deltaTime);
            UpdateOrStop(delta.magnitude < shootRadius, hearShoot, deltaTime);
            bullertIgnore.Update(deltaTime);
        }

        public bool CanAttack()
        {
            if (hearShoot.CanAttack())
            {
                return true;
            }

            if (delta.magnitude > attackRadius)
            {
                return attackCache.CanAttack = false;
            }
            else
            {
                attackCache.CanAttack = true;
            }

            return visibilityAttack.CanAttack();
        }

        private void UpdateBehaviour(float deltaTime, BaseBehaviour enable, BaseBehaviour disable)
        {
            if (!enable.running)
            {
                disable.Stop();
                enable.Run().Forget();
            }
            enable.Update(deltaTime);
        }

        private void UpdateOrStop(bool update, BaseBehaviour behaviour, float deltaTime)
        {
            if (update)
            {
                if (!behaviour.running)
                    behaviour.Run().Forget();

                behaviour.Update(deltaTime);
            }
            else
            {
                behaviour.Stop();
            }
        }

        protected override void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            GizmosEx.DrawWireArc(unit.Movement.Position + Vector3.up * 0.1f, Vector3.up, shootRadius);

            Gizmos.color = Color.red;
            GizmosEx.DrawWireArc(unit.Movement.Position + Vector3.up * 0.1f, Vector3.up, attackRadius);

            var delta = target.Movement.Position - unit.Movement.Position;
            if (delta.magnitude < attackRadius)
            {
                if (attackCache.CanAttack)
                {
                    Gizmos.color = Color.white;
                }
                else
                {
                    Gizmos.color = Color.red;
                }
                Gizmos.DrawLine(unit.Center, target.Center);
            }
        }
    }
}
