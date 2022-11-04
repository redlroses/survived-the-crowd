using System;
using UnityEngine;

namespace Projectiles
{
    public abstract class Projectile : MonoBehaviour, IPoolable<Projectile>
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private int _damageValue;
        [SerializeField] private float _lifeTime;

        public event Action<Projectile> Disabled;

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
            Disabled?.Invoke(this);
        }
    }
}