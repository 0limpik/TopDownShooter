using System;
using System.Threading.Tasks;
using TopDown.Scripts.Attributes;
using TopDown.Scripts.Enemy.Behaviors.Common;
using TopDown.Scripts.Extensions;
using TopDown.Scripts.Unit;
using UnityEngine;
using UnityEngine.AI;

namespace TopDown.Scripts.Enemy.Behaviors.Movement
{
    [Serializable]
    [Info("Устанавливает направление движения влево или вправо по окружности от цели")]
    internal class StrafeCircleMoveBehaviour : UnitBehaviour
    {
        private const float outOfMeshStuckRange = 10f;

        public event Action OnDirectionChange;

        private IUnit target;

        private int direction;

        private Vector3 delta => target.Movement.Position - unit.Movement.Position;

        public void Init(IUnit unit, IUnit target)
        {
            base.Init(unit);
            this.target = target;
        }

        protected override Task OnRun()
        {
            return base.OnRun();
        }

        protected override void OnStop()
        {

        }

        public override void Update(float deltaTime)
        {
            var unitPos = unit.Movement.Position;
            var playerPos = target.Movement.Position;

            var move = unit.Movement.Speed * deltaTime;

            var intersections = ShapesEx.GetCircleIntersections(
                unitPos.ToXZ(), playerPos.ToXZ(),
                move, delta.magnitude);

            Vector2 point1;
            Vector2 point2;

            if (intersections.point2.HasValue)
            {
                point1 = direction % 2 == 0 ? intersections.point1.Value : intersections.point2.Value;
                point2 = direction % 2 != 0 ? intersections.point1.Value : intersections.point2.Value;
            }
            else if (intersections.point1.HasValue)
                point1 = point2 = intersections.point1.Value;
            else
            {
                point1 = point2 = unitPos.ToXZ();
            }

            var intersection = point1.ToXZ();

            if (!NavMesh.SamplePosition(intersection, out _, move * 2, NavMesh.AllAreas))
            {
                intersection = point2.ToXZ();
                if (NavMesh.SamplePosition(intersection, out _, move * 2, NavMesh.AllAreas))
                {
                    unit.Movement.Direction = intersection - unit.Movement.Position;
                    ChangeDirection();
                }
                else
                {
                    NavMesh.SamplePosition(unit.Movement.Position, out NavMeshHit hit, outOfMeshStuckRange, NavMesh.AllAreas);
                    intersection = hit.position;
                }
            }
            unit.Movement.Direction = intersection - unit.Movement.Position;
        }

        public void ChangeDirection()
        {
            direction++;
            OnDirectionChange?.Invoke();
        }

        protected override void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            GizmosEx.DrawWireArc(unit.Movement.Position, Vector3.up, unit.Movement.Speed * 0.1f, 100);
            GizmosEx.DrawWireArc(target.Movement.Position, Vector3.up, delta.magnitude, 200);
        }
    }
}
