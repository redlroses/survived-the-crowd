using UnityEngine;

namespace Sources.DamageDeal.Data
{
    public class OverlapSphereData
    {
        public OverlapSphereData(Vector3 sphereCenter, float sphereRadius, LayerMask layerMask)
        {
            SphereCenter = sphereCenter;
            SphereRadius = sphereRadius;
            Mask = layerMask;
        }

        public Vector3 SphereCenter { get; }
        public float SphereRadius { get; }
        public LayerMask Mask { get; }
    }
}