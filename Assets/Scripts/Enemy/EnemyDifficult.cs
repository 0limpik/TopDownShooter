using System;
using System.Linq;
using TopDown.Scripts.Level;
using TopDown.Scripts.ScriptableObjects;
using UnityEngine;

namespace TopDown.Scripts.Enemy
{
    internal class EnemyDifficult : MonoBehaviour
    {
        public event Action<string> OnChangeDifficult;

        [SerializeField] private LevelScript levelScript;
        [SerializeField] private InitEnemyBehavior initEnemy;

        [SerializeField] private EnemyLevel[] enemyLevels = new EnemyLevel[0];

        void Awake()
        {
            levelScript.OnScoreChanged += OnScoreChanged;
        }

        void Start()
        {
            SetDifficult(0);
        }

        private void OnScoreChanged(int playerScore, int enemyScore)
        {
            var difference = playerScore - enemyScore;
            SetDifficult(difference);
        }

        private void SetDifficult(int difference)
        {
            var sorted = enemyLevels.OrderBy(x => x.difference);

            var min = sorted.FirstOrDefault();
            var max = sorted.LastOrDefault();

            for (int i = 1; i < enemyLevels.Length; i++)
            {
                var prev = enemyLevels[i - 1];
                var level = enemyLevels[i];

                if (prev.difference <= difference && level.difference >= difference)
                {
                    float delta = level.difference - prev.difference;

                    var ratio = (difference - prev.difference) / delta;

                    ChangeDifficult(ratio <= 0.5f ? prev : level);
                    return;
                }
            }

            if (min.difference > difference)
            {
                ChangeDifficult(min);
                return;
            }

            if (max.difference < difference)
            {
                ChangeDifficult(max);
                return;
            }
        }

        private void ChangeDifficult(EnemyLevel level)
        {
            initEnemy.SetBehaviours(level.patrool, level.attack);
            OnChangeDifficult?.Invoke(level.name);
        }

        [Serializable]
        private class EnemyLevel
        {
            public int difference;
            public string name;
            public PatrolBehaviourSO patrool;
            public ChaseAttackBehaviourSO attack;
        }
    }
}
