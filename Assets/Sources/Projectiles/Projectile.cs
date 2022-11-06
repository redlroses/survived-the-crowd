using System;
using Sources.Health;
using Sources.Pool;
using UnityEngine;

namespace Projectiles
{
    public abstract class Projectile : MonoBehaviour, IPoolable<Projectile>
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private int _damageValue;
        [SerializeField] private float _lifeTime;

        public event Action<Projectile> Destroyed;

        private void Start()
        {
            SetLifeTime(_lifeTime);
        }

        private void Update()
        {
            Move(_moveSpeed);
        }

        protected abstract void Move(float moveSpeed);

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.Damage(_damageValue);
            }

            Disable();
        }

        private void SetLifeTime(float lifeTime)
        {
            Invoke(nameof(Disable), lifeTime);
        }

        private void Disable()
        {
            Destroyed?.Invoke(this);
        }
    }
}