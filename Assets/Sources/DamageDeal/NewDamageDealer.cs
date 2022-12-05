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
    }
}