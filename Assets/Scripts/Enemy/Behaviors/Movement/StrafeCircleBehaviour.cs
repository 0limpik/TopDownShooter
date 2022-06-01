using System;
using System.Threading.Tasks;
using TopDown.Scripts.Attributes;
using TopDown.Scripts.Enemy.Behaviors.Common;
using TopDown.Scripts.Extensions;
using TopDown.Scripts.Unit;
using UnityEngine;

namespace TopDown.Scripts.Enemy.Behaviors.Movement
{
    [Serializable]
    [Info("Меняет направление движения стрейфа")]
    internal class StrafeCircleBehaviour : UnitBehaviour
    {
        [SerializeField] private StrafeCircleMoveBehaviour strafeCircle;
        [SerializeField] private WaitBehaviour wait;

        public void Init(IUnit unit, IUnit player)
        {
            base.Init(unit);

            strafeCircle.Init(unit, player);
            strafeCircle.OnDirectionChange += OnDirectionChange;
        }

        protected async override Task OnRun()
        {
            await base.OnRun();
            await strafeCircle.Run();
        }

        protected override void OnStop()
        {
            wait.Stop();
            strafeCircle.Stop();
        }

        public override void Update(float deltaTime)
        {
            if (!wait.running)
            {
                strafeCircle.ChangeDirection();
            }
            wait.Update(deltaTime);

            strafeCircle.Update(deltaTime);
        }

        private void OnDirectionChange()
        {
            wait.Stop();
            wait.Run().Forget();
        }
    }
}
