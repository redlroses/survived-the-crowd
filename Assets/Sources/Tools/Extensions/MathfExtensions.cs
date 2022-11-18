using UnityEngine;

namespace Sources.Tools.Extensions
{
    public static class MathfExtensions
    {
        public static float Clamp(ref this float value, float clampValue)
        {
            return value = Mathf.Clamp(value, -clampValue, clampValue);
        }
    }
}