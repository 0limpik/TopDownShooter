using System;
using TopDown.Scripts.Enemy.Behaviors.Attack;
using TopDown.Scripts.Enemy.Behaviors.Patrol;
using TopDown.Scripts.ScriptableObjects;
using TopDown.Scripts.Unit;
using TopDown.Scripts.Weapon;
using UnityEngine;

namespace TopDown.Scripts.Enemy
{
    [ExecuteAlways]
    internal class InitEnemyBehavior : MonoBehaviour
    {
        public event Action<IAttackBehaviour, bool> OnChangedAttack;

        [SerializeField] public UnitScript unit;
        [SerializeField] public UnitScript _player;
        [SerializeField] public WeaponScript _weapon;

        public PatrolBehaviour Patrol => _Patrol.Patrol;
        [Header("Behaviours")]
        [SerializeField] private PatrolBehaviourSO _Patrol;
        private PatrolBehaviourSO _OriginalPatrol;

        public ChaseAttackBehaviour ChaseAttack => _ChaseAttack.ChaseAttack;
        [Space(10)]
        [SerializeField] private ChaseAttackBehaviourSO _ChaseAttack;
        private ChaseAttackBehaviourSO _OriginalChaseAttack;

        void Awake()
        {
            if (Application.isPlaying)
            {
                _OriginalPatrol = _Patrol;
                _OriginalChaseAttack = _ChaseAttack;
            }
            SetBehaviours(_Patrol, _ChaseAttack);
        }

        public void SetBehaviours(PatrolBehaviourSO patrol, ChaseAttackBehaviourSO chase)
        {
            if (ChaseAttack.Attack != null)
            {
                ChaseAttack.Attack.OnChangedAttack -= ChangedAttack;
            }

            if (Application.isPlaying)
            {
                _Patrol = Instantiate(patrol);
                _ChaseAttack = Instantiate(chase);
            }

            Patrol.Init(unit);

            ChaseAttack.Init(unit, _player, _weapon);

            ChaseAttack.Attack.OnChangedAttack += ChangedAttack;
        }

        private void ChangedAttack(IAttackBehaviour behaviour, bool attack)
            => OnChangedAttack?.Invoke(behaviour, attack);

        void OnDestroy()
        {
            if (Application.isPlaying)
            {
                _Patrol = _OriginalPatrol;
                _ChaseAttack = _OriginalChaseAttack;
            }
        }

        void OnDrawGizmos()
        {
            unit.OnDrawGizmosInvoke();
        }
    }
}
