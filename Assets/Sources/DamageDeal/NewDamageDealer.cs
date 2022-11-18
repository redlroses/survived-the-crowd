using Sources.DamageDeal.Data;
using Sources.HealthLogic;

namespace Sources.DamageDeal
{
    public abstract class NewDamageDealer
    {
        public void DealDamage(int damage, DamageData data)
        {
            if (TryHit(data, out IDamageable damageable))
            {
                damageable.Damage(damage);
            }
        }

        protected abstract bool TryHit(DamageData data, out IDamageable damageable);

        // private bool TryHit(OverlapSphereData data, out IDamageable damageable)
        // {
        //     damageable = null;
        //     int hitsCount = Physics.OverlapSphereNonAlloc(data.SphereCenter, data.SphereRadius, HitColliders, data.Mask);
        //     return hitsCount > 0 && HitColliders.FirstOrDefault().TryGetComponent(out damageable);
        // }
        //
        // private bool TryHit(RayCastData data, out IDamageable damageable)
        // {
        //     damageable = null;
        //     bool isHit = Physics.Raycast(data.Ray, out RaycastHit hitInfo, float.MaxValue, data.Mask);
        //     RayHit?.Invoke(hitInfo);
        //     return isHit && hitInfo.collider.TryGetComponent(out damageable);
        // }
    }
}