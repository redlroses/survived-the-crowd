using System;
using Sources.Custom;
using UnityEngine;
using UnityEngine.AI;

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
            if (Vector3.Distance(Target.GetAttackPoint(transform.position), transform.position) < _range && _isWithinReach == false)
            {
                _isWithinReach = true;
                EnteredRange?.Invoke();
            }

            if (Vector3.Distance(Target.GetAttackPoint(transform.position), transform.position) >= _maxRange && _isWithinReach)
            {
                OutOfRange?.Invoke();
                _isWithinReach = false;
            }
        }
    }
}