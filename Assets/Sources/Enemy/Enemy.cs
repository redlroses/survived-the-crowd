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
        private IEnemyAnimator Animator => (IEnemyAnimator) _animator;
        private IHealth Health => (IHealth) _health;

        private void OnEnable()
        {
            Animator.DeathAnimationEnded += Disable;
            Health.Empty += OnEmptyHealth;
            EnablePhysics();
            _switcher.EnableComponents();
        }

        private void OnDisable()
        {
            Animator.DeathAnimationEnded -= Disable;
            Health.Empty -= OnEmptyHealth;
        }

        private void OnEmptyHealth()
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

        private void OnDestroy()
        {
            Destroyed?.Invoke(this);
        }

        private void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}
