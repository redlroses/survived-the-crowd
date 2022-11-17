using System;
using Sources.Custom;
using UnityEngine;

namespace Sources.Enemy
{
    public class AgentAttackRangeTracker : MonoBehaviour
    {
        [SerializeField] [RequireInterface(typeof(IAttackable))] private MonoBehaviour _target;
        [SerializeField] private float _range;
        [SerializeField] private float _maxRange;

        private bool _isWithinReach;

        public event Action EnteredRange;
        public event Action OutOfRange;

        private IAttackable Target => (IAttackable) _target;

        private void Update()
        {
            if (IsEnteredInAttackRange())
            {
                _isWithinReach = true;
                EnteredRange?.Invoke();
            }

            if (IsOutOfAttackRange())
            {
                _isWithinReach = false;
                OutOfRange?.Invoke();
            }
        }

        private bool IsOutOfAttackRange()
            => Vector3.Distance(Target.GetAttackPoint(transform.position), transform.position) >= _maxRange && _isWithinReach;

        private bool IsEnteredInAttackRange()
            => Vector3.Distance(Target.GetAttackPoint(transform.position), transform.position) < _range && _isWithinReach == false;

        public void Init(IAttackable attackable)
        {
            _target = (MonoBehaviour) attackable;
        }
    }
}