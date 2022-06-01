using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopDown.Scripts.Attributes;
using TopDown.Scripts.Enemy.Behaviors.Common;
using TopDown.Scripts.Unit;
using UnityEngine;
using UnityEngine.AI;

namespace TopDown.Scripts.Enemy.Behaviors.Movement
{
    [Serializable]
    [Info("Создает путь используя NavMesh")]
    [Info("Устанавливает направление движения для следования по созданнуму пути")]
    internal class MoveBehaviour : UnitBehaviour, IFuturePosition
    {
        private const float navMeshRadius = 10f;

        private Vector3? position;

        protected Queue<Vector3> path = new Queue<Vector3>();

        public new void Init(IUnit unit)
        {
            base.Init(unit);
        }

        protected override Task OnRun()
        {
            if (!position.HasValue)
            {
                throw new InvalidOperationException($"{nameof(SetPosition)} first");
            }

            return base.OnRun();
        }

        protected override void OnStop()
        {
            position = null;

            if (path.Count != 0 && unit != null)
            {
                var delta = path.Peek() - unit.Movement.Position;
                unit.Rotation.LookAt = unit.Rotation.LookAt + delta.normalized;
            }

            unit.Movement.Direction = Vector3.zero;

            path.Clear();
        }

        public override void Update(float deltaTime)
        {
            if (path.Count == 0)
            {
                return;
            }

            var delta = path.Peek() - unit.Movement.Position;
            delta.y = 0;

            while (delta.magnitude == 0)
            {
                path.Dequeue();
                delta = path.Peek() - unit.Movement.Position;
                delta.y = 0;
            }

            if (delta.magnitude < unit.Movement.Speed * deltaTime)
            {
                if (path.Count == 1)
                {
                    Stop();
                    return;
                }

                path.Dequeue();
            }
            unit.Movement.Direction = delta;
            unit.Rotation.LookAt = path.Peek();
        }

        public void SetPosition(Vector3 value)
        {
            position = GeneratePath(value);
        }

        public Vector3 GetFuturePosition(float time)
        {
            if (this.path.Count == 0)
                return unit.Movement.Position;

            var path = this.path.ToArray();

            var speed = unit.Movement.Speed;

            var magnitude = time * speed;
            var start = unit.Movement.Position;
            var end = path[0];
            var sum = Vector3.zero;
            for (int i = 1; i < path.Length; i++)
            {
                var delta = end - start;

                if (magnitude > delta.magnitude)
                {
                    magnitude -= delta.magnitude;
                    sum += delta;
                }
                else
                {
                    return unit.Movement.Position + sum + delta * (magnitude / delta.magnitude);
                }

                start = path[i - 1];
                end = path[i];
            }
            return path[path.Length - 1];
        }

        public Vector3 GetFuturePosition(GetTime GetTime)
        {
            var time = GetTime(Vector3.zero);

            return GetFuturePosition(time);
        }

        protected virtual Vector3 GeneratePath(Vector3 value)
        {
            var path = new NavMeshPath();

            NavMesh.SamplePosition(value, out NavMeshHit hit, navMeshRadius, NavMesh.AllAreas);
            NavMesh.SamplePosition(unit.Movement.Position, out NavMeshHit self, navMeshRadius, NavMesh.AllAreas);
            NavMesh.CalculatePath(self.position, hit.position, NavMesh.AllAreas, path);

            this.path.Clear();

            for (int i = 0; i < path.corners.Length; i++)
            {
                this.path.Enqueue(path.corners[i]);
            }

            return this.path.Count > 1 ? this.path.Last() : this.path.Peek();
        }

        protected override void OnDrawGizmos()
        {
            if (path.Count > 0)
            {
                Gizmos.color = Color.yellow;
                foreach (var item in path)
                {
                    Gizmos.DrawSphere(item, 0.25f);
                }

                var destination = path.Last();

                Gizmos.color = Color.green;
                Gizmos.DrawSphere(path.Last(), 0.25f);

                Gizmos.DrawLine(unit.Movement.Position, path.First());

                var queue = path.ToArray();

                for (int i = 1; i < queue.Length; i++)
                {
                    var start = queue[i - 1];
                    var end = queue[i];

                    Gizmos.DrawLine(start, end);
                }
            }
        }
    }
}
