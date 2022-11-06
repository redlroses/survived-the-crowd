using System;

namespace Sources.HealthLogic
{
    public interface IDamageable
    {
        public event Action Damaged;
        public void Damage(int value);
    }
}
