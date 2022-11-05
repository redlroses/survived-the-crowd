using System;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Enemy
{
    [RequireComponent(typeof(Animator))]
    public sealed class AgentMover : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Transform _target;

        private IEnemyAnimator _animator;
        public Transform Target => _target;

        private void Awake()
        {
            _animator = GetComponent<IEnemyAnimator>();
        }

        private void Start()
        {
            _animator.StartMove();
        }

        private void Update()
        {
            MoveToPlayer();
            _animator.SetSpeed(_agent.velocity.magnitude);
        }

        private void MoveToPlayer()
        {
            _agent.destination = _target.position;
        }

        public void ApplyTarget(Transform target)
        {
            _target = target;
        }
    }
}