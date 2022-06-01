using TopDown.Scripts.Enemy.Behaviors.Patrol;
using UnityEngine;

namespace TopDown.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(PatrolBehaviour), menuName = "Game/" + nameof(PatrolBehaviour), order = 1)]
    internal class PatrolBehaviourSO : ScriptableObject
    {
        [field: SerializeField] public PatrolBehaviour Patrol { get; private set; }
    }
}
