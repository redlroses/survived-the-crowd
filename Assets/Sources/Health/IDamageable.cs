using System;

namespace Sources.Health
{
    public interface IDamageable
    {
        public event Action Damaged;
        public void Damage(int value);
    }
}
