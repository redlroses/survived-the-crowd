using System;
using UnityEngine;

namespace Sources.Tools
{
    public static class ComponentTool
    {
        public static void CheckNull<T>(T component)
        {
            if (component == null)
            {
                throw new NullReferenceException($"{nameof(component)} is null");
            }
        }

        public static void ValidateInterface<TInterface>(ref MonoBehaviour mono)
        {
            if (mono is TInterface)
            {
                return;
            }

            Debug.LogError($"{mono.name} needs to implement {nameof(TInterface)}");
            mono = null;
        }
    }
}