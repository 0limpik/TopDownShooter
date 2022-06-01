using UnityEngine;

namespace TopDown.Scripts.Visibility.Ricochet
{
    internal struct TrianglePlane
    {
        public Vector3 V1 { get; private set; }
        public Vector3 V2 { get; private set; }
        public Vector3 V3 { get; private set; }

        public Vector3 Center { get; private set; }

        private readonly float distance;

        public Vector3 Normal { get; private set; }

        public TrianglePlane(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 normal)
        {
            this.V1 = v1;
            this.V2 = v2;
            this.V3 = v3;

            this.Normal = normal.normalized;

            Center = (V1 + V2 + V3) / 3;

            distance = 0f - Vector3.Dot(Normal, V1);
        }

        public bool ClosestPoint(Vector3 point, out Vector3 closest)
        {
            float d = Vector3.Dot(Normal, point) + distance;
            closest = point - Normal * d;

            // Compute vectors        
            var v0 = V3 - V1;
            var v1 = V2 - V1;
            var v2 = point - V1;

            // Compute dot products
            var dot00 = Vector3.Dot(v0, v0);
            var dot01 = Vector3.Dot(v0, v1);
            var dot02 = Vector3.Dot(v0, v2);
            var dot11 = Vector3.Dot(v1, v1);
            var dot12 = Vector3.Dot(v1, v2);

            // Compute barycentric coordinates
            var invDenom = 1 / (dot00 * dot11 - dot01 * dot01);
            var u = (dot11 * dot02 - dot01 * dot12) * invDenom;
            var v = (dot00 * dot12 - dot01 * dot02) * invDenom;

            // Check if point is in triangle
            return (u >= 0) && (v >= 0) && (u + v < 1);
        }
    }
}
