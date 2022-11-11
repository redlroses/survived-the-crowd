using System;
using Sources.HealthLogic;
using Sources.Pool;
using UnityEngine;

namespace Sources.Projectiles
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

        private void OnDestroy()
        {
            Destroyed?.Invoke(this);
        }

        protected abstract void Move(float moveSpeed);

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable damageable))
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
            gameObject.SetActive(false);
        }
    }
}