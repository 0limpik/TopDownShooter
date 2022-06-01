using TopDown.Scripts.Enemy.Behaviors.Attack;
using UnityEngine;

namespace TopDown.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(ChaseAttackBehaviour), menuName = "Game/" + nameof(ChaseAttackBehaviour), order = 1)]
    internal class ChaseAttackBehaviourSO : ScriptableObject
    {
        [field: SerializeField] public ChaseAttackBehaviour ChaseAttack { get; private set; }
    }
}
