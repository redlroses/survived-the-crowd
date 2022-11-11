using System;
using System.Linq;
using Sources.DamageDeal.Data;
using Sources.HealthLogic;
using UnityEngine;

namespace Sources.DamageDeal
{
    public static class DamageDealer
    {
        private static readonly Collider[] _hitColliders = new Collider[1];

        public static event Action<RaycastHit> RayHited;

        public static void DealDamage(int damage, OverlapSphereData data)
        {
            if (TryHit(data, out IDamageable damageable))
            {
                damageable.Damage(damage);
            }
        }

        public static void DealDamage(int damage, RayCastData data)
        {
            if (TryHit(data, out IDamageable damageable))
            {
                damageable.Damage(damage);
            }
        }

        private static bool TryHit(OverlapSphereData data, out IDamageable damageable)
        {
            damageable = null;
            int hitsCount = Physics.OverlapSphereNonAlloc(data.SphereCenter, data.SphereRadius, _hitColliders, data.Mask);
            return hitsCount > 0 && _hitColliders.FirstOrDefault().TryGetComponent(out damageable);
        }

        private static bool TryHit(RayCastData data, out IDamageable damageable)
        {
            damageable = null;
            bool isHit = Physics.Raycast(data.Ray, out RaycastHit hitInfo, float.MaxValue, data.Mask);
            RayHited?.Invoke(hitInfo);
            return isHit && hitInfo.collider.TryGetComponent(out damageable);
        }
    }
}