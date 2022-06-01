using System;
using System.Threading.Tasks;
using TopDown.Scripts.Attributes;
using TopDown.Scripts.Enemy.Behaviors.Common;
using TopDown.Scripts.Extensions;
using TopDown.Scripts.Unit;
using TopDown.Scripts.Visibility;
using UnityEngine;

namespace TopDown.Scripts.Enemy.Behaviors.Movement
{
    [Serializable]
    [Info("Следует за целью")]
    [Info("Если цель видима, двигается стрейфами")]
    [Info("Если цель не видима, двигается обычно")]
    internal class StrafeFollowBehaviour : UnitBehaviour, IFuturePosition
    {
        [Tooltip("Gizmos -> Arc Yellow")]
        [SerializeField] public float followRadius = 8f;

        [SerializeField] private StrafeMoveBehaviour strafeMove;
        [SerializeField] private MoveBehaviour move;

        private IFuturePosition current;

        public Vector3 GetFuturePosition(float time)
            => current.GetFuturePosition(time);
        public Vector3 GetFuturePosition(GetTime GetTime)
            => current.GetFuturePosition(GetTime);

        [NonSerialized] public bool IgnoreVisible = false;

        private IUnit target;

        private Vector3 lastVisiblePosition;

        public void Init(IUnit unit, IUnit target)
        {
            base.Init(unit);
            this.target = target;

            strafeMove.Init(unit);
            move.Init(unit);
        }

        protected override Task OnRun()
        {
            return base.OnRun();
        }

        protected override void OnStop()
        {
            strafeMove.Stop();
            move.Stop();
        }

        public override void Update(float deltaTime)
        {
            var visible = UnitVisibility.Instance.IsVisible(unit, target);

            if (IgnoreVisible || visible)
            {
                lastVisiblePosition = target.Movement.Position;
            }

            var delta = lastVisiblePosition - unit.Movement.Position;

            if (delta.magnitude > followRadius || !visible)
            {
                if (visible)
                {
                    UpdateBehaviour(deltaTime, strafeMove, move);
                }
                else
                {
                    UpdateBehaviour(deltaTime, move, strafeMove);
                }
            }
            else
            {
                OnStop();
            }
        }

        private void UpdateBehaviour(float deltaTime, MoveBehaviour enable, MoveBehaviour disable)
        {
            current = enable;
            if (!enable.running)
            {
                disable.Stop();
                enable.SetPosition(lastVisiblePosition);
                enable.Run().Forget();
            }
            enable.Update(deltaTime);
        }

        protected override void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            GizmosEx.DrawWireArc(unit.Movement.Position, Vector3.up, followRadius);
        }
    }
}
