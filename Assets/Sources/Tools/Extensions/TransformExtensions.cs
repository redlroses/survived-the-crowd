using UnityEngine;

namespace Tools.Extensions
{
    public static class TransformExtensions
    {
        public static void SetLocalEulerZ(this Transform transform, float to)
        {
            var rotation = transform.rotation;
            transform.localRotation = Quaternion.Euler(rotation.eulerAngles.x, rotation.eulerAngles.y, to);
        }

        public static void SetLocalEulerY(this Transform transform, float to)
        {
            var rotation = transform.rotation;
            transform.localRotation = Quaternion.Euler(rotation.eulerAngles.x, to, rotation.eulerAngles.z);
        }

        public static void SetLocalEulerX(this Transform transform, float to)
        {
            var rotation = transform.rotation;
            transform.localRotation = Quaternion.Euler(to, rotation.eulerAngles.y, rotation.eulerAngles.z);
        }
    }
}