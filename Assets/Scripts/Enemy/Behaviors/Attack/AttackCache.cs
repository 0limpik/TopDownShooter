using System;

namespace TopDown.Scripts.Enemy.Behaviors.Attack
{
    internal interface IAttack
    {
        public event Action<IAttackBehaviour, bool> OnChangedAttack;
        public bool CanAttack { get; }
    }

    internal class AttackCache : IAttack
    {
        public event Action<IAttackBehaviour, bool> OnChangedAttack
        {
            add
            {
                _OnChangedAttack += value;
                foreach (var item in inner)
                {
                    item.OnChangedAttack += value;
                }
            }
            remove
            {
                _OnChangedAttack -= value;
                foreach (var item in inner)
                {
                    item.OnChangedAttack -= value;
                }
            }
        }
        public event Action<IAttackBehaviour, bool> _OnChangedAttack;

        public bool CanAttack
        {
            get => _CanAttack;
            set
            {
                if (value != _CanAttack)
                {
                    _CanAttack = value;
                    _OnChangedAttack?.Invoke(behaviour, value);
                }
            }
        }
        private bool _CanAttack = false;

        private readonly IAttackBehaviour behaviour;
        private readonly IAttack[] inner;

        public AttackCache(IAttackBehaviour behaviour)
            : this(behaviour, null)
        {
        }

        public AttackCache(IAttackBehaviour behaviour, params IAttack[] inner)
        {
            this.behaviour = behaviour;
            this.inner = inner ?? new IAttack[0];
        }

        public bool SetCanAttackNotNotify(bool value)
            => _CanAttack = value;
    }
}
