using System;
using UnityEngine;

namespace Sources.HealthLogic
{
    public class Health : MonoBehaviour, IHealth, IDamageable
    {
        [SerializeField] [Min(0)] private int _maxPoints;
        [SerializeField] [Min(0)] private int _currentPoints;

        public event Action Empty;
        public event Action Damaged;

        public int Max => _maxPoints;
        public int Current => _currentPoints;
        public bool IsAlive => _currentPoints > 0;
        public bool IsDead => !IsAlive;

        private void OnEnable()
        {
            _currentPoints = _maxPoints;
        }

        public void Damage(int value)
        {
            ValidateDamage(value);

            if (IsDead)
            {
                return;
            }

            _currentPoints -= value;
            CheckIsLethalDamage();
            Damaged?.Invoke();
        }

        private void CheckIsLethalDamage()
        {
            if (_currentPoints > 0)
            {
                return;
            }

            Empty?.Invoke();
            _currentPoints = 0;
        }

        private void ValidateDamage(int value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }
        }
    }
}
