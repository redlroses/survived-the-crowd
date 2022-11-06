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

            _currentPoints -= value;

            if (_currentPoints <= 0)
            {
                _currentPoints = 0;
            }

            Damaged?.Invoke();
        }
    }
}
