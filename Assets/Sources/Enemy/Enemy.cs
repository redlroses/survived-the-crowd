using System;
using Sources.Custom;
using Sources.Pool;
using UnityEngine;

namespace Sources.Enemy
{
    public class Enemy : MonoBehaviour, IPoolable<Enemy>
    {
        [SerializeField] [RequireInterface(typeof(IEnemyAnimator))] private MonoBehaviour _animator;
        [SerializeField] private AgentToTargetMover _agentToTargetMover;

        public event Action<Enemy> Destroyed;
        private IEnemyAnimator Animator => (IEnemyAnimator) _animator;

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

        private void OnDestroy()
        {
            Destroyed?.Invoke(this);
        }
    }
}
