using UnityEngine;

namespace TopDown.Scripts.Extensions
{
    internal static class ShapesEx
    {
        /// <summary>
        /// Gets the intersections of two circles
        /// </summary>
        /// <param name="center1">The first circle's center</param>
        /// <param name="center2">The second circle's center</param>
        /// <param name="radius1">The first circle's radius</param>
        /// <param name="radius2">The second circle's radius. If omitted, assumed to equal the first circle's radius</param>
        /// <returns>An array of intersection points. May have zero, one, or two values</returns>
        /// <remarks>Adapted from http://csharphelper.com/blog/2014/09/determine-where-two-circles-intersect-in-c/</remarks>
        public static (Vector2? point1, Vector2? point2) GetCircleIntersections(Vector2 center1, Vector2 center2, float radius1, float radius2)
        {
            var delta = center1 - center2;

            var d = Mathf.Sqrt(Mathf.Pow(delta.x, 2) + Mathf.Pow(delta.y, 2));
            // Return an empty array if there are no intersections
            if (!(Mathf.Abs(radius1 - radius2) <= d && d <= radius1 + radius2))
                return (null, null);

            // Intersections i1 and possibly i2 exist
            var dsq = d * d;
            var (r1sq, r2sq) = (radius1 * radius1, radius2 * radius2);
            var r1sq_r2sq = r1sq - r2sq;
            var a = r1sq_r2sq / (2 * dsq);
            var c = Mathf.Sqrt(2 * (r1sq + r2sq) / dsq - r1sq_r2sq * r1sq_r2sq / (dsq * dsq) - 1);

            var fx = (center1.x + center2.x) / 2 + a * (center2.x - center1.x);
            var gx = c * (center2.y - center1.y) / 2;

            var fy = (center1.y + center2.y) / 2 + a * (center2.y - center1.y);
            var gy = c * (center1.x - center2.x) / 2;

            var i1 = new Vector2((float)(fx + gx), (float)(fy + gy));
            var i2 = new Vector2((float)(fx - gx), (float)(fy - gy));

            if (i1 == i2)
                return (i1, null);

            return (i1, i2);
        }

        public static bool LineLineIntersection(out Vector3 intersection, Vector3 point1, Vector3 vector1, Vector3 point2, Vector3 vector2)
        {
            var delta = point2 - point1;
            var cross1 = Vector3.Cross(vector1, vector2);
            var cross2 = Vector3.Cross(delta, vector2);

            var planarFactor = Vector3.Dot(delta, cross1);

            //is coplanar, and not parrallel
            if (Mathf.Abs(planarFactor) < 0.0001f && cross1.sqrMagnitude > 0.0001f)
            {
                var s = Vector3.Dot(cross2, cross1) / cross1.sqrMagnitude;
                intersection = point1 + vector1 * s;
                return true;
            }

            intersection = Vector3.zero;
            return false;
        }
    }
}
