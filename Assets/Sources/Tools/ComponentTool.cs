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
    }
}