using Tools;
using UnityEngine;

namespace Sources.Tools.Extensions
{
    public static class VectorExtensions
    {
        public static Vector2 RotateVector2(this Vector2 baseVector, float byAngle)
        {
            float newAngle = Mathf.Atan2(baseVector.y, baseVector.x) * Mathf.Rad2Deg + byAngle;
            return new Vector2(Mathf.Cos(newAngle / Mathf.Rad2Deg), Mathf.Sin(newAngle / Mathf.Rad2Deg));
        }

        public static Vector3 SetX(this Vector3 baseVector, float to)
        {
            return new Vector3(baseVector.x, to, baseVector.z);
        }

        public static Vector3 SetY(this Vector3 baseVector, float to)
        {
            return new Vector3(baseVector.x, to, baseVector.z);
        }

        public static Vector3 SetZ(this Vector3 baseVector, float to)
        {
            return new Vector3(baseVector.x, baseVector.y, to);
        }

        public static Vector2 ToInputFormat(this Vector3 baseVector)
        {
            return new Vector2(baseVector.x, baseVector.z).normalized;
        }

        public static Vector2 ToWorld(this Vector2 baseVector)
        {
            return baseVector.RotateVector2(-Constants.HalfPiDegrees);
        }
    }
}