using System.Collections.Generic;
using TopDown.Scripts.Extensions;
using TopDown.Scripts.Visibility.Ricochet;
using UnityEngine;

namespace TopDown.Scripts.Visibility
{
    internal class RicochetVisibility : MonoBehaviour
    {
        public static RicochetVisibility Instance { get; private set; }

        [SerializeField] private MeshCollider[] obstacle;
        private MeshTriangles[] triangles;

        void Awake()
        {
            Instance = this;

            triangles = new MeshTriangles[obstacle.Length];
            for (int i = 0; i < obstacle.Length; i++)
            {
                triangles[i] = new MeshTriangles(obstacle[i]);
            }
        }

        public List<Vector3> GetRicochetPoints(Vector3 start, Vector3 end)
        {
            var points = new List<Vector3>();

            foreach (var triangle in triangles)
            {
                foreach (var plane in triangle.GetReflectPlanes(new Vector3[] { start, end }))
                {
                    var point = GetRicochetPoint(plane, start, end);

                    if (point.HasValue)
                        points.Add(point.Value);
                }
            }
            return points;
        }

        private Vector3? GetRicochetPoint(TrianglePlane plane, Vector3 start, Vector3 end)
        {
            plane.ClosestPoint(start, out Vector3 startClosest);
            plane.ClosestPoint(end, out Vector3 endClosest);

            if (!ShapesEx.LineLineIntersection(out Vector3 center, start, start - endClosest, end, end - startClosest))
            {
                return null;
            }

            if (!plane.ClosestPoint(center, out Vector3 closest))
            {
                return null;
            }

            var linecastOffest = closest + plane.Normal * 0.01f;

            var linecast1 = Physics.Linecast(
                start, linecastOffest,
                1 << LayerMask.NameToLayer("Obstacle"));

            var linecast2 = Physics.Linecast(
                linecastOffest, end,
                1 << LayerMask.NameToLayer("Obstacle"));

            if (linecast1 || linecast2)
                return null;

            return closest;
        }
    }
}
