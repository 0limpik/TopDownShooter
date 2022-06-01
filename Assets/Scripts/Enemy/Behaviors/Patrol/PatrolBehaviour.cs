using System;
using System.Threading.Tasks;
using TopDown.Scripts.Attributes;
using TopDown.Scripts.Enemy.Behaviors.Base;
using TopDown.Scripts.Enemy.Behaviors.Common;
using TopDown.Scripts.Enemy.Behaviors.Movement;
using TopDown.Scripts.Unit;
using UnityEngine;

namespace TopDown.Scripts.Enemy.Behaviors.Patrol
{
    [Serializable]
    [Info("Патрулирует месность, перемещяясь по случайным точкам")]
    internal class PatrolBehaviour : BaseBehaviour
    {
        [SerializeField] private WaitBehaviour wait;
        [SerializeField] private RandomPointBehaviour randomPoint;

        private IBehaviour current;

        public void Init(IUnit unit)
        {
            randomPoint.Init(unit);
        }

        protected override async Task OnRun()
        {
            while (running)
            {
                await Run(randomPoint);
                await Run(wait);
            }
        }

        protected override void OnStop()
        {
            current?.Stop();
        }

        public override void Update(float deltaTime)
        {
            current.Update(deltaTime);
        }

        private Task Run(IBehaviour behaviour)
        {
            if (!running)
                return Task.CompletedTask;

            current = behaviour;

            return behaviour.Run();
        }
    }
}
