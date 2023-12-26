using Sources.AnimatorStateMachine;
using Sources.Tools.Extensions;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Enemy
{
    public sealed class AgentToTargetMover : MonoBehaviour
    {
        private readonly float _rotatingSpeed = 10f;

        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] [RequireInterface(typeof(IAttackable))] private MonoBehaviour _attackable;
        [SerializeField] private AgentAttackRangeTracker _rangeTracker;
        [SerializeField] private float _speed;
        [SerializeField] [RequireInterface(typeof(IAnimationStateReader))] private MonoBehaviour _stateReader;

        private Vector3 _attackPoint;
        private bool _isInAttackRange;
        private Transform _selfTransform;

        private IAttackable Attackable => (IAttackable)_attackable;

        private IAnimationStateReader StateReader => (IAnimationStateReader)_stateReader;

        private void FixedUpdate()
        {
            Vector3 attackPoint = FindAttackPoint();

            if (_isInAttackRange)
            {
                RotateTo(attackPoint);
            }
            else
            {
                if (StateReader.State == AnimatorState.Attack)
                {
                    return;
                }

                MoveToPoint(attackPoint);
            }
        }

        private void OnEnable()
        {
            _selfTransform = transform;
            _agent.speed = _speed;
            _isInAttackRange = false;
            _rangeTracker.RangeEntered += OnRangeEntered;
            _rangeTracker.RangedExited += OnRangedExited;
        }

        private void OnDisable()
        {
            _rangeTracker.RangeEntered -= OnRangeEntered;
            _rangeTracker.RangedExited -= OnRangedExited;
        }

        public void ApplyTarget(IAttackable target)
        {
            _attackable = (MonoBehaviour)target;
        }

        private void OnRangedExited()
        {
            _isInAttackRange = false;
        }

        private void OnRangeEntered()
        {
            _isInAttackRange = true;
        }

        private void MoveToPoint(Vector3 destination)
        {
            if (StateReader.State == AnimatorState.Hit)
            {
                _agent.speed = 1f / _speed;
            }
            else
            {
                _agent.speed = _speed;
            }

            _agent.SetDestination(destination);
        }

        private void RotateTo(Vector3 attackPoint)
        {
            Vector3 lookDirection = attackPoint - _selfTransform.position;

            if (lookDirection == Vector3.zero)
            {
                return;
            }

            Quaternion toTargetRotation = Quaternion.LookRotation(lookDirection, Vector3.up);

            _selfTransform.rotation = Quaternion.Lerp(
                _selfTransform.rotation,
                toTargetRotation,
                Time.deltaTime * _rotatingSpeed);
        }

        private Vector3 FindAttackPoint()
        {
            Vector3 selfPosition = _selfTransform.position;

            Vector3 attackPoint = Attackable.GetAttackPoint(selfPosition)
                .SetY(selfPosition.y);

            return attackPoint;
        }
    }
}