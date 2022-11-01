﻿using UnityEngine;

namespace Tools.Extensions
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
    }
}