using UnityEngine;

namespace Tools
{
    public static class VectorTool
    {
        public static Vector2 RotateVector2(this Vector2 baseVector, float byAngle)
        {
            float newAngle = Mathf.Atan2(baseVector.y, baseVector.x) * Mathf.Rad2Deg + byAngle;
            return new Vector2(Mathf.Cos(newAngle / Mathf.Rad2Deg), Mathf.Sin(newAngle / Mathf.Rad2Deg));
        }
    }
}