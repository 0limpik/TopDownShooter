using System;
using System.Threading.Tasks;
using TopDown.Scripts.Attributes;
using TopDown.Scripts.Enemy.Behaviors.Base;
using TopDown.Scripts.Extensions;
using TopDown.Scripts.Unit;
using TopDown.Scripts.Weapon;
using UnityEngine;

namespace TopDown.Scripts.Enemy.Behaviors.Attack
{
    [Serializable]
    [Info("Продолжает преследовать цель, даже если она скрылась из виду")]
    internal class ChaseAttackBehaviour : BaseBehaviour, IAttackBehaviour
    {
        public IAttack Attack => AttackCache;
        private AttackCache AttackCache;

        [SerializeField] private float chaseDuration = 2f;

        [SerializeField] private AttackBehaviour attack;

        private float lostAttack = float.NegativeInfinity;
        private float time;

        public void Init(IUnit unit, IUnit player, WeaponScript weapon)
        {
            attack.Init(unit, player, weapon);
            AttackCache = new AttackCache(this, attack.Attack);
        }

        protected override Task OnRun()
        {
            attack.Run().Forget();
            return Task.CompletedTask;
        }

        protected override void OnStop()
        {
            attack.Stop();
        }

        public override void Update(float deltaTime)
        {
            time += deltaTime;

            attack.Update(deltaTime);
        }

        public bool CanAttack()
        {
            var canAttack = attack.CanAttack();

            if (canAttack)
            {
                lostAttack = time + chaseDuration;
                AttackCache.SetCanAttackNotNotify(false);
                return true;
            }

            if (time < lostAttack)
            {
                return AttackCache.CanAttack = true;
            }

            return AttackCache.CanAttack = false;
        }
    }
}
