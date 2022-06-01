using System;
using TopDown.Scripts.Enemy.Behaviors.Attack;
using TopDown.Scripts.Enemy.Behaviors.Base;
using TopDown.Scripts.Enemy.Behaviors.Patrol;
using UnityEngine;

namespace TopDown.Scripts.ScriptableObjects
{
    [Serializable]
    public enum BehavioursSO
    {
        Patrol,
        ChaseAttack
    }

    [CreateAssetMenu(fileName = nameof(BaseBehaviour), menuName = "Game/" + nameof(BaseBehaviour), order = 1)]
    public class BaseBehaviourSO : ScriptableObject
    {
        [SerializeField] private PatrolBehaviour patrol;
        [SerializeField] private ChaseAttackBehaviour chaseAttack;

        [SerializeField] public BehavioursSO behaviourType;
        public BaseBehaviour Behaviour

        => behaviourType switch
        {
            BehavioursSO.Patrol => patrol,
            BehavioursSO.ChaseAttack => chaseAttack,
            _ => throw new NotImplementedException(),
        };

        void Awake()
        {

        }

        //[Button("Create Patrol Behaviour")]
        //void CreatePatrol()
        //{
        //    patrol = new PatrolBehaviour();
        //}

        //[Button("Create Chase Attack Behaviour")]
        //void CreateChaseAttack()
        //{
        //    chaseAttack = new ChaseAttackBehaviour();
        //}
    }
}
