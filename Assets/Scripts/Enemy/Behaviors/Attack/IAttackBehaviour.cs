namespace TopDown.Scripts.Enemy.Behaviors.Attack
{
    internal interface IAttackBehaviour
    {
        IAttack Attack { get; }

        bool CanAttack();
    }
}
