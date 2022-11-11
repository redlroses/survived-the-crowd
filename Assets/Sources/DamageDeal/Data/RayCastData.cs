using UnityEngine;

namespace Sources.DamageDeal.Data
{
    public class RayCastData
    {
        public RayCastData(Ray ray, LayerMask mask)
        {
            Mask = mask;
            Ray = ray;
        }

        public Ray Ray { get; }
        public LayerMask Mask { get; }
    }
}