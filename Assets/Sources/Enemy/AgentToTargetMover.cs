using System;
using Sources.AnimatorStateMachine;
using Sources.Custom;
using Sources.Tools.Extensions;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Enemy
{
    public sealed class AgentToTargetMover : MonoBehaviour
    {
        private readonly float _rotatingSpeed = 10f;

        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private AgentAttackRangeTracker _rangeTracker;
        [SerializeField] [RequireInterface(typeof(IAttackable))] private MonoBehaviour _attackable;
        [SerializeField] [RequireInterface(typeof(IAnimationStateReader))] private MonoBehaviour _stateReader;

        private Vector3 _attackPoint;
        private bool _isInAttackRange;

        private IAttackable Attackable => (IAttackable) _attackable;
        private IAnimationStateReader StateReader => (IAnimationStateReader) _stateReader;

        private void OnEnable()
        {
            _rangeTracker.EnteredRange += OnEnteredRange;
            _rangeTracker.OutOfRange += OnOutOfRange;
        }

        private void OnDisable()
        {
            _rangeTracker.EnteredRange -= OnEnteredRange;
            _rangeTracker.OutOfRange -= OnOutOfRange;
        }

        private void Update()
        {
            FindAttackPoint();

            if (_isInAttackRange)
            {
                RotateToAttackable();
            }
            else
            {
                if (StateReader.State == AnimatorState.Attack)
                {
                    return;
                }

                MoveToAttackable();
            }
        }

        private void OnOutOfRange()
        {
            _isInAttackRange = false;
        }

        private void OnEnteredRange()
        {
            _isInAttackRange = true;
        }

        public void ApplyTarget(IAttackable target)
        {
            _attackable = (MonoBehaviour) target;
        }

        private void MoveToAttackable()
        {
            _agent.destination = _attackPoint;
        }

        private void RotateToAttackable()
        {
            Quaternion toTargetRotation = Quaternion.LookRotation(_attackPoint - transform.position, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toTargetRotation, Time.deltaTime * _rotatingSpeed);
        }

        private void FindAttackPoint()
        {
            _attackPoint = Attackable.GetAttackPoint(attackerPosition: transform.position)
                .SetY(transform.position.y);
        }
    }
}