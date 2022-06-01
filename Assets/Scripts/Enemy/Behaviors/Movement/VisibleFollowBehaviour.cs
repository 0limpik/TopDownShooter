using System;
using System.Threading.Tasks;
using TopDown.Scripts.Enemy.Behaviors.Base;
using TopDown.Scripts.Extensions;
using TopDown.Scripts.Unit;
using TopDown.Scripts.Visibility;
using UnityEngine;

namespace TopDown.Scripts.Enemy.Behaviors.Movement
{
    [Serializable]
    internal class VisibleFollowBehaviour : BaseBehaviour
    {
        [SerializeField] public float followRadius = 8f;

        [SerializeField] private MoveBehaviour move;

        private IUnit unit;
        private IUnit target;

        public void Init(IUnit unit, IUnit target)
        {
            this.unit = unit;
            this.target = target;

            move.Init(unit);
        }

        protected override Task OnRun()
        {
            return Task.CompletedTask;
        }

        protected override void OnStop()
        {
            move.Stop();
        }

        private Vector3 lastVisiblePosition;

        public override void Update(float deltaTime)
        {
            var visible = UnitVisibility.Instance.IsVisible(unit, target);

            if (visible)
            {
                lastVisiblePosition = target.Movement.Position;
            }

            var delta = lastVisiblePosition - unit.Movement.Position;

            if (delta.magnitude > followRadius || !visible)
            {
                if (!move.running)
                {
                    move.SetPosition(lastVisiblePosition);
                    move.Run().Forget();
                }
                move.Update(deltaTime);
            }
            else
            {
                move.Update(deltaTime);
                move.Stop();
            }
        }
    }
}
