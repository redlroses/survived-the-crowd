using UnityEngine;

namespace Tools.Extensions
{
    public static class MathfExtensions
    {
        public static float Clamp(ref this float value, float clampValue)
        {
            return value = Mathf.Clamp(value, -clampValue, clampValue);
        }
    }
}