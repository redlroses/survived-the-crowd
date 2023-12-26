using System;

namespace Sources.HealthLogic
{
    public interface IHealth
    {
        public int Max { get; }

        public int Current { get; }

        public bool IsAlive { get; }

        public event Action Ended;

        public event Action Changed;

        public void Damage(int value);

        public void Heal(int value);
    }
}