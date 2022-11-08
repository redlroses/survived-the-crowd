using System;
using Sources.Custom;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Enemy
{
    public class AgentAttackRangeTracker : MonoBehaviour
    {
        [SerializeField] [RequireInterface(typeof(IAttackable))] private MonoBehaviour _target;
        [SerializeField] private NavMeshAgent _agent;
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
                Debug.Log("in");
                EnteredRange?.Invoke();
            }

            if (Vector3.Distance(Target.GetAttackPoint(transform.position), transform.position) >= _maxRange && _isWithinReach)
            {
                OutOfRange?.Invoke();
                Debug.Log("out");
                _isWithinReach = false;
            }
        }
    }
}