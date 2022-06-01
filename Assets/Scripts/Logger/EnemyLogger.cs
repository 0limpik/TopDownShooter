using TopDown.Scripts.Enemy;
using TopDown.Scripts.Enemy.Behaviors.Attack;
using TopDown.Scripts.Level;
using TopDown.Scripts.UI;
using UnityEngine;

namespace TopDown.Scripts.Logger
{
    internal class EnemyLogger : MonoBehaviour
    {
        [SerializeField] private EventsUI events;
        [SerializeField] private InitEnemyBehavior enemy;

        [SerializeField] private EnemyDifficult difficult;

        [SerializeField] private LevelScript level;

        void Start()
        {
            enemy.OnChangedAttack += OnChangedAttack;
            level.OnEnemyWin += () => events.AddEvent("Враг победил");
            difficult.OnChangeDifficult += (x) => events.AddEvent($"Враг сложности {x.ToLower()}");
        }

        private void OnChangedAttack(IAttackBehaviour behaviour, bool attack)
        {
            if (behaviour is ChaseAttackBehaviour)
                AddMessage("Преследует игрока", "Потерял интерес");

            if (behaviour is AttackBehaviour)
                AddMessage("Игрок вошел в радиус атаки", "Игрок вышел из радиуса атаки");

            if (behaviour is VisibilityAttackBehaviour)
                AddMessage("Увидел игрока", "Потерял игрока из виду");

            if (behaviour is HearShootBehaviour)
                AddMessage("Услышал игрока", "Перестал слышать игрока");

            void AddMessage(string isAttack, string isNotAttack)
            {
                if (attack)
                    events.AddEvent(isAttack);
                else
                    events.AddEvent(isNotAttack);
            }
        }
    }
}
