using System;

namespace Sources.Health
{
    public interface IHealth
    {
        public event Action Empty;
        public int Max { get; }
        public int Current { get; }
        public bool IsAlive { get; }
    }
}