﻿using System;
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
        [SerializeField] private float _explosionRadius;
        [SerializeField] private LayerMask _mask;

        public event Action<Projectile> Destroyed;
        public event Action<Projectile> Disabled;

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

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable damageable) == false)
            {
                return;
            }

            foreach (Collider collider in Physics.OverlapSphere(transform.position, _explosionRadius, _mask))
            {
                if (collider.TryGetComponent(out damageable))
                {
                    damageable.Damage(_damageValue);
                }
            }

            Disable();
        }

        public void Construct(int damage)
        {
            _damageValue = damage;
        }

        protected abstract void Move(float moveSpeed);

        private void SetLifeTime(float lifeTime)
        {
            Invoke(nameof(Disable), lifeTime);
        }

        private void Disable()
        {
            Disabled?.Invoke(this);
            gameObject.SetActive(false);
        }
    }
}