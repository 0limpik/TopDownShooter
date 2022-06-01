using UnityEngine;

namespace TopDown.Scripts.Extensions
{
    internal static class VectorEx
    {
        private const float error = 0.25f;

        public static string ToDirection(this Vector3 vector)
        {
            vector.Normalize();

            if (IsDirection(vector, Vector3.forward))
                return "вперед";
            if (IsDirection(vector, Vector3.back))
                return "назад";
            if (IsDirection(vector, Vector3.right))
                return "вправо";
            if (IsDirection(vector, Vector3.left))
                return "влево";
            if (IsDirection(vector, Vector3.forward + Vector3.right))
                return "вперед-вправо";
            if (IsDirection(vector, Vector3.forward + Vector3.left))
                return "вперед-влево";
            if (IsDirection(vector, Vector3.back + Vector3.right))
                return "назад-вправо";
            if (IsDirection(vector, Vector3.back + Vector3.left))
                return "назад-влево";

            return vector.ToString();
        }

        private static bool IsDirection(Vector3 vector, Vector3 direction)
        {
            vector.Normalize();
            direction.Normalize();

            var plusDirection = direction.Add(error);
            var minusDirection = direction.Add(-error);

            bool x, y, z;
            x = y = z = false;

            if (vector.x <= plusDirection.x && vector.x >= minusDirection.x)
                x = true;
            if (vector.y <= plusDirection.y && vector.y >= minusDirection.y)
                y = true;
            if (vector.z <= plusDirection.z && vector.z >= minusDirection.z)
                z = true;

            return x && y && z;
        }

        public static Vector3 Add(this Vector3 vector, float additive)
        {
            vector.x += additive;
            vector.y += additive;
            vector.z += additive;

            return vector;
        }


        public static Vector2 ToXZ(this ref Vector3 vector)
            => new Vector2(vector.x, vector.z);

        public static Vector3 ToXZ(this ref Vector2 vector)
             => new Vector3(vector.x, 0, vector.y);

        public static bool XZ(this ref Vector3 vector, ref Vector3 vector2)
            => vector.X(ref vector2) && vector.Z(ref vector2);

        public static bool X(this ref Vector3 vector, ref Vector3 vector2)
            => Mathf.Approximately(vector.x, vector2.x);
        public static bool Y(this ref Vector3 vector, ref Vector3 vector2)
             => Mathf.Approximately(vector.y, vector2.y);
        public static bool Z(this ref Vector3 vector, ref Vector3 vector2)
              => Mathf.Approximately(vector.z, vector2.z);
    }
}
