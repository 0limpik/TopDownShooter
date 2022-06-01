using TopDown.Scripts.Unit;
using TopDown.Scripts.Visibility;

namespace TopDown.Scripts.Enemy.Behaviors.Attack
{
    internal class VisibilityAttackBehaviour : IAttackBehaviour
    {
        public IAttack Attack => AttackCache;
        private AttackCache AttackCache;

        private IUnit unit;
        private IUnit target;

        public VisibilityAttackBehaviour(IUnit unit, IUnit target)
        {
            this.unit = unit;
            this.target = target;
            AttackCache = new AttackCache(this);
        }

        public bool CanAttack()
        {
            if (UnitVisibility.Instance.IsVisible(unit, target))
                return AttackCache.CanAttack = true;
            else
                return AttackCache.CanAttack = false;
        }
    }
}
