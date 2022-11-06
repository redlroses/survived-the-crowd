using System;
using Sources.Pool;
using UnityEngine;
using static Sources.Tools.ComponentTool;

namespace Sources.Enemy
{
    public class Enemy : MonoBehaviour, IPoolable<Enemy>
    {
        [SerializeField] private MonoBehaviour _animator;
        [SerializeField] private AgentMover _agentMover;

        public event Action<Enemy> Destroyed;
        private IEnemyAnimator Animator => (IEnemyAnimator) _animator;

        private void OnValidate()
        {
            ValidateInterface<IEnemyAnimator>(ref _animator);
        }

        private void OnEnable()
        {
            Animator.DeathAnimationEnded += DisableInPool;
        }

        private void DisableInPool()
        {
            Destroyed?.Invoke(this);
        }

        private void OnDisable()
        {
            Animator.DeathAnimationEnded -= DisableInPool;
        }

        public void SetTarget(Transform target)
        {
            _agentMover.ApplyTarget(target);
        }

        private void OnDestroy()
        {
            Destroyed?.Invoke(this);
        }
    }
}
