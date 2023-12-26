﻿using System;
using UnityEngine;

namespace Sources.Enemy
{
    public class AgentAttackRangeTracker : MonoBehaviour
    {
        [SerializeField] private float _maxRange;
        [SerializeField] private float _range;
        [SerializeField] [RequireInterface(typeof(IAttackable))] private MonoBehaviour _target;

        private bool _isWithinReach;

        public event Action RangeEntered;

        public event Action RangedExited;

        private IAttackable Target => (IAttackable)_target;

        private void Update()
        {
            if (Target.IsAttackable && IsEnteredInAttackRange())
            {
                _isWithinReach = true;
                RangeEntered?.Invoke();
            }
            else if (Target.IsAttackable == false || IsOutOfAttackRange())
            {
                _isWithinReach = false;
                RangedExited?.Invoke();
            }
        }

        public void Init(IAttackable attackable)
        {
            _target = (MonoBehaviour)attackable;
        }

        private bool IsOutOfAttackRange()
        {
            Vector3 selfPosition = transform.position;

            return _isWithinReach
                   && Vector3.Distance(Target.GetAttackPoint(selfPosition), selfPosition) >= _maxRange;
        }

        private bool IsEnteredInAttackRange()
        {
            Vector3 selfPosition = transform.position;

            return _isWithinReach == false
                   && Vector3.Distance(Target.GetAttackPoint(selfPosition), selfPosition) < _range;
        }
    }
}