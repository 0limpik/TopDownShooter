using System;
using System.Threading.Tasks;
using TopDown.Scripts.Attributes;
using TopDown.Scripts.Enemy.Behaviors.Base;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TopDown.Scripts.Enemy.Behaviors.Common
{
    [Serializable]
    [Info("Ожидает задержку + случайное время в диапазоне")]
    public class WaitBehaviour : BaseBehaviour
    {
        [SerializeField] public float delay = 1f;
        [SerializeField] public float range = 1f;

        private float time;
        [SerializeField, HideInInspector] private float pastTime;

        protected override Task OnRun()
        {
            time = Random.Range(0, range);

            return Task.CompletedTask;
        }

        protected override void OnStop() { }

        public override void Update(float deltaTime)
        {
            if (pastTime > 0)
            {
                pastTime -= deltaTime;
            }
            else
            {
                pastTime = delay + time;
                Stop();
            }
        }
    }
}
