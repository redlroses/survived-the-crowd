using System;
using Sources.Creatures;
using UnityEngine;
using static Sources.Tools.ComponentTool;

namespace Sources.Enemy
{
    public class Enemy : Creature, IPoolable<Enemy>
    {
        [SerializeField] private MonoBehaviour _animator;
        [SerializeField] private AgentMover _agentMover;

        public event Action<Enemy> Disabled;
        private IEnemyAnimator Animator => (IEnemyAnimator) _animator;

        private void OnValidate()
        {
            ValidateInterface<IEnemyAnimator>(ref _animator);
        }

        private void OnEnable()
        {
            Animator.DeathAnimationEnded += DisableInPool;
        }

        private void OnDisable()
        {
            Animator.DeathAnimationEnded -= DisableInPool;
        }

        public void SetTarget(Transform target)
        {
            _agentMover.ApplyTarget(target);
        }

        protected override void OnDied()
        {
            Animator.PlayDeath();
        }

        private void DisableInPool()
        {
            Disabled?.Invoke(this);
        }
    }
}
