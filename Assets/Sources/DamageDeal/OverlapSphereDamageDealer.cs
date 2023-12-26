using System.Linq;
using Sources.DamageDeal.Data;
using Sources.HealthLogic;
using UnityEngine;

namespace Sources.DamageDeal
{
    public class OverlapSphereDamageDealer : NewDamageDealer
    {
        private readonly Collider[] _hitColliders = new Collider[1];

        protected override bool TryHit(DamageData data, out IDamageable damageable)
        {
            damageable = null;
            OverlapSphereData overlapSphereData = data as OverlapSphereData;

            int hitsCount =
                Physics.OverlapSphereNonAlloc(
                    overlapSphereData.SphereCenter,
                    overlapSphereData.SphereRadius,
                    _hitColliders,
                    overlapSphereData.Mask);

            return hitsCount > 0 && _hitColliders.FirstOrDefault().TryGetComponent(out damageable);
        }
    }
}