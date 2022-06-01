using System;
using TopDown.Scripts.Enemy;
using TopDown.Scripts.Level;
using UnityEngine;
using UnityEngine.UIElements;

namespace TopDown.Scripts.UI
{
    [RequireComponent(typeof(UIDocument))]
    internal class ScoreUI : MonoBehaviour
    {
        private UIDocument document;

        private VisualElement score;
        private Label player;
        private Label enemy;
        private Label difficultLabel;

        [SerializeField] private LevelScript level;
        [SerializeField] private EnemyDifficult difficult;

        void Awake()
        {
            document = GetComponent<UIDocument>();

            score = document.rootVisualElement.Q("score") ?? throw new ArgumentException();

            player = score.Q<Label>("player");
            enemy = score.Q<Label>("enemy");
            difficultLabel = score.Q<Label>("difficult");

            level.OnScoreChanged += Level_OnScoreChanged;
            difficult.OnChangeDifficult += Difficult_OnChangeDifficult;
        }

        private void Level_OnScoreChanged(int playerScore, int enemyScore)
        {
            player.text = playerScore.ToString();
            enemy.text = enemyScore.ToString();
        }

        private void Difficult_OnChangeDifficult(string difficult)
        {
            difficultLabel.text = difficult;
        }
    }
}
