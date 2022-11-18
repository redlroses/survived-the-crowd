using System;
using Sources.HealthLogic;
using UnityEngine;

namespace Sources.DamageDeal.Data
{
    public class RayDamageDealer : NewDamageDealer
    {
        public event Action<RaycastHit> RayHit;

        protected override bool TryHit(DamageData data, out IDamageable damageable)
        {
            damageable = null;
            var rayData = data as RayCastData;
            bool isHit = Physics.Raycast(rayData.Ray, out RaycastHit hitInfo, float.MaxValue, rayData.Mask);
            RayHit?.Invoke(hitInfo);
            return isHit && hitInfo.collider.TryGetComponent(out damageable);
        }
    }
}