using System;
using TopDown.Scripts.Attributes;
using UnityEngine;
using UnityEngine.AI;

namespace TopDown.Scripts.Enemy.Behaviors.Movement
{
    internal delegate Vector3 GetOffset(Vector3 prevPoint, Vector3 point);

    [Serializable]
    [Info("Создает путь используя NavMesh, добавляет в путь стрейфы влево и вправо")]
    [Info("Устанавливает направление движения для следования по созданнуму пути")]
    internal class StrafeMoveBehaviour : MoveBehaviour
    {
        [SerializeField] private float width = 1f;
        [SerializeField] private float height = 1f;

        private int strafeDirection;

        protected override Vector3 GeneratePath(Vector3 value)
        {
            var basePath = base.GeneratePath(value);

            if (path.Count == 0)
            {
                return basePath;
            }

            var paths = path.ToArray();

            path.Clear();

            path.Enqueue(paths[0]);

            for (int i = 1; i < paths.Length; i++)
            {
                var start = paths[i - 1];
                var end = paths[i];

                var delta = end - start;

                var count = (int)(delta.magnitude / this.height);

                var height = delta.magnitude / count;

                var forward = delta.normalized;
                var right = Quaternion.AngleAxis(-90, Vector3.up) * forward;

                var point = start;

                var side = strafeDirection++ % 2 == 0 ? right : right * -1;

                for (int j = 0; j < count; j++)
                {
                    point += forward * height * 0.25f + side * width * 0.25f;
                    AddPoint(point);

                    point += forward * height * 0.5f + side * width * 0.5f * -1;
                    AddPoint(point);

                    point += forward * height * 0.25f + side * width * 0.25f;
                }

                void AddPoint(Vector3 point)
                {
                    if (NavMesh.SamplePosition(point, out NavMeshHit hit, width / 4, NavMesh.AllAreas))
                    {
                        path.Enqueue(hit.position);
                    }
                }
            }

            path.Enqueue(paths[paths.Length - 1]);

            return basePath;
        }
    }
}
