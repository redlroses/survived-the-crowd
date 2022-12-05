using System;

namespace Sources.HealthLogic
{
    public interface IDamageable
    {
        public event Action Changed;
        public void Damage(int value);
    }
}
