using System;
using TopDown.Scripts.Unit;
using TopDown.Scripts.Weapon;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TopDown.Scripts.Level
{
    internal delegate void OnScoreChanged(int playerScore, int enemyScore);

    internal class LevelScript : MonoBehaviour
    {
        public event OnScoreChanged OnScoreChanged;
        public event Action OnPlayerWin;
        public event Action OnEnemyWin;

        public int PlayerScore { get; private set; }
        public int EnemyScore { get; private set; }

        [SerializeField] private UnitScript player;
        [SerializeField] private UnitScript enemy;

        [SerializeField] private Transform[] spawnPoints = new Transform[0];

        [SerializeField] private float loseHeight = -5f;

        void Awake()
        {
            BulletRepository.OnAdd += (b) => b.OnCollision += CheckWinner;
            BulletRepository.OnRemove += (b) => b.OnCollision -= CheckWinner;
        }

        void Update()
        {
            Check(player.gameObject);
            Check(enemy.gameObject);

            void Check(GameObject obj)
            {
                if (obj.transform.position.y < loseHeight)
                {
                    CheckWinner(obj);
                }
            }
        }

        private void CheckWinner(GameObject obj)
        {
            if (obj == player.gameObject)
            {
                EnemyScore++;
                OnScoreChanged?.Invoke(PlayerScore, EnemyScore);
                OnEnemyWin?.Invoke();
                Restart();
            }
            if (obj == enemy.gameObject)
            {
                PlayerScore++;
                OnScoreChanged?.Invoke(PlayerScore, EnemyScore);
                OnPlayerWin?.Invoke();
                Restart();
            }
        }

        private void Restart()
        {
            foreach (var bullet in BulletRepository.Bullets)
            {
                Destroy(bullet.gameObject);
            }

            var playerPos = spawnPoints[Random.Range(0, spawnPoints.Length)].position;

            var enemyPos = playerPos;

            foreach (var point in spawnPoints)
            {
                var deltaPoint = enemyPos - point.position;
                var deltaPlayer = enemyPos - playerPos;

                if (deltaPoint.magnitude > deltaPlayer.magnitude)
                {
                    enemyPos = point.position;
                }
            }

            player.transform.position = playerPos;
            enemy.transform.position = enemyPos;
        }
    }
}
