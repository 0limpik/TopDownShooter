using System.Collections.Generic;
using UnityEngine;

namespace TopDown.Scripts.Visibility.Ricochet
{
    internal class MeshTriangles
    {
        public IEnumerable<TrianglePlane> Planes => _Planes;
        private List<TrianglePlane> _Planes = new List<TrianglePlane>();

        public MeshTriangles(MeshCollider meshCollider)
        {
            var triangles = meshCollider.sharedMesh.triangles;
            var vertices = meshCollider.sharedMesh.vertices;
            var normals = meshCollider.sharedMesh.normals;

            for (int i = 0; i < triangles.Length; i += 3)
            {
                var v1 = vertices[triangles[i]];
                var v2 = vertices[triangles[i + 1]];
                var v3 = vertices[triangles[i + 2]];

                var n1 = normals[triangles[i]];
                var n2 = normals[triangles[i + 1]];
                var n3 = normals[triangles[i + 2]];

                var n = (n1 + n2 + n3) / 3;
                _Planes.Add(new TrianglePlane
                (
                    meshCollider.transform.position + meshCollider.transform.rotation * v1,
                    meshCollider.transform.position + meshCollider.transform.rotation * v2,
                    meshCollider.transform.position + meshCollider.transform.rotation * v3,
                    meshCollider.transform.rotation * n
                ));
            }
        }

        public IEnumerable<TrianglePlane> GetReflectPlanes(Vector3 position)
        {
            var x = new List<TrianglePlane>();

            foreach (var plane in _Planes)
            {
                var delta = plane.Center - position;

                var angle = Vector3.Angle(delta.normalized, plane.Normal);

                if (angle > 90)
                {
                    x.Add(plane);
                }
            }

            return x;
        }

        public IEnumerable<TrianglePlane> GetReflectPlanes(Vector3[] positions)
        {
            var planes = new List<TrianglePlane>();

            foreach (var plane in _Planes)
            {
                var skip = false;

                foreach (var position in positions)
                {
                    var delta = plane.Center - position;
                    var angle = Vector3.Angle(delta.normalized, plane.Normal);

                    if (angle < 90)
                    {
                        skip = true;
                        break;
                    }
                }

                if (skip)
                    continue;

                planes.Add(plane);
            }

            return planes;
        }
    }
}
