using System;
using Sources.Health;
using UnityEngine;

namespace Sources.Creatures
{
    public abstract class Creature : MonoBehaviour, IDamageable
    {
        [SerializeField] [Min(1)] private int _maxHealth;

        public event Action Died;
        public event Action Damaged;

        public bool IsAlive { get; private set; }

        public int Health { get; private set; }

        protected virtual void OnDamaged(int health)
        {
        }

        protected virtual void OnDied()
        {
        }

        public void Init()
        {
            Health = _maxHealth;
            IsAlive = true;
        }

        public void Damage(int value)
        {
            if (IsAlive == false)
            {
                return;
            }

            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            Health -= value;

            if (Health <= 0)
            {
                Health = 0;
                IsAlive = false;
                Die();
            }

            Damaged?.Invoke();
            OnDamaged(Health);
        }

        private void Die()
        {
            Died?.Invoke();
            OnDied();
        }
    }
}