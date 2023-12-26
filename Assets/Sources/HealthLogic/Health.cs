using System;
using UnityEngine;

namespace Sources.HealthLogic
{
    public class Health : MonoBehaviour, IHealth, IDamageable
    {
        public event Action Ended;

        public event Action Changed;

        public bool IsDead => !IsAlive;

        [field: SerializeField]
        [field: Min(0)]
        public int Max { get; private set; }

        [field: SerializeField]
        [field: Min(0)]
        public int Current { get; private set; }

        public bool IsAlive => Current > 0;

        private void OnEnable()
        {
            Current = Max;
            Changed?.Invoke();
        }

        public void Damage(int value)
        {
            Validate(value);

            if (IsDead)
            {
                return;
            }

            Current -= value;
            CheckIsLethalDamage();
            Changed?.Invoke();
        }

        public void Heal(int value)
        {
            Validate(value);

            Current += value;

            if (Current >= Max)
            {
                Current = Max;
            }

            Changed?.Invoke();
        }

        protected void SetMaxPoints(int value)
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            Max = value;
        }

        private void CheckIsLethalDamage()
        {
            if (Current > 0)
            {
                return;
            }

            Ended?.Invoke();
            Current = 0;
        }

        private void Validate(int value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }
        }
    }
}