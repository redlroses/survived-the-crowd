using System;
using UnityEngine;

namespace Sources.HealthLogic
{
    public class Health : MonoBehaviour, IHealth, IDamageable
    {
        [SerializeField] [Min(0)] private int _maxPoints;
        [SerializeField] [Min(0)] private int _currentPoints;

        public event Action Empty;
        public event Action Changed;

        public int Max => _maxPoints;
        public int Current => _currentPoints;
        public bool IsAlive => _currentPoints > 0;
        public bool IsDead => !IsAlive;

        private void OnEnable()
        {
            _currentPoints = _maxPoints;
            Changed?.Invoke();
        }

        public void Damage(int value)
        {
            Validate(value);

            if (IsDead)
            {
                return;
            }

            _currentPoints -= value;
            CheckIsLethalDamage();
            Changed?.Invoke();
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

        private void Validate(int value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }
        }
    }
}
