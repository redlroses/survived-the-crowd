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
        [SerializeField] private AgentAttackRangeTracker _rangeTracker;
        [SerializeField] [RequireInterface(typeof(IAttackable))] private MonoBehaviour _attackable;
        [SerializeField] [RequireInterface(typeof(IAnimationStateReader))] private MonoBehaviour _stateReader;
        [SerializeField] private float _speed;

        private Vector3 _attackPoint;
        private bool _isInAttackRange;

        private IAttackable Attackable => (IAttackable) _attackable;
        private IAnimationStateReader StateReader => (IAnimationStateReader) _stateReader;

        private void OnEnable()
        {
            _agent.speed = _speed;
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

                MoveToPoint(destination: attackPoint);
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

        private void MoveToPoint(Vector3 destination)
        {
            _agent.SetDestination(destination);
        }

        private void RotateTo(Vector3 attackPoint)
        {
            Vector3 lookDirection = attackPoint - transform.position;

            if (lookDirection == Vector3.zero)
            {
                return;
            }

            Quaternion toTargetRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toTargetRotation, Time.deltaTime * _rotatingSpeed);
        }

        private Vector3 FindAttackPoint()
        {
            var selfPosition = transform.position;
            Vector3 attackPoint = Attackable.GetAttackPoint(attackerPosition: selfPosition)
                .SetY(selfPosition.y);
            return attackPoint;
        }
    }
}