using System;
using System.Threading.Tasks;
using TopDown.Scripts.Attributes;
using TopDown.Scripts.Enemy.Behaviors.Common;
using TopDown.Scripts.Extensions;
using TopDown.Scripts.Unit;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace TopDown.Scripts.Enemy.Behaviors.Movement
{
    [Serializable]
    [Info("Выбирает случайную точку, и движется к ней")]
    internal class RandomPointBehaviour : UnitBehaviour
    {
        [Tooltip("Gizmos -> Random point on Arc Black")]
        [SerializeField] public float circleRange = 20f;

        [SerializeField] private MoveBehaviour move;

        public new void Init(IUnit unit)
        {
            base.Init(unit);

            move.Init(unit);
        }

        protected override async Task OnRun()
        {
            await base.OnRun();

            var destenation = GeneratePath();
            move.SetPosition(destenation);
            await move.Run();
            Stop();
        }

        protected override void OnStop()
        {
            move.Stop();
        }

        public override void Update(float deltaTime)
        {
            move.Update(deltaTime);
        }

        private Vector3 GeneratePath()
        {
            var hit = new NavMeshHit();

            for (int i = 0; i < 15; i++)
            {
                var direction = Random.insideUnitSphere.normalized * circleRange;

                var position = unit.Movement.Position;
                position.x += direction.x;
                position.z += direction.z;

                if (NavMesh.SamplePosition(position, out hit, 1f, NavMesh.AllAreas))
                {
                    var hitPosition = hit.position;

                    if (hitPosition.XZ(ref position))
                    {
                        break;
                    }
                }
            }

            return hit.position;
        }

        protected override void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            GizmosEx.DrawWireArc(unit.Movement.Position + Vector3.up * 0.1f, Vector3.up, circleRange);
        }
    }
}
