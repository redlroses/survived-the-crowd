using System;
using Sources.HealthLogic;
using Sources.Pool;
using UnityEngine;

namespace Sources.Enemy
{
    public class Enemy : MonoBehaviour, IPoolable<Enemy>
    {
        [SerializeField] [RequireInterface(typeof(IEnemyAnimator))] private MonoBehaviour _animator;
        [SerializeField] [RequireInterface(typeof(IHealth))] private MonoBehaviour _health;
        [SerializeField] private Collider _physicCollider;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private ComponentSwitcher _switcher;

        public event Action<Enemy> Destroyed;

        public IHealth Health => (IHealth)_health;

        private IEnemyAnimator Animator => (IEnemyAnimator)_animator;

        private void OnEnable()
        {
            Animator.DeathAnimationEnded += Disable;
            Health.Ended += OnEndedHealth;
            EnablePhysics();
            _switcher.EnableComponents();
        }

        private void OnDisable()
        {
            Animator.DeathAnimationEnded -= Disable;
            Health.Ended -= OnEndedHealth;
        }

        private void OnDestroy()
        {
            Destroyed?.Invoke(this);
        }

        private void OnEndedHealth()
        {
            DisablePhysics();
            _switcher.DisableComponents();
        }

        private void EnablePhysics()
        {
            _rigidbody.isKinematic = false;
            _physicCollider.enabled = true;
        }

        private void DisablePhysics()
        {
            _rigidbody.isKinematic = true;
            _physicCollider.enabled = false;
        }

        private void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}