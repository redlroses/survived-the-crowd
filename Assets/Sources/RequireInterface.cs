using System;
using UnityEngine;

namespace Sources.Custom
{
    public class RequireInterface : PropertyAttribute
    {
        public readonly Type RequireType;

        public RequireInterface(Type requireType)
        {
            RequireType = requireType;
        }
    }
}