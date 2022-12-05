using System;

namespace Sources.HealthLogic
{
    public interface IHealth
    {
        public event Action Empty;
        public int Max { get; }
        public int Current { get; }
        public bool IsAlive { get; }
        public void Damage(int value);
        public void Heal(int value);
    }
}